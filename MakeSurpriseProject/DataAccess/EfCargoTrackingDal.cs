using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace MakeSurpriseProject.DataAccess
{
    public class EfCargoTrackingDal
    {
        private readonly MakeSurpriseFinalDbContext _context;

        public EfCargoTrackingDal(MakeSurpriseFinalDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderItem>> GetAllCargosAsync(int userId)
        {
            var orderItems = await _context.OrderItems
                .Where(o => o.UserRelative.UserId == userId)
                .Include(o => o.Cargos)
                .Include(o => o.Address)
                    .ThenInclude(a => a.Province)
                .Include(o => o.Address)
                    .ThenInclude(a => a.District)
                .Include(o => o.Address)
                    .ThenInclude(a => a.Neighbourhood)
                .Include(o => o.UserRelative)
                .AsNoTracking()
                .ToListAsync();

            return orderItems;
        }
    }
}
