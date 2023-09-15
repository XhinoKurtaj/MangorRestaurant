using System.Reflection.PortableExecutable;

namespace Mango.Web.Models
{
    public class CartDto
    {
        public required CartHeaderDto Header { get; set; }
        public IEnumerable<CartDetailsDto> CartDetails { get; set; }
    }
}
