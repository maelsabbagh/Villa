using Villa_WebApp.Models.DTO;

namespace Villa_WebApp.Services.IServices
{
    public interface IVillaNumberAPIService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaNumberDTO villaNumberDTO);
        Task<T> UpdateAsync<T>(VillaNumberUpdateDTO villaUpdateDTO);
        Task<T> DeleteAsync<T>(int id);

    }
}
