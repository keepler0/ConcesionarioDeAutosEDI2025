using AutoMapper;
using Concesionario.Application.Dtos.TipoDeVenta;
using Concesionario.Entities;

namespace Concesionario.WebApi.Mapping
{
	public class TipoDeVentaMappingProfile:Profile
	{
        public TipoDeVentaMappingProfile()
        {
            CreateMap<TipoDeVenta,TipoDeVentaResponseDto>();
            CreateMap<TipoDeVentaRequestDto, TipoDeVenta>();
        }
    }
}
