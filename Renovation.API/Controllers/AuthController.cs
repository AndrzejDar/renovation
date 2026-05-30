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
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                //Add roles to user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    var rolesResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (!rolesResult.Succeeded)
                    {
                        return BadRequest("User created but role assignment failed");
                    }
                }
                return Ok("User was registered");
            }
            return BadRequest("Something went wrong");
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
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
