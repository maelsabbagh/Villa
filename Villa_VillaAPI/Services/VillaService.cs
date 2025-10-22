using Villa_VillaAPI.Data;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.Services
{
    public class VillaService : IVillaService
    {
        public VillaDTO getVilla(int id)
        {
            return VillaStore.villaList.Where(v => v.Id == id).FirstOrDefault();
        }

        public IEnumerable<VillaDTO> getVillas()
        {
            return VillaStore.villaList;
        }
    }
}
