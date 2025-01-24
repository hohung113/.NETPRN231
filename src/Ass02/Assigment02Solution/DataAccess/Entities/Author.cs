namespace DataAccess.Entities
{
    public class Author : BaseEntity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string EmailAddress { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
