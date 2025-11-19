using AutoMapper;
using Concesionario.Application.Dtos.Auto;
using Concesionario.Entities;

namespace Concesionario.WebApi.Mapping
{
	public class AutoMappingProfile:Profile
	{
        public AutoMappingProfile()
        {
            CreateMap<Auto, AutoResponseDto>().ForMember(dest=>dest.Marca,opt=>opt.MapFrom(src=>src.Marca!.Descripcion))
                                              .ForMember(dest => dest.Carroceria, opt => opt.MapFrom(src => src.Carroceria!.Descripcion))
											  .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color!.Descripcion))
											  .ForMember(dest => dest.Combustible, opt => opt.MapFrom(src => src.Combustible!.Descripcion))
											  .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado!.Descripcion))
											  .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.Pais!.Nombre))
											  .ForMember(dest => dest.Traccion, opt => opt.MapFrom(src => src.Traccion!.Descripcion))
											  .ForMember(dest => dest.Transmision, opt => opt.MapFrom(src => src.Transmision!.Descripcion));
            CreateMap<AutoRequestDto, Auto>();
        }
    }
}
