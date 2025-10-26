using AutoMapper;
using Concesionario.Application.Dtos.TipoDePago;
using Concesionario.Entities;

namespace Concesionario.WebApi.Mapping
{
	public class TipoDePagoMappingProfile:Profile
	{
        public TipoDePagoMappingProfile()
        {
            CreateMap<TipoDePago, TipoDePagoResponseDto>();
            CreateMap<TipoDePagoRequestDto, TipoDePago>();
        }
    }
}
