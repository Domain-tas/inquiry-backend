using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApi.Controllers.v1.Auth.Responses;
using RestApi.Controllers.v1.AuthController.Requests;

namespace RestApi.Controllers.v1.Auth;

[AllowAnonymous]
[ApiController]
public class AuthController : v1_BaseController
{
    IAuthService _authService;
    IUserService _userService;
    public AuthController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
    {
        var result = await _authService.RegisterUserAsync(request.Name!, request.Password!);
        if (result is null)
            return new BadRequestResult();

        return new OkResult();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("Login")]
    public async Task<ActionResult<SuccessfulLoginResponse>> Login([FromBody] LoginRequest request)
    {
        var user = await _userService.GetUserAsync(request.Name!);
        if(user is null) 
            return new BadRequestResult();

        var token = await _authService.LoginUserAsync(request.Name!, request.Password!);
        if (token is null)
            return new BadRequestResult();

        var result = new SuccessfulLoginResponse
        {
            Token = token.Token,
            Expires = token.Expires,
        };

        return new OkObjectResult(result);
    }
}
