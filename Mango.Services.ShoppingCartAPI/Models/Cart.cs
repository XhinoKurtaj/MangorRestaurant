namespace Mango.Services.ShoppingCartAPI.Models
{
    public class Cart
    {
        public required CartHeader Header { get; set; }
        public IEnumerable<CartDetails> CartDetails { get; set; }
    }
}
