namespace Mango.Web.Models
{
    public class CartHeaderDto
    {
        public int CartHeaderId { get; set; }
        public required string UserId { get; set; }
        public string CouponCode { get; set; }
        public double OrderTotal { get; set; }
    }
}
