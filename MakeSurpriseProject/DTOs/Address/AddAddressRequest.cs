namespace MakeSurpriseProject.DTOs.Address
{
    public class AddAddressRequest
    {
        public int UserId { get; set; }

        public string AddressTag { get; set; } = null!;

        public int ProvinceId { get; set; }

        public int DistrictId { get; set; }

        public int NeighbourhoodId { get; set; }

        public string FullAddress { get; set; } = null!;
    }
}
