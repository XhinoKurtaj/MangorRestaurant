namespace Mango.Web.Models
{
    public class CartDetailsDto
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        public virtual required CartHeaderDto CartHeader { get; set; }
        public int ProductId { get; set; }
        public virtual required ProductDto Product { get; set; }
        public int Count { get; set; }
    }
}
