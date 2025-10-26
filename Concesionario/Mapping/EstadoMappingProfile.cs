using AutoMapper;
using Concesionario.Application.Dtos.Estado;
using Concesionario.Entities;

namespace Concesionario.WebApi.Mapping
{
	public class EstadoMappingProfile:Profile
	{
        public EstadoMappingProfile()
        {
            CreateMap<Estado, EstadoResponseDto>();
            CreateMap<EstadoRequestDto,Estado>();
        }
    }
}
