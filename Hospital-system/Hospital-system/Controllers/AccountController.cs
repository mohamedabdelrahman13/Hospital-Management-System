using AutoMapper;
using Hospital_system.DTOs;
using Hospital_system.Helpers;
using Hospital_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hospital_system.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(IMapper mapper
            ,UserManager<ApplicationUser> userManager
            ,RoleManager<IdentityRole> roleManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var user = mapper.Map<ApplicationUser>(registerDTO);

            IdentityResult res = await userManager.CreateAsync(user , registerDTO.Password);

            if (res.Succeeded) 
            {
               var roleRes =  await userManager.AddToRoleAsync(user , registerDTO.Role);
                if (roleRes.Succeeded)
                {
                    return Ok(new GeneralResponse
                    {
                        StatusCode = 200,
                        Message = "Account is created"
                    });
                }

                return BadRequest(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "role is not saved"
                });
               
            }

            var errorMessage = string.Join(", ", res.Errors.Select(e => e.Description));

            return Ok(new GeneralResponse
            {
                StatusCode = 400,
                Message =   errorMessage
            });
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            ApplicationUser? user = await userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null || user.isDeleted) 
            {
                return Ok(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Your account is not active. Please reach out to the administrator."
                });
            }

            //check if the account is locked
            if (await userManager.IsLockedOutAsync(user))
            {
                return Ok(new GeneralResponse
                {
                    StatusCode = 403,
                    Message = "Account is locked due to multiple failed login attempts. Try again later."
                });
                
            } 

            bool isFound = await userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!isFound)
            {

                // Increment failed attempts
                await userManager.AccessFailedAsync(user);
                return Ok(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Invalid Email or Password"
                });

            }

            // Reset lockout if success
            await userManager.ResetAccessFailedCountAsync(user);


            //creating the claims..
            var userClaims = new List<Claim>();
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            userClaims.Add(new Claim("Id", user.Id));
            userClaims.Add(new Claim(ClaimTypes.Name, user.UserName));
            userClaims.Add(new Claim("email", user.Email));
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                //userClaims.Add(new Claim(ClaimTypes.Role, role));
                userClaims.Add(new Claim("roles", role));
            }

            //_______________________________________________

            //create the signing Credentials
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("efbh3hf3f9u2f8-293ufho21fpi3hf84828433##$$!#8yfgf13ugf"));
            var signCred = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken myToken = new JwtSecurityToken(
                issuer: "https://localhost:44363/",
                audience: "https://localhost:44363/",
                claims: userClaims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: signCred
            );

            var token = new JwtSecurityTokenHandler().WriteToken(myToken);
            return Ok(new
            {
                StatusCode = 200,
                Token = token,
                expiryDate = DateTime.Now.AddHours(8),
            });

        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles =await roleManager.Roles.ToListAsync();
            return Ok(roles);
        }


        [HttpPost("DeleteUser/{UserId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var userFromDB = await userManager.FindByIdAsync(userId);
            if (userFromDB == null)
            {
                return Ok(new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "User not found"
                });
            }

            userFromDB.isDeleted = true;
            userFromDB.DeletedAt = DateTime.Now;

            IdentityResult res = await userManager.UpdateAsync(userFromDB);

            if (res.Succeeded) 
            {
                return Ok(new GeneralResponse
                {
                    StatusCode = 200,
                    Message = "User updated successfully"
                });
                
            }
                return Ok(new GeneralResponse
                {
                    StatusCode = 400,
                    Message = "Error updating user"
                });
       
        }

        [HttpGet("FilterUsers/{query}")]
        public async Task<IActionResult> FilterUsers(string query)
        {
            var usersFromDB = await userManager.Users.Where(u => u.UserName.ToLower().Contains(query.ToLower())).ToListAsync();
            if (!usersFromDB.Any())
            {
                return Ok(new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "No user with specified name",
                });
            }
            var usersDTO = mapper.Map<List<UserWithDoctorDTO>>(usersFromDB);
            return Ok(new GeneralResponse
            {
                StatusCode = 200,
                Message = "Data retrieved",
                Data = usersDTO

            });          
        }
    }
}
