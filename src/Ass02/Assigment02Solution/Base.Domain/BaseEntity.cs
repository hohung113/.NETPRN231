using System.ComponentModel.DataAnnotations;

namespace Base.Domain
{
    public abstract class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}
