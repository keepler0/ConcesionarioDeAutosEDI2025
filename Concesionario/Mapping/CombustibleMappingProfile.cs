using AutoMapper;
using Concesionario.Application.Dtos.Combustible;
using Concesionario.Entities;

namespace Concesionario.WebApi.Mapping
{
	public class CombustibleMappingProfile:Profile
	{
        public CombustibleMappingProfile()
        {
            CreateMap<Combustible, CombustibleResponseDto>();
            CreateMap<CombustibleRequestDto, Combustible>();
        }
    }
}
