using MakeSurpriseProject.ApiServices;
using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DTOs.Shopping;
using MakeSurpriseProject.Entities;
using MakeSurpriseProject.Models;
using MakeSurpriseProject.Models.Address;
using MakeSurpriseProject.Models.Shopping;
using Microsoft.EntityFrameworkCore;
using System;

namespace MakeSurpriseProject.DataAccess
{
    public class EfShoppingDal
    {
        private readonly MakeSurpriseFinalDbContext _context;
        string gettingReady = "Hazırlanıyor";
        private readonly ApiService _apiService;
        public EfShoppingDal(MakeSurpriseFinalDbContext context, ApiService apiService)
        {
            _context = context;
            _apiService = apiService;
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

        public async Task FinalizeOrderAsync(OrderRequest _order)
        {
            Order order = new Order()
            {
                UserId = _order.UserId,
                TotalAmount = _order.OrderItems.Sum(orderItem => orderItem.Price),
                OrderDate = DateTime.Now,
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var _orderItem in _order.OrderItems)
            {

                ApiRequestDataModel answers = _context.UserRelatives
                    .Include(ur => ur.FormAnswer.FirstQuestionAnswerNavigation)
                    .Include(ur => ur.FormAnswer.SecondQuestionAnswerNavigation)
                    .Include(ur => ur.FormAnswer.ThirdQuestionAnswerNavigation)
                    .Include(ur => ur.FormAnswer.SeventhQuestionAnswerNavigation)
                    .Include(ur => ur.FormAnswer.EleventhQuestionAnswerNavigation)
                    .Include(ur => ur.FormAnswer.FourteenthQuestionAnswerNavigation)
                    .Include(ur => ur.FormAnswer.FifteenthQuestionAnswerNavigation)
                    .Where(ur => ur.UserRelativeId == _orderItem.UserRelativeId)
                    .Select(ur => new ApiRequestDataModel
                    {
                        Cinsiyet = ur.FormAnswer.FirstQuestionAnswerNavigation.OptionText,
                        Yas = EfShoppingDal.StringToInt(ur.FormAnswer.SecondQuestionAnswerNavigation.OptionText),
                        Renk = ur.FormAnswer.ThirdQuestionAnswerNavigation.OptionText,
                        //Butce = ur.FormAnswer.FifthQuestionAnswerNavigation.OptionText,
                        Butce = Math.Truncate(_orderItem.Price).ToString() + " TL",
                        Tatil = ur.FormAnswer.SeventhQuestionAnswerNavigation.OptionText,
                        Dekorasyon = ur.FormAnswer.EleventhQuestionAnswerNavigation.OptionText,
                        Giyim = ur.FormAnswer.FourteenthQuestionAnswerNavigation.OptionText,
                        Işiklandirma = ur.FormAnswer.FifteenthQuestionAnswerNavigation.OptionText
                    })
                    .FirstOrDefault();


                string categoryName = await _apiService.PostAsync("http://127.0.0.1:8000/predict", answers);

                var product = await _context.Products.Where(p => p.CategoryName == categoryName && (p.TargetGender == answers.Cinsiyet || p.TargetGender == "Unisex") && (decimal)p.Price <= _orderItem.Price).OrderByDescending(p => p.Price).FirstOrDefaultAsync();

                OrderItem orderItem = new OrderItem()
                {
                    OrderId = order.OrderId,
                    UserRelativeId = _orderItem.UserRelativeId,
                    GiftNote = _orderItem.GiftNote,
                    Price = _orderItem.Price,
                    AddressId = _orderItem.AddressId,
                    ProductId = product?.ProductId ?? null,
                };

                await _context.OrderItems.AddAsync(orderItem);
                await _context.SaveChangesAsync();

                Cargo cargo = new Cargo()
                {
                    CargoStatus = "Hazırlanıyor",
                    OrderItemId = orderItem.OrderItemId,
                };

                await _context.Cargos.AddAsync(cargo);
                await _context.SaveChangesAsync();

                ShoppingCart cart = await _context.ShoppingCarts.Where(shoppingCart => shoppingCart.ShoppingCartId == _orderItem.ShoppingCartId).FirstOrDefaultAsync();
                if (cart != null)
                {
                    _context.ShoppingCarts.Remove(cart);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public static int StringToInt(string value)
        {
            if (value.Contains("-"))
            {
                // '-' ile ayrılan değerleri alıp ortalamasını hesapla
                var parts = value.Split('-');
                int start = int.Parse(parts[0]);
                int end = int.Parse(parts[1]);
                return (start + end) / 2;
            }
            else if (value.EndsWith("+"))
            {
                // '+' ile biten değerlerde sadece sayıyı al
                return int.Parse(value.TrimEnd('+'));
            }
            else
            {
                // Geçerli bir format değilse hata fırlat
                throw new ArgumentException($"Geçersiz format: {value}");
            }
        }
    }
}
