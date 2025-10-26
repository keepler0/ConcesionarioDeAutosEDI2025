using AutoMapper;
using Concesionario.Application.Dtos.Color;
using Concesionario.Entities;

namespace Concesionario.WebApi.Mapping
{
	public class ColorMappingDto:Profile
	{
        public ColorMappingDto()
        {
            CreateMap<Color, ColorResponseDto>();
            CreateMap<ColorResquestDto, Color>();
        }
    }
}
