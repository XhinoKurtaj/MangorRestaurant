using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ShoppingCartAPI.Models.Dto
{
    public class CartHeaderDto
    {
        public int  CartHeaderId { get; set; }
        public required string UserId { get; set; }
        public string CouponCode { get; set; }
    }
}
