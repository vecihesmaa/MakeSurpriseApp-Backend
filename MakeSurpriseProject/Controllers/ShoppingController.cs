using MakeSurpriseProject.DTOs.Shopping;
using MakeSurpriseProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeSurpriseProject.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly ShoppingManager shoppingManager;

        public ShoppingController(ShoppingManager _shoppingManager)
        {
            shoppingManager = _shoppingManager;
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] AddProductRequest product)
        {
            bool isSaved = await shoppingManager.AddProduct(product);
            if (isSaved)
            {
                return Ok();
            }
            return BadRequest();
        }

        public async Task<IActionResult> DeleteShoppingCartItem(int shoppingCartId)
        {
            bool isDeleted = await shoppingManager.DeleteShoppingCartItemAsync(shoppingCartId);
            if (isDeleted)
                return Ok();
            return BadRequest();
        }

        public async Task<IActionResult> GetAllShoppingDetails(int userId)
        {
            var result = await shoppingManager.GetAllShoppingDetailsAsync(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> FinalizeOrder([FromBody] OrderRequest order)
        {
            try
            {
                await shoppingManager.FinalizeOrderAsync(order);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
