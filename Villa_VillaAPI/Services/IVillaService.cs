using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.Services
{
    public interface IVillaService
    {
        IEnumerable<VillaDTO> getVillas();
        VillaDTO getVilla(int id);
    }
}
