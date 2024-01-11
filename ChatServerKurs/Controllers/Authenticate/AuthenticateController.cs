using ChatServerKurs.Functions.User;
using Microsoft.AspNetCore.Mvc;

namespace ChatServerKurs.Controllers.Authenticate
{

    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : Controller
    {
        private IUserFunction _userFunction;

        public AuthenticateController(IUserFunction userFunction)
        {
            _userFunction = userFunction;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate(AuthenticateRequest request)
        {
            var response = _userFunction.Authenticate(request.LoginId, request.Password);
            if (response == null)
                return BadRequest(new { StatusMessage = "Invalid username or password!" });

            return Ok(response);
        }


        [HttpPost("Register")]
        public IActionResult Register(RegisterRequest request)
        {
            var response = _userFunction.Register(request.LoginId, request.Password, request.UserName, Convert.FromBase64String(request.Avatar));
            if (!response)
                return BadRequest(new { StatusMessage = "Login already exist" });

            return Ok( new { StatusMessage = "Acc created" });
        }
    }
}

