using System.Linq.Expressions;
using Villa_VillaAPI.Models;

namespace Villa_VillaAPI.IRepository
{
    public interface IVillaRepository
    {
        Task Create(Villa villa);
        Task Update(Villa villa);

        Task<Villa> GetVilla(int id);
        Task<Villa> GetVilla(Expression<Func<Villa, bool>>? filter = null, bool isTracked = true);
        Task<IEnumerable<Villa>> GetAll(Expression<Func<Villa, bool>>? filter=null);
       

        Task Save();
        Task DeleteVilla(Villa villa);
    }
}
