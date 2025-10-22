using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.Services
{
    public class VillaService : IVillaService
    {
        public IEnumerable<VillaDTO> getVillas()
        {
            return new List<VillaDTO>()
            {
                new VillaDTO(){Id=1,Name="big villa"},
                new VillaDTO(){Id=2,Name="small villa"},

            };
        }
    }
}
