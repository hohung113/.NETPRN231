namespace DataAccess.Entities
{
    public class Publisher : BaseEntity
    {
        public string PublisherName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Book> Books { get; set; }

    }
}
