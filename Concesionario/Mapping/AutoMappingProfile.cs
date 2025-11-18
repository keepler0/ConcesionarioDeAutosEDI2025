using AutoMapper;
using Concesionario.Application.Dtos.Auto;
using Concesionario.Entities;

namespace Concesionario.WebApi.Mapping
{
	public class AutoMappingProfile:Profile
	{
        public AutoMappingProfile()
        {
            CreateMap<Auto, AutoResponseDto>().ForMember(dest=>dest.Marca,opt=>opt.MapFrom(src=>src.Marca!.ToString()))
                                              .ForMember(dest => dest.Carroceria, opt => opt.MapFrom(src => src.Carroceria!.ToString()))
											  .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color!.ToString()))
											  .ForMember(dest => dest.Combustible, opt => opt.MapFrom(src => src.Combustible!.ToString()))
											  .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado!.ToString()))
											  .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais!.ToString()))
											  .ForMember(dest => dest.Traccion, opt => opt.MapFrom(src => src.Traccion!.ToString()))
											  .ForMember(dest => dest.Transmision, opt => opt.MapFrom(src => src.Transmision!.ToString()));
            CreateMap<AutoRequestDto, Auto>();
        }
    }
}
