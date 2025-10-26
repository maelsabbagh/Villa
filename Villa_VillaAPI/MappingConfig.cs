using AutoMapper;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>().ReverseMap();

            CreateMap<VillaCreateDTO, Villa>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate.GetValueOrDefault()));

            CreateMap<VillaUpdateDTO, Villa>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Sqmt, opt => opt.MapFrom(src => src.Sqmt.GetValueOrDefault()))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate.GetValueOrDefault()));

            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumberCreateDTO, VillaNumber>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<VillaNumberUpdateDTO, VillaNumber>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now));


                

        }
    }
}
