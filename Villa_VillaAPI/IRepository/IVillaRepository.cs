using Villa_VillaAPI.Models;

namespace Villa_VillaAPI.IRepository
{
    public interface IVillaRepository
    {
        Task Create(Villa villa);
        Task Update(Villa villa);

        Task<Villa> GetVilla(int id);
        Task<IEnumerable<Villa>> GetVillas();

        Task Save();
        Task DeleteVilla(Villa villa);
    }
}
