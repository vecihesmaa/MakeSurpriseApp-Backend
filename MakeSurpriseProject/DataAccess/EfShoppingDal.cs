using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DTOs.Shopping;
using MakeSurpriseProject.Entities;
using MakeSurpriseProject.Models.Address;
using MakeSurpriseProject.Models.Shopping;
using Microsoft.EntityFrameworkCore;

namespace MakeSurpriseProject.DataAccess
{
    public class EfShoppingDal
    {
        private readonly MakeSurpriseDbContext _context;
        string gettingReady = "Hazırlanıyor";
        public EfShoppingDal(MakeSurpriseDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddProduct(AddProductRequest product)
        {
            try
            {
                ShoppingCart cart = new ShoppingCart()
                {
                    UserId = product.UserId,
                    UserRelativeId = product.UserRelativeId,
                    Price = product.Price,
                    Note = product.Note,
                };

                await _context.ShoppingCarts.AddAsync(cart);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public async Task<bool> DeleteShoppingCartItemAsync(int shoppingCartId)
        {
            var shoppingCartItem = await _context.ShoppingCarts.Where(shoppingCart => shoppingCart.ShoppingCartId == shoppingCartId).FirstOrDefaultAsync();
            if (shoppingCartItem != null)
            {
                _context.ShoppingCarts.Remove(shoppingCartItem);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ShoppingDetailsResponse> GetAllShoppingDetailsAsync(int userId)
        {
            var shoppingDetailList = await _context.ShoppingCarts.Where(shoppingCart => shoppingCart.UserId == userId)
                                            .Join(
                                                _context.UserRelatives,
                                                cart => cart.UserRelativeId,
                                                relative => relative.UserRelativeId,
                                                (cart, relative) => new ShoppingDetailsModel
                                                {
                                                    UserId = cart.UserId,
                                                    UserRelativeId = cart.UserRelativeId,
                                                    UserRelativeFirstName = relative.FirstName,
                                                    UserRelativeLastName = relative.LastName,
                                                    UserRelativeTag = relative.Tag,
                                                    ShoppingCartId = cart.ShoppingCartId,
                                                    ShoppingCartPrice = cart.Price,
                                                    ShoppingCartNote = cart.Note
                                                })
                                            .ToListAsync();
            var addressDetailList = await _context.Addresses.Select(address => new AddressDetailModel()
            {
                AddressId = address.AddressId,
                AddressTag = address.AddressTag,
                UserId = address.UserId,
            }).Where(address => address.UserId == userId).ToListAsync();

            ShoppingDetailsResponse response = new ShoppingDetailsResponse()
            {
                ShoppingDetailList = shoppingDetailList,
                AddressDetailList = addressDetailList
            };

            return response;
        }

        //public async Task FinalizeOrderAsync(OrderRequest _order)
        //{
        //    Order order = new Order()
        //    {
        //        UserId = _order.UserId,
        //        TotalAmount = _order.OrderItems.Sum(orderItem => orderItem.Price),
        //        OrderDate = DateTime.Now,
        //    };
        //    await _context.Orders.AddAsync(order);
        //    await _context.SaveChangesAsync();

        //    foreach (var _orderItem in _order.OrderItems)
        //    {
        //        OrderItem orderItem = new OrderItem()
        //        {
        //            OrderId = order.OrderId,
        //            UserRelativeId = _orderItem.UserRelativeId,
        //            GiftNote = _orderItem.GiftNote,
        //            Price = _orderItem.Price,
        //            AddressId = _orderItem.AddressId,
        //        };

        //        await _context.OrderItems.AddAsync(orderItem);
        //        await _context.SaveChangesAsync();

        //        Cargo cargo = new Cargo()
        //        {
        //            CargoStatus = gettingReady,
        //            OrderItemId = orderItem.OrderItemId,
        //        };

        //        await _context.Cargos.AddAsync(cargo);
        //        await _context.SaveChangesAsync();

        //        ShoppingCart cart = await _context.ShoppingCarts.Where(shoppingCart => shoppingCart.ShoppingCartId == _orderItem.ShoppingCartId).FirstOrDefaultAsync();
        //        if (cart != null)
        //        {
        //            _context.ShoppingCarts.Remove(cart);
        //            await _context.SaveChangesAsync();
        //        }
        //    }
        //}
    }
}
