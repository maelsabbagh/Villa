using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Villa>> GetVillas()
        {
            return await _context.Villas
                .ToListAsync();
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
    }
}
