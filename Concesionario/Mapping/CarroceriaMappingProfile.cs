using AutoMapper;
using Concesionario.Application.Dtos.Carroceria;
using Concesionario.Entities;

namespace Concesionario.WebApi.Mapping
{
	public class CarroceriaMappingProfile:Profile
	{
        public CarroceriaMappingProfile()
        {
            CreateMap<Carroceria, CarroceriaResponseDto>();
            CreateMap<CarroceriaRequestDto, Carroceria>();
        }
    }
}
