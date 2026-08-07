using Microsoft.AspNetCore.Mvc;
using SmartStudentManagementSystemRESTfulAPI.Application.Services;
using SmartStudentManagementSystemRESTfulAPI.Dtos.AccountDtos;

namespace SmartStudentManagementSystemRESTfulAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;


        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }



        // POST: api/account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            await _accountService.RegisterAsync(dto);

            return StatusCode(StatusCodes.Status201Created,
                new
                {
                    message = "User registered successfully."
                });
        }



        // POST: api/account/login
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto dto)
        {
            var response = await _accountService.LoginAsync(dto);

            return Ok(response);
        }



        // POST: api/account/logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();

            return Ok(new
            {
                message = "Logout successful."
            });
        }
    }
}