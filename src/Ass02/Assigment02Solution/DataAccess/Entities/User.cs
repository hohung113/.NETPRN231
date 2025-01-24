namespace DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Source { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime HireDate { get; set; }
        public int PubId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
