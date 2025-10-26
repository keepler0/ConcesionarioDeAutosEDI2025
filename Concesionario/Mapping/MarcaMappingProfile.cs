using AutoMapper;
using Concesionario.Application.Dtos.Marca;
using Concesionario.Entities;

namespace Concesionario.WebApi.Mapping
{
	public class MarcaMappingProfile:Profile
	{
        public MarcaMappingProfile()
        {
            CreateMap<Marca, MarcaResponseDto>();
            CreateMap<MarcaRequestDto,Marca>();
        }
    }
}
