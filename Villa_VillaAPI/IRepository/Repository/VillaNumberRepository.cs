using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Villa_VillaAPI.Data;
using Villa_VillaAPI.Models;

namespace Villa_VillaAPI.IRepository.Repository
{
    public class VillaNumberRepository : IVillaNumberRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VillaNumberRepository> _logger;

        public VillaNumberRepository(ApplicationDbContext context,ILogger<VillaNumberRepository>logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task Create(VillaNumber villaNumber)
        {
            await _context.VillaNumbers.AddAsync(villaNumber);
            await Save();
        }

        public async Task Delete(VillaNumber villaNumber)
        {
             _context.Remove(villaNumber);
            await Save();
        }

        public async Task<IEnumerable<VillaNumber>> GetAll(Expression<Func<VillaNumber, bool>>? filter = null)
        {
            IQueryable<VillaNumber> query = _context.VillaNumbers
                .AsNoTracking();

            query = query.Include(v=>v.Villa);
            if(filter!=null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<VillaNumber> GetVillaNumber(int villaNo)
        {
            return await _context.VillaNumbers
                .Include(v=>v.Villa)
                .FirstOrDefaultAsync(v => v.VillaNo == villaNo);
        }

        public async Task<VillaNumber> GetVillaNumber(Expression<Func<VillaNumber, bool>>? filter = null, bool isTracked = true)
        {
            IQueryable<VillaNumber> query = _context.VillaNumbers;
            if(!isTracked)
            {
                query = query.AsNoTracking();
            }
            if(filter!=null)
            {
                query = query.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(VillaNumber villaNumber)
        {
            _context.VillaNumbers.Update(villaNumber);
            await Save();
        }

        public async Task<bool> IsVillaNumberExists(int villaNo)
        {
            return await _context.VillaNumbers.AnyAsync(v => v.VillaNo == villaNo);
        }
    }
}
