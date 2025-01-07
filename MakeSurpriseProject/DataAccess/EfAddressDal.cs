using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DTOs.Address;
using MakeSurpriseProject.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace MakeSurpriseProject.DataAccess
{
    public class EfAddressDal
    {
        private readonly MakeSurpriseDbContext _context;

        public EfAddressDal(MakeSurpriseDbContext context)
        {
            _context = context;
        }
        public async Task<List<AddressResponse>> GetAllAddresses(int userId)
        {
            var addresses = await _context.Addresses.Where(address => address.UserId == userId)
                    .Select(address => new AddressResponse
                    {
                        AddressId = address.AddressId,
                        SehirAdi = address.Province.SehirAdi,
                        IlceAdi = address.District.IlceAdi,
                        AddressTag = address.AddressTag,
                        FullAddress = address.FullAddress
                    })
                    .ToListAsync();
            return addresses;

        }

        public async Task<bool> AddAddress(AddAddressRequest _address)
        {
            Address address = new Address()
            {
                AddressTag = _address.AddressTag,
                UserId = _address.UserId,
                DistrictId = _address.DistrictId,
                ProvinceId = _address.ProvinceId,
                NeighbourhoodId = _address.NeighbourhoodId,
                FullAddress = _address.FullAddress,
            };

            try
            {
                await _context.Addresses.AddAsync(address);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteAddress(int addressId)
        {
            var address = await _context.Addresses.Where(address => address.AddressId == addressId).FirstOrDefaultAsync();
            if (address is not null)
            {
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<Sehirler>> GetAllProvinces()
        {
            return await _context.Sehirlers
                    .OrderBy(sehir => sehir.SehirAdi)
                    .ToListAsync();
        }

        public async Task<List<Ilceler>> GetAllDistricts(int provinceId)
        {
            return await _context.Ilcelers.Where(ilce => ilce.SehirId == provinceId).OrderBy(ilce => ilce.IlceAdi).ToListAsync();
        }

        public async Task<List<SemtMah>> GetAllNeighbourhoods(int districtId)
        {
            return await _context.SemtMahs.Where(semtMah => semtMah.IlceId == districtId).OrderBy(semtMah => semtMah.MahalleAdi).ToListAsync();
        }

    }
}
