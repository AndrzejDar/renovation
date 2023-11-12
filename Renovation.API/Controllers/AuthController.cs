using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Renovation.API.Models.DTO;

namespace Renovation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager) {
            this.userManager = userManager;
        }
        
        


        //POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto reqisterRequsetDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = reqisterRequsetDto.Username,
                Email = reqisterRequsetDto.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, reqisterRequsetDto.Password);
        
            if (identityResult.Succeeded)
            {
                //Add roles to user
                if(reqisterRequsetDto.Roles != null && reqisterRequsetDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, reqisterRequsetDto.Roles);
                    if(identityResult.Succeeded)
                    {
                        return Ok("user was registerd");

                    }

                }

            }
            return BadRequest("Someting went wrong");
        }
    }
}
