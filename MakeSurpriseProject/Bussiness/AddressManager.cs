using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DataAccess;
using MakeSurpriseProject.DTOs.Address;
using MakeSurpriseProject.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MakeSurpriseProject.Services
{
    public class AddressManager
    {
        private readonly MakeSurpriseDbContext context;
        private readonly EfAddressDal efAddressDal;

        public AddressManager(MakeSurpriseDbContext _context, EfAddressDal _efAddressDal)
        {
            context = _context;
            efAddressDal = _efAddressDal;
        }

        public async Task<List<AddressResponse>> GetAllAddresses(int userId)
        {
            var result = await efAddressDal.GetAllAddresses(userId);
            return result;
        }

        public async Task<bool> AddAddress(AddAddressRequest _address)
        {
            var result = await efAddressDal.AddAddress(_address);
            return result;
        }

        public async Task<bool> DeleteAddress(int addressId)
        {
            var result = await efAddressDal.DeleteAddress(addressId);
            return result;
        }

        public async Task<List<Sehirler>> GetAllProvinces()
        {
            var result = await efAddressDal.GetAllProvinces();
            return result;
        }

        public async Task<List<Ilceler>> GetAllDistricts(int provinceId)
        {
            var result = await efAddressDal.GetAllDistricts(provinceId);
            return result;
        }

        public async Task<List<SemtMah>> GetAllNeighbourhoods(int districtId)
        {
            var result = await efAddressDal.GetAllNeighbourhoods(districtId);
            return result;
        }
    }
}
