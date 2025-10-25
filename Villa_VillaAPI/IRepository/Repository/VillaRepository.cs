using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Villa_VillaAPI.Data;
using Villa_VillaAPI.Models;

namespace Villa_VillaAPI.IRepository.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VillaRepository> _logger;

        public VillaRepository(ApplicationDbContext context,ILogger<VillaRepository>logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task Create(Villa villa)
        {
            await _context.Villas.AddAsync(villa);
            await Save();
        } 

        public async Task<Villa> GetVilla(int id)
        {
            try
            {
                return await _context.Villas
                    .FirstOrDefaultAsync(v => v.Id == id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<Villa> GetVilla(Expression<Func<Villa, bool>> filter = null, bool isTracked = true)
        {
            IQueryable<Villa> query = _context.Villas;
            if (isTracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();

        }


        public async Task DeleteVilla(Villa villa)
        {
            _context.Remove(villa);
            await Save();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(Villa villa)
        {
            _context.Update(villa);
            await Save();

        }

       

        public async Task<IEnumerable<Villa>> GetAll(Expression<Func<Villa,bool>> filter = null)
        {
            IQueryable<Villa> query = _context.Villas;

            if(filter!=null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }
    }
}
