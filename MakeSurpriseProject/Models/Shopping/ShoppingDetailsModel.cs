namespace MakeSurpriseProject.Models.Shopping
{
    public class ShoppingDetailsModel
    {
        public int ShoppingCartId { get; set; }

        public int UserId { get; set; }

        public int UserRelativeId { get; set; }

        public decimal ShoppingCartPrice { get; set; }

        public string? ShoppingCartNote { get; set; }

        public string UserRelativeFirstName { get; set; } = null!;

        public string UserRelativeLastName { get; set; } = null!;

        public string UserRelativeTag { get; set; } = null!;
    }
}
