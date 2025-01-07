using MakeSurpriseProject.Models.Shopping;

namespace MakeSurpriseProject.DTOs.Shopping
{
    public class OrderRequest
    {
        public int UserId { get; set; } 
        public List<OrderItemModel> OrderItems { get; set; }
    }
}
