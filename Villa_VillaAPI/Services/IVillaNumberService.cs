using System.Linq.Expressions;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.Services
{
    public interface IVillaNumberService
    {
        Task<VillaNumberDTO> GetVillaNumber(Expression<Func<VillaNumber, bool>>? filter = null,bool isTracked=true);
        Task<VillaNumberDTO> GetVillaNumber(int villaNo);
        Task<IEnumerable<VillaNumberDTO>> GetAll(Expression<Func<VillaNumber, bool>>? filter = null);

        Task<VillaNumberDTO> AddVillaNumber(VillaNumberCreateDTO villaNumberCreateDTO);

        Task<bool> DeleteVillaNumber(int id);

    }
}
