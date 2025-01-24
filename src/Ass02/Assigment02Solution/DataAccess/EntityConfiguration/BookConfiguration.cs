namespace DataAccess.EntityConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Publisher)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.PubId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.BookAuthors)
                .WithOne(x => x.Book)
                .HasForeignKey(y => y.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
