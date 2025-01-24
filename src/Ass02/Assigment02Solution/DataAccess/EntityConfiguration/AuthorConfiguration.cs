using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityConfiguration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Author");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.BookAuthors)
                .WithOne(x => x.Author)
                .HasForeignKey(y => y.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
