namespace Mango.Services.ShoppingCartAPI.Models.Dto
{
    public class CartDto
    {
        public required CartHeaderDto Header { get; set; }
        public IEnumerable<CartDetailsDto> CartDetails { get; set; }
    }
}
