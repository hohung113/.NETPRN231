using BusinessObject;
using BusinessObject.Entity;
using DataAccess.Repository;
using eStoreAPI.Dtos;
using eStoreAPI.JWTServices;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
            var mem = memberRepository.GetMemberByEmail(email);
            if (mem == null)
            {
                return NotFound("User not found.");
            }
            bool isValidPassword = Helper.VerifyPassword(mem.Password, password);

            if (isValidPassword)
            {
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
            else
            {
                return Unauthorized("Invalid password.");
            }
        }

        // Register
        [HttpPost("Register")]
        public IActionResult Register(RegisterDTO dto)
        {
            if (dto.Password != dto.PasswordConfirm)
            {
                return BadRequest("Password and confirmation do not match.");
            }
            var existingUser = memberRepository.GetMemberByEmail(dto.Email);

            if (existingUser != null)
            {
                return BadRequest("Email is already in use.");
            }

            dto.Password = Helper.HashPassword(dto.Password);

            var mem = dto.Adapt<Member>();
            memberRepository.AddMemeber(mem);

            return Ok("Registration successful.");
        }
    }
}
