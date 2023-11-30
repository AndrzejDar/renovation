using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Renovation.API.Data;
using Renovation.API.Models.Domain;
using Renovation.API.Models.DTO;
using Renovation.API.Repositories;

namespace Renovation.API.Controllers
{

 


    [Route("api/")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly RenovationAuthDbContext authDbContext;

        public UsersController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository, RenovationAuthDbContext authDbContext)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.authDbContext = authDbContext;
        }

        [HttpGet]
        [Authorize]
        [Route("me")]

        public async Task<IActionResult> GetMyUser()
        {
            var userIdentity = HttpContext.User.Identity;
            string userEmail = null;

            if (userIdentity != null && userIdentity.IsAuthenticated) {
                userEmail = User.Claims.FirstOrDefault(c=>c.Type.EndsWith("emailaddress"))?.Value;
            }

            if(string.IsNullOrEmpty(userEmail))
            {
                return BadRequest("invalid user");
            }
            //var user = await userManager.FindByEmailAsync(userEmail) as ApplicationUser;

            var user = await authDbContext.Users.Include(u=>(u as ApplicationUser).UserProfile).FirstOrDefaultAsync(u=>u.Email==userEmail);

            if (user != null) {
                var userProfile = (user as ApplicationUser)?.UserProfile;
                return Ok(new {ApplicationUser = user,UserProfile = userProfile});
            }
            return BadRequest();
        }
    }
}
