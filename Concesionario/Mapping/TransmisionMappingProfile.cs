using AutoMapper;
using Concesionario.Application.Dtos.Transmision;
using Concesionario.Entities;

namespace Concesionario.WebApi.Mapping
{
	public class TransmisionMappingProfile:Profile
	{
        public TransmisionMappingProfile()
        {
            CreateMap<Transmision, TransmisionResponseDto>();
            CreateMap<TransmisionRequestDto, Transmision>();
        }
    }
}
