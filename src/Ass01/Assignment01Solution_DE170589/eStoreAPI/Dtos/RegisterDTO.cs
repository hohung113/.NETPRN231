namespace eStoreAPI.Dtos
{
    public class RegisterDTO
    {
        public string Email { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string Password { get; set; } = null!;
        public string PasswordConfirm { get; set; } = null!;
    }
}
