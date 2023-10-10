using Microsoft.AspNetCore.Mvc;
using OnlineLibrarySystem.Jwt;
using OnlineLibrarySystem.Models;

namespace OnlineLibrarySystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly IJwtTokenHandler _jwtTokenHandler;

    public LoginController(IJwtTokenHandler jwtTokenHandler)
    {
        _jwtTokenHandler = jwtTokenHandler;
    }


    [HttpPost]
    public async Task<UserLoggedIn> Login([FromBody] UserAttempt userAttempt)
    {
        string token = null; 
        var userName = userAttempt.UserName.ToLower();
        var ok = userAttempt != null && userName.Equals("john") &&
                 userAttempt.Password.ToLower().Equals("password");

        if (ok)
        {
            token = await _jwtTokenHandler.GetToken(userName);
        }
        
        return ok ? new UserLoggedIn(userName, token) : null; 

    }
}