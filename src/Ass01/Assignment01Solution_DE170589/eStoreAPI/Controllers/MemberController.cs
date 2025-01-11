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
        private readonly IJWTService _jwtService;
        public MemberController(IJWTService jWTService)
        {
            _jwtService = jWTService;
        }

        [HttpGet("Login")]
        public IActionResult Login(string email, string password)
        {
            bool isAdmin = false;
            string token = null;
            if (email.Equals("admin@estore.com"))
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
            return Ok(new { Token = token });
        }

    }
}
