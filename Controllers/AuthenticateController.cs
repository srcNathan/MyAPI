using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;


[Route("api/[controller]")]
[ApiController]

public class AuthenticateController : ControllerBase
{
    // Vars
    private readonly JwtTokenService _jwtTokenService;


    // Constructor
    public AuthenticateController(JwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }


    // Methods
    [HttpPost("login")]
    public IActionResult Authenticate([FromBody] UserAccount user)
    {
        // Dummy credentials for now. Replace with actual authentication logic in the future
        if (user.Username == "admin" && user.Password == "admin")
        {
            var token = _jwtTokenService.GenerateToken(user.Username);
            return Ok(new { Token = token });
        }

        // Return unauthorized if the username and password combination does not match
        return Unauthorized();
    }


    // END OF CLASS //
}