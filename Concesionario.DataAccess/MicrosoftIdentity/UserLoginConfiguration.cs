using Concesionario.Entities.MicrosoftIdentity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concesionario.DataAccess.MicrosoftIdentity
{
	public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
	{
		public void Configure(EntityTypeBuilder<UserLogin> builder)
		{
			builder.ToTable(nameof(UserLogin));
		}
	}
}
