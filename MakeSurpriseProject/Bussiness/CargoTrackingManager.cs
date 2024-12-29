using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DataAccess;
using MakeSurpriseProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace MakeSurpriseProject.Services
{
    public class CargoTrackingManager
    {
        private readonly EfCargoTrackingDal _efCargoTrackingDal;
        public CargoTrackingManager(EfCargoTrackingDal efCargoTrackingDal)
        {
            _efCargoTrackingDal = efCargoTrackingDal;
        }

        public async Task<List<OrderItem>> GetAllCargosAsync(int userId)
        {
            var result = await _efCargoTrackingDal.GetAllCargosAsync(userId);
            return result;
        }
    }
}
