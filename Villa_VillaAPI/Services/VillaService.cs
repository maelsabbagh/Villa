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

        public IEnumerable<VillaDTO> GetVillas()
        {
            return _context.Villas
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
                .ToList();
        }

        public VillaDTO GetVilla(int id)
        {
            //return VillaStore.villaList.Where(v => v.Id == id).FirstOrDefault();

            return _context.Villas
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
                .FirstOrDefault(v => v.Id == id);
                

        }

        public VillaDTO AddVilla(VillaDTO villaDTO)
        {
            int id = _context.Villas.Max(v => v.Id);
            id++;

            Villa villa = new Villa()
            {
                Name = villaDTO.Name,
                ImageURL = villaDTO.ImageURL,
                Details = villaDTO.Details,
                Amenity = villaDTO.Amenity,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqmt = villaDTO.Sqmt,
                CreatedDate=DateTime.Now
            };

            _context.Villas.Add(villa);
            _context.SaveChanges();
            villaDTO.Id = villa.Id;
            return villaDTO;
        }

        public bool DeleteVilla(int id)
        {
            var villa = _context.Villas.FirstOrDefault(v => v.Id == id);
            if (villa == null) return false;


            _context.Villas.Remove(villa);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateVilla(VillaDTO villaDTO)
        {
            var villa = _context.Villas.FirstOrDefault(v => v.Id == villaDTO.Id);

            if (villa == null) return false;

            villa.Name = villaDTO.Name;
            villa.Sqmt = villaDTO.Sqmt;
            villa.Occupancy = villaDTO.Occupancy;
            villa.Amenity = villaDTO.Amenity;
            villa.Rate = villaDTO.Rate;
            villa.Details = villaDTO.Details;
            villa.ImageURL = villaDTO.ImageURL;
            villa.UpdatedDate = DateTime.Now;

            _context.Update(villa);
            _context.SaveChanges();

            return true;
        }
    }
}
