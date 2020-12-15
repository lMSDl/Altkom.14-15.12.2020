using Microsoft.AspNetCore.Mvc;
using Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Post([FromBody] User user) {
            var token = _authService.Authenticate(user);

            if(token == null)
            return BadRequest(new {message = "Incorrect credentials"});
            return Ok(token);
        } 
    }
}