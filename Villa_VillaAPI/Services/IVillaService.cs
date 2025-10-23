using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.Services
{
    public interface IVillaService
    {
        IEnumerable<VillaDTO> GetVillas();
        VillaDTO GetVilla(int id);
        VillaDTO AddVilla(string Name);

        bool DeleteVilla(int id);
    }
}
