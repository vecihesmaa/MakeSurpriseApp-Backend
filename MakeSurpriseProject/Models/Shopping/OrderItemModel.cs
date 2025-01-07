using System.Globalization;

namespace MakeSurpriseProject.Models.Shopping
{
    public class OrderItemModel
    {
        public int ShoppingCartId { get; set; }

        public int UserRelativeId { get; set; }

        public string? GiftNote { get; set; }

        public decimal Price { get; set; }

        public int AddressId { get; set; }
    }
}
