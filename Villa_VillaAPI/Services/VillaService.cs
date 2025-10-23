using Villa_VillaAPI.Data;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.Services
{
    public class VillaService : IVillaService
    {


        public IEnumerable<VillaDTO> getVillas()
        {
            return VillaStore.villaList;
        }

        public VillaDTO getVilla(int id)
        {
            return VillaStore.villaList.Where(v => v.Id == id).FirstOrDefault();
        }

        public VillaDTO AddVilla(string Name)
        {
            int id = VillaStore.villaList.Max(v => v.Id);
            id++;

            VillaDTO villa = new VillaDTO
            {
                Id = id,
                Name = Name
            };

            VillaStore.villaList.Add(villa);

            return villa;
        }
    }
}
