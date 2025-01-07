namespace MakeSurpriseProject.DTOs.Shopping
{
    public class AddProductRequest
    {
        public int UserId { get; set; }

        public int UserRelativeId { get; set; }

        public decimal Price { get; set; }

        public string? Note { get; set; }
    }
}
