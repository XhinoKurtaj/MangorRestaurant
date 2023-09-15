using AutoMapper;
using Mango.Services.ShoppingCartAPI.DbContexts;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public CartRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            Cart cart = _mapper.Map<Cart>(cartDto);

            var prodInDb = await _db.Products
                .FirstOrDefaultAsync(x => x.ProductId == cartDto.CartDetails.FirstOrDefault()
                .ProductId);

            if (prodInDb == null)
            {
                _db.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _db.SaveChangesAsync();
            }

            var cartHeaderFromDb = await _db.CartHeader.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == cart.Header.UserId);

            if (cartHeaderFromDb == null)
            {
                _db.CartHeader.Add(cart.Header);
                await _db.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.Header.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                var CartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        x => x.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                        x.CartHeaderId == cartHeaderFromDb.CartHeaderId
                    );

                if (CartDetailsFromDb == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = CartDetailsFromDb.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += CartDetailsFromDb.Count;
                    _db.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartDto>(cart);

        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeaderFromDb = await _db.CartHeader.FirstOrDefaultAsync(
                    x => x.UserId == userId
                );

            if (cartHeaderFromDb != null)
            {
                _db.CartDetails
                    .RemoveRange(_db.CartDetails.Where(x => x.CartHeaderId == cartHeaderFromDb.CartHeaderId));
                _db.CartHeader.Remove(cartHeaderFromDb);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<CartDto> GetCartByUserId(string userId)
        {
            Cart cart = new()
            {
                Header = await _db.CartHeader.FirstOrDefaultAsync(x => x.UserId == userId)
            };

            cart.CartDetails = _db.CartDetails
                .Where(x => x.CartHeaderId == cart.Header.CartHeaderId)
                .Include(x => x.Product);

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = await _db.CartDetails
                    .FirstOrDefaultAsync(x => x.CartDetailsId == cartDetailsId);

                int totalCountOfCartItems = _db.CartDetails
                    .Where(x => x.CartHeaderId == cartDetails.CartHeaderId).Count();

                _db.CartDetails.Remove(cartDetails);

                if (totalCountOfCartItems == 1)
                {
                    var cartHeader = await _db.CartHeader
                        .FirstOrDefaultAsync(x => x.CartHeaderId == cartDetails.CartHeaderId);

                    _db.CartHeader.Remove(cartHeader);
                }

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
