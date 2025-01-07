namespace MakeSurpriseProject.DTOs.Address
{
    public class AddressResponse
    {
        public int AddressId { get; set; }

        public string AddressTag { get; set; } = null!;

        public string SehirAdi { get; set; } = null!;

        public string IlceAdi { get; set; } = null!;

        public string FullAddress { get; set; } = null!;
    }
}
