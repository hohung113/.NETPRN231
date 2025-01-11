using BusinessObject.Entity;

namespace eStoreAPI.Dtos
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = String.Empty;
    }
}
