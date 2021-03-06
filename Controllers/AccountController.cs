using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AuthApp.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Extensions.Options;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace AuthApp.Controllers
{
  [Route("api/account")]
  public class AccountController : Controller
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly JWTSettings _options;

    public AccountController(
      UserManager<IdentityUser> userManager,
      SignInManager<IdentityUser> signInManager,
      IOptions<JWTSettings> optionsAccessor)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _options = optionsAccessor.Value;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Credentials Credentials)
    {
      if (ModelState.IsValid)
      {
        var user = new IdentityUser { UserName = Credentials.Email, Email = Credentials.Email };
        var result = await _userManager.CreateAsync(user, Credentials.Password);
        if (result.Succeeded)
        {
          return Ok("Registration Successfully");
        }
        return Errors(result);

      }
      return Error("Unexpected error");
    }

    [HttpPost("token")]
    public async Task<IActionResult> SignIn([FromBody] Credentials Credentials)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(Credentials.Email, Credentials.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(Credentials.Email);
                return new JsonResult(  new Dictionary<string, object>
                {
                    { "access_token", GetAccessToken(user) },
                    { "id_token", GetIdToken(user) }
                });
            }
            return new JsonResult("Username or password is incorrect") { StatusCode = 401 };
        }
        return Error("Unexpected error");
    }

    private string GetIdToken(IdentityUser user) {
      var payload = new Dictionary<string, object>
      {
        { "id", user.Id },
        { "sub", user.Email },
        { "email", user.Email },
        { "emailConfirmed", user.EmailConfirmed },
      };
      return GetToken(payload);
    }

    private string GetAccessToken(IdentityUser user) {
      var payload = new Dictionary<string, object>
      {
        { "email", user.Email },
        { JwtRegisteredClaimNames.UniqueName, user.UserName }
      };
      return GetToken(payload);
    }

    private string GetToken(Dictionary<string, object> payload) {
      var secret = _options.SecretKey;

      payload.Add("iss", _options.Issuer);
      payload.Add("aud", _options.Audience);
      payload.Add("nbf", ConvertToUnixTimestamp(DateTime.Now));
      payload.Add("iat", ConvertToUnixTimestamp(DateTime.Now));
      payload.Add("exp", ConvertToUnixTimestamp(DateTime.Now.AddDays(7)));
      IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
      IJsonSerializer serializer = new JsonNetSerializer();
      IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
      IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

      return encoder.Encode(payload, secret);
    }

    private JsonResult Errors(IdentityResult result)
    {
      var items = result.Errors
          .Select(x => x.Description)
          .ToArray();
      return new JsonResult(items) {StatusCode = 400};
    }

    private JsonResult Error(string message)
    {
      return new JsonResult(message) {StatusCode = 400};
    }

    private static double ConvertToUnixTimestamp(DateTime date)
    {
      DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
      TimeSpan diff = date.ToUniversalTime() - origin;
      return Math.Floor(diff.TotalSeconds);
    }
  }
}