using DataAccess.Repository;
using eStoreAPI.JWTServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private IMemberRepository memberRepository = new MemeberRepository();
        private readonly IConfiguration _configuration;
        private readonly IJWTService _jwtService;
        public MemberController(IJWTService jWTService, IConfiguration configuration)
        {
            _jwtService = jWTService;
            _configuration = configuration; 
        }

        [HttpGet("Login")]
        public IActionResult Login(string email, string password)
        {
            bool isAdmin = false;
            string token = null;
            if (email.Equals(_configuration["AdminAccount:Email"]))
            {
                 isAdmin = memberRepository.LoginAdmin(email, password);
                if (isAdmin)
                {
                    token = _jwtService.GenerateToken(email);
                    return Ok(new { Token = token });
                }
            }
            // User
            var user = memberRepository.Login(email, password);
            if (user == false)
            {
                return NotFound();
            }
            token = _jwtService.GenerateToken(email);

            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(30)
            });

            return Ok(new { Token = token });
        }

    }
}
