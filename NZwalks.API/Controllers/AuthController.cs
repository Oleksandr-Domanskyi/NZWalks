using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.Dtos;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequsetDto registerRequsetDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequsetDto.Username,
                Email = registerRequsetDto.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequsetDto.Password);

            if (identityResult.Succeeded)
            {
                //Add rolese to this User
                if (registerRequsetDto.Roles != null && registerRequsetDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequsetDto.Roles);

                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered!! Please login");
                    }
                }
               
            }
            return BadRequest("Something went Wrong!!");

        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if(user != null)
            {
                var checkPasswordRezult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordRezult)
                {
                    //Create token


                    return Ok();
                }
            }
            return BadRequest("Username or Password wrong");
        }
    }
}
