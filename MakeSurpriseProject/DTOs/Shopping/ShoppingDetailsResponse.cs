using MakeSurpriseProject.Models.Address;
using MakeSurpriseProject.Models.Shopping;

namespace MakeSurpriseProject.DTOs.Shopping
{
    public class ShoppingDetailsResponse
    {
        public List<ShoppingDetailsModel> ShoppingDetailList { get; set; }

        public List<AddressDetailModel> AddressDetailList{ get; set; }
    }
}
