using AutoMapper;
using Concesionario.Application.Dtos.Pais;
using Concesionario.Entities;

namespace Concesionario.WebApi.Mapping
{
	public class PaisMappingProfile:Profile
	{
        public PaisMappingProfile()
        {
            CreateMap<Pais, PaisResponseDto>();
            CreateMap<PaisRequestDto,Pais>();
        }

    }
}
