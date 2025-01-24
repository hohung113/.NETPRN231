using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Users)
                .WithOne(x => x.Role)
                .HasForeignKey(y => y.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
