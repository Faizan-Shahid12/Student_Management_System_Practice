using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Student_Management_System.Application.DTO;
using Student_Management_System.Application.Interfaces;
using Student_Management_System.Application.Services;
using Student_Management_System.Domain.Entities;
using Student_Management_System.Infrastructure.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Student_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly JwtSettings options;
        private readonly IStudentService StudService;
        private readonly ITeacherService TeachService;
        
        public AuthController(UserManager<IdentityUser> us, RoleManager<IdentityRole> Rol ,IOptions<JwtSettings> option,IStudentService s1, ITeacherService t1)
        {
            UserManager = us;
            RoleManager = Rol;
            options = option.Value;
            StudService = s1;
            TeachService = t1;
        }

        [HttpPost("RegisterStudent")]
        public async Task<IActionResult> RegisterStudent([FromQuery] RegisterStudentDTO reg)
        {
            var user = new IdentityUser { UserName = reg.FirstName + reg.LastName, Email= reg.Email };

            var result = await UserManager.CreateAsync(user, reg.Password);

            if (await RoleManager.RoleExistsAsync("Student"))
            {
                await UserManager.AddToRoleAsync(user, "Student");
            }

            if (result.Succeeded)
            {
                var student = new StudentDTO()
                { 
                    FirstName=reg.FirstName, 
                    LastName=reg.LastName,
                    Phone = reg.Phone,
                    Department = reg.Department,
                };

                await StudService.AddStudent(student);

                return Ok("Registered Successfully");
            }

            return BadRequest("Please try again");
        }

        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromQuery] string email, [FromQuery] string password)
        {
            var user = new IdentityUser {UserName=email, Email = email };

            var result = await UserManager.CreateAsync(user, password);

            if (await RoleManager.RoleExistsAsync("Admin"))
            {
                await UserManager.AddToRoleAsync(user, "Admin");

                return Ok("Registered Successfully");
            }

            return BadRequest("Please try again");

        }

        [HttpPost("RegisterTeacher")]
        public async Task<IActionResult> Register([FromQuery] RegisterTeacherDTO reg)
        {
            var user = new IdentityUser { UserName = reg.FirstName+reg.LastName, Email= reg.Email };

            var result = await UserManager.CreateAsync(user, reg.Password);

            if (await RoleManager.RoleExistsAsync("Teacher"))
            {
                await UserManager.AddToRoleAsync(user, "Teacher");
            }

            if (result.Succeeded)
            {
                var teach = new TeacherDTO()
                { 
                    FirstName=reg.FirstName, 
                    LastName=reg.LastName,
                    Phone = reg.Phone,
                    Department = reg.Department,
                    Designation = reg.Designation,
                    OfficeLocation = reg.OfficeLocation,
                };

                await TeachService.AddTeacher(teach);

                return Ok("Registered Successfully");
            }

            return BadRequest("Please try again");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] string Email)
        {
            var user = await UserManager.FindByEmailAsync(Email);
            
            if (user == null)
            {
                return BadRequest("User does not exist");
            }

            await UserManager.DeleteAsync(user);

            return Ok("Successful");

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromQuery] LoginDTO log)
        {
            var user = await UserManager.FindByEmailAsync(log.Email);

            if (user == null)
            {
                return BadRequest("UserName incorrect");
            }

            if (await UserManager.CheckPasswordAsync(user, log.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,new Guid().ToString())
                };

                var userRoles = await UserManager.GetRolesAsync(user);

                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = new JwtSecurityToken
                (
                    issuer: options.Issuer,
                    expires: DateTime.Now.AddDays(options.Expiry),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.key!)), SecurityAlgorithms.HmacSha256)
                );

                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized();
        }
    }
}
