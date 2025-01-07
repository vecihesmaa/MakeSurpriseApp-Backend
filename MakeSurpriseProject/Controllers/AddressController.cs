using MakeSurpriseProject.DTOs.Address;
using MakeSurpriseProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakeSurpriseProject.Controllers
{
    public class AddressController : Controller
    {
        private readonly AddressManager addressManager;

        public AddressController(AddressManager _addressManager)
        {
            addressManager = _addressManager;
        }


        public async Task<IActionResult> GetAllAddresses(int userId)
        {
            var addresses = await addressManager.GetAllAddresses(userId);
            return Ok(addresses);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddAddressRequest address)
        {
            bool isSaved = await addressManager.AddAddress(address);
            if (!isSaved)
            {
                return BadRequest();
            }
            return Ok();
        }

        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            bool isDeleted = await addressManager.DeleteAddress(addressId);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return Ok();
        }

        public async Task<IActionResult> GetAllProvinces()
        {
            var provinces = await addressManager.GetAllProvinces();
            return Ok(provinces);
        }

        public async Task<IActionResult> GetAllDistricts(int provinceId)
        {
            var districts = await addressManager.GetAllDistricts(provinceId);
            return Ok(districts);
        }

        public async Task<IActionResult> GetAllNeighbourhoods(int districtId)
        {
            var neighbourhoods = await addressManager.GetAllNeighbourhoods(districtId);
            return Ok(neighbourhoods);
        }
    }
}
