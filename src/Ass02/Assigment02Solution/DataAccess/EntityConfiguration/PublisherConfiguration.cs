using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityConfiguration
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("Publisher");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Users)
                .WithOne(x => x.Publisher)
                .HasForeignKey(y => y.PubId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Books)
                 .WithOne(x => x.Publisher)
                 .HasForeignKey(y => y.PubId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
