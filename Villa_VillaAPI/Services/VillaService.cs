using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Villa_VillaAPI.Data;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.Services
{
    public class VillaService : IVillaService
    {
        private readonly ApplicationDbContext _context;

        public VillaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VillaDTO>> GetVillas()
        {
            return await _context.Villas
                .AsNoTracking()
                .Select(v=>new VillaDTO()
                {
                    Id=v.Id,
                    Name=v.Name,
                    Amenity=v.Amenity,
                    Details=v.Details,
                    ImageURL=v.ImageURL,
                    Occupancy= v.Occupancy,
                    Rate= v.Rate,
                    Sqmt = v.Sqmt
                })
                .ToListAsync();
        }

        public async Task<VillaDTO> GetVilla(int id)
        {
            //return VillaStore.villaList.Where(v => v.Id == id).FirstOrDefault();

            return await _context.Villas
                .AsNoTracking()
                .Select(v => new VillaDTO()
                {
                    Id = v.Id,
                    Name = v.Name,
                    Amenity = v.Amenity,
                    Details = v.Details,
                    ImageURL = v.ImageURL,
                    Occupancy = v.Occupancy,
                    Rate = v.Rate,
                    Sqmt = v.Sqmt
                })
                .FirstOrDefaultAsync(v => v.Id == id);
                

        }

        public async Task<VillaDTO> AddVilla(VillaCreateDTO villaDTO)
        {

            Villa villa = new Villa()
            {
                Name = villaDTO.Name,
                ImageURL = villaDTO.ImageURL,
                Details = villaDTO.Details,
                Amenity = villaDTO.Amenity,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate.GetValueOrDefault(),
                Sqmt = villaDTO.Sqmt,
                CreatedDate=DateTime.Now
            };

            await _context.Villas.AddAsync(villa);
            await _context.SaveChangesAsync();

            var savedVillaDTO = new VillaDTO()
            {
                Name = villa.Name,
                ImageURL = villa.ImageURL,
                Details = villa.Details,
                Amenity = villa.Amenity,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqmt = villa.Sqmt,
                Id = villa.Id
            };
            return savedVillaDTO;
        }

        public async Task<bool> DeleteVilla(int id)
        {
            var villa = await _context.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null) return false;


            _context.Villas.Remove(villa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateVilla(VillaUpdateDTO villaDTO)
        {
            var villa = await _context.Villas.FirstOrDefaultAsync(v => v.Id == villaDTO.Id);

            if (villa == null) return false;

            villa.Name = villaDTO.Name;
            villa.Sqmt = villaDTO.Sqmt.GetValueOrDefault();
            villa.Occupancy = villaDTO.Occupancy;
            villa.Amenity = villaDTO.Amenity;
            villa.Rate = villaDTO.Rate.GetValueOrDefault();
            villa.Details = villaDTO.Details;
            villa.ImageURL = villaDTO.ImageURL;
            villa.UpdatedDate = DateTime.Now;

            _context.Update(villa);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
