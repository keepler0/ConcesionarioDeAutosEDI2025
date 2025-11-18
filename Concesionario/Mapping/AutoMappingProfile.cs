using AutoMapper;
using Concesionario.Application.Dtos.Auto;
using Concesionario.Entities;

namespace Concesionario.WebApi.Mapping
{
	public class AutoMappingProfile:Profile
	{
        public AutoMappingProfile()
        {
            CreateMap<Auto, AutoResponseDto>();
            CreateMap<AutoRequestDto, Auto>();
        }
    }
}
