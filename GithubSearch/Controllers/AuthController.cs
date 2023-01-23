using GithubSearch.Auth;
using GithubSearch.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebApi.Controllers;

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthController> _logger;
    private readonly UserAuthDto _defUser;

        public AuthController(ITokenService tokenService,
        ILogger<AuthController> logger)
    {
        _tokenService= tokenService;
        _logger = logger;
        _defUser= new UserAuthDto("user","Uu_123");
    }

    /// <summary>
    /// Представляет токен доступа на основании <paramref name="password"/> и <paramref name="username"/> либо <paramref name="id"/>.
    /// Если указаны оба параметра <paramref name="username"/> и <paramref name="id"/> поиск будет идти по <paramref name="id"/>
    /// </summary>
    /// <param name="id">Id пользователя, пароль у которого уже установлен, например <example>493fbfb1-2608-4c39-be19-906dfa4edde7</example></param>
    /// <param name="username">Пользователь, пароль у которого уже установлен, например <example>Petrov</example></param>
    /// <param name="password">Действующий пароль, например <example>Uu_123</example></param>
    /// <returns>
    /// The <see cref="Task"/>that represents the asynchronous operation, containing the <see cref="ActionResult"/> of the operation.
    /// </returns>
    [HttpGet]
    public ActionResult GetUserToken([FromQuery] string? id, [FromQuery] string? username, [FromQuery] string password)
    {
        if (id == null & username == null) return BadRequest("Должен быть указан хотя бы один из параметров Id или UserName");
        UserAuthDto user = new (id,username,password);
        if(user.UserName == _defUser.UserName & user.Password == _defUser.Password)
        {
            var token = _tokenService.BuildToken(user);
            return Ok(token);
        }
        else return Unauthorized();
    }
}