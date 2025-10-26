using AutoMapper;
using Concesionario.Application.Dtos.Traccion;
using Concesionario.Entities;

namespace Concesionario.WebApi.Mapping
{
	public class TraccionMappingProfile:Profile
	{
        public TraccionMappingProfile()
        {
            CreateMap<Traccion, TraccionResponseDto>();
            CreateMap<TraccionRequestDto, Traccion>();
        }
    }
}
