namespace MakeSurpriseProject.Models.Address
{
    public class AddressDetailModel
    {
        public int UserId { get; set; }

        public int AddressId { get; set; }

        public string AddressTag { get; set; } = null!;
    }
}
