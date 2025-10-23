using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>()
        {
            new VillaDTO(){Id=1,Name="big villa",Sqft=500,Occupancy=6},
            new VillaDTO(){Id=2,Name="small villa",Sqft=250,Occupancy=4},
        };
    }

}
