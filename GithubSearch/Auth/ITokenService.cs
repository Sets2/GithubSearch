namespace GithubSearch.Auth;

public interface ITokenService
{
    string BuildToken(Models.UserAuthDto user);
}