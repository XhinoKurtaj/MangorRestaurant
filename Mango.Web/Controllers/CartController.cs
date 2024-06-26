﻿using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CartController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public CartController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            //var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var userId = User.Claims.ToList()[2]?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveFromCartAsync<ResponseDto>(cartDetailsId, accessToken);

            CartDto cartDto = new();

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
        {
            //var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var userId = User.Claims.ToList()[2]?.Value;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.GetCartByUserIdAsync<ResponseDto>(userId, accessToken);

            CartDto cartDto = new();

            if (response != null && response.IsSuccess)
            {
                cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
            }

            if(cartDto.Header != null)
            {
                foreach(var detail in cartDto.CartDetails)
                {
                    cartDto.Header.OrderTotal += (detail.Product.Price * detail.Count);
                }
            }
            return cartDto;
        }
    }
} 
