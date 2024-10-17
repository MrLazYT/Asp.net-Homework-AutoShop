using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTOs.User;
using BusinessLogic.Interfaces;

namespace WebAPI_ShopTech_PV321.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            IdentityUser user = await _accountsService.Get(id);

            return Ok(user);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Post([FromBody] RegisterDto registerDto)
        {
            await _accountsService.Register(registerDto);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginPost([FromBody] LoginDto loginDto)
        {
            string token = await _accountsService.Login(loginDto);

            return Ok(token);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutPost()
        {
            if (User.Identity!.IsAuthenticated)
            {
                await _accountsService.Logout();
                
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
