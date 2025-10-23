using Microsoft.AspNetCore.Http.HttpResults;
using Villa_VillaAPI.Data;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.Services
{
    public class VillaService : IVillaService
    {


        public IEnumerable<VillaDTO> GetVillas()
        {
            return VillaStore.villaList;
        }

        public VillaDTO GetVilla(int id)
        {
            return VillaStore.villaList.Where(v => v.Id == id).FirstOrDefault();
        }

        public VillaDTO AddVilla(VillaDTO villaDTO)
        {
            int id = VillaStore.villaList.Max(v => v.Id);
            id++;

            VillaDTO villa = new VillaDTO
            {
                Id = id,
                Name = villaDTO.Name,
                Occupancy=villaDTO.Occupancy,
                Sqft=villaDTO.Sqft
            };

            VillaStore.villaList.Add(villa);

            return villa;
        }

        public bool DeleteVilla(int id)
        {
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            if (villa == null) return false;


            VillaStore.villaList.Remove(villa);
            return true;
        }

        public bool UpdateVilla(VillaDTO villaDTO)
        {
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == villaDTO.Id);

            if (villa == null) return false;

            villa.Name = villaDTO.Name;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;

            return true;
        }
    }
}
