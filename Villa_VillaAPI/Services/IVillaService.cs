using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.Services
{
    public interface IVillaService
    {
        Task<IEnumerable<VillaDTO>> GetVillas();
        Task<VillaDTO> GetVilla(int id);
        Task<VillaDTO> AddVilla(VillaDTO villaDTO);

        Task<bool> DeleteVilla(int id);

        Task<bool> UpdateVilla(VillaDTO villaDto);
    }
}
