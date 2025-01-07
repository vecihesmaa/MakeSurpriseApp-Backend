using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DataAccess;
using MakeSurpriseProject.DTOs.Shopping;
using MakeSurpriseProject.Entities;
using MakeSurpriseProject.Models.Address;
using MakeSurpriseProject.Models.Shopping;
using Microsoft.EntityFrameworkCore;

namespace MakeSurpriseProject.Services
{
    public class ShoppingManager
    {
        private readonly EfShoppingDal efShoppingDal;

        public ShoppingManager(EfShoppingDal _efShoppingDal)
        {
            efShoppingDal = _efShoppingDal;
        }

        public async Task<bool> AddProduct(AddProductRequest product)
        {
            var result = await efShoppingDal.AddProduct(product);
            return result;
        }

        public async Task<bool> DeleteShoppingCartItemAsync(int shoppingCartId)
        {
            var result = await efShoppingDal.DeleteShoppingCartItemAsync(shoppingCartId);
            return result;
        }

        public async Task<ShoppingDetailsResponse> GetAllShoppingDetailsAsync(int userId)
        {
            var result = await efShoppingDal.GetAllShoppingDetailsAsync(userId);
            return result;
        }   

        //public async Task FinalizeOrderAsync(OrderRequest _order)
        //{
            
        //}
    }
}
