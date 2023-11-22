using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Renovation.API.Models.DTO;
using Renovation.API.Repositories;

namespace Renovation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository) {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
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


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LogitRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null) {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    //get roles
                    var roles = await userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        //create token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    }                  
                }
            }
            return BadRequest("User or password is incorrect");
        }
    }
}
