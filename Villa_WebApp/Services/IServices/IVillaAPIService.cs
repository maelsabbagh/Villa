using Villa_Villa_WebApp.Models.DTO;
using Villa_WebApp.Models.DTO;

namespace Villa_WebApp.Services.IServices
{
    public interface IVillaAPIService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaCreateDTO villaCreateDTO);
        Task<T> UpdateAsync<T>(VillaUpdateDTO villaUpdateDTO);
        Task<T> DeleteAsync<T>(int id);
    }
}
