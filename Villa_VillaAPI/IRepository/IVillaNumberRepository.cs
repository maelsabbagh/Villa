using System.Linq.Expressions;
using Villa_VillaAPI.Models;

namespace Villa_VillaAPI.IRepository
{
    public interface IVillaNumberRepository
    {
        Task Create(VillaNumber villaNumber);
        Task Update(VillaNumber villaNumber);
        Task Delete(VillaNumber villaNumber);
        Task<VillaNumber> GetVillaNumber(int villaNo);
        Task<VillaNumber> GetVillaNumber(Expression<Func<VillaNumber, bool>>? filter = null,bool isTracked=true);
        Task<IEnumerable<VillaNumber>> GetAll(Expression<Func<VillaNumber, bool>>? filter = null);
        Task<bool> IsVillaNumberExists(int villaNo);
        Task Save();
    }
}
