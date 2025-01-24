namespace DataAccess.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public int PubId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public decimal Price { get; set; }
        public string Advance { get; set; }
        public string Royalty { get; set; } 
        public DateTime YtdSales { get; set; }
        public string Notes { get; set; }
        public DateTime PublishedDate { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
