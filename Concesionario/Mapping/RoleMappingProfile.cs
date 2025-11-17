using AutoMapper;
using Concesionario.Application.Dtos.Identity.Roles;
using Concesionario.Entities.MicrosoftIdentity;

namespace Concesionario.WebApi.Mapping
{
	public class RoleMappingProfile:Profile
	{
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleResponseDto>();
            CreateMap<RoleRequestDto, Role>();
        }
    }
}
