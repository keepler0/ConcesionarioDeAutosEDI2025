using AutoMapper;
using Concesionario.Application.Dtos.CategoriaTributaria;
using Concesionario.Entities;

namespace Concesionario.WebApi.Mapping
{
	public class CategoriaTributariaMappingProfile:Profile
	{
        public CategoriaTributariaMappingProfile()
        {
            CreateMap<CategoriaTributaria, CategoriaTributariaResponseDto>();
            CreateMap<CategoriaTributariaRequestDto,CategoriaTributaria>();
        }
    }
}
