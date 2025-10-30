using Concesionario.Entities.MicrosoftIdentity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concesionario.DataAccess.MicrosoftIdentity
{
	public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
	{
		public void Configure(EntityTypeBuilder<UserClaim> builder)
		{
			builder.ToTable(nameof(UserClaim));
		}
	}
}
