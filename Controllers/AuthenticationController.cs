using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PatientRecordApi.Services;

namespace PatientRecordApi.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly UserServices _userServices;

    public AuthenticationController(UserServices userServices, ILogger<AuthenticationController> logger)
    {
        _logger = logger;
        _userServices = userServices;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(User user)
    {
        var loggedIn = _userServices.Login(user);

        if (loggedIn.Result)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email)
                };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            return Ok("Logged in successfully");
        }
        else
            return Unauthorized("Unauthorized");
    }

    [HttpPost("signUp")]
    public async Task<ActionResult> SignUp(User user)
    {
        try
        {
            await _userServices.SignUp(user);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }

        return Ok("SignedUp successfully");

    }


    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Ok();
    }
}