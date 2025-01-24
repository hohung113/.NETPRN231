namespace DataAccess.Entities
{
    public class BookAuthor : BaseEntity
    {
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int AuthorOrder { get; set; }
        public decimal RoyalityPercentange { get; set; }
    }
}
