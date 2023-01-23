namespace GithubSearch.Models;

public class UserAuthDto
{
    public string? Id;
    public string? UserName;
    public string Password = "";
    public string Role = "user";
    public string ConcurrencyStamp = "";
    public List<string>? Roles = null;

    public UserAuthDto() : base()
    {
    }

    public UserAuthDto(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public UserAuthDto(string? id, string? userName, string password)
    {
        Id=id;
        UserName = userName;
        Password = password;
    }

    public UserAuthDto(string? id, string? userName, string password, string role, string concurrencyStamp, List<string> roles)
    {
        Id = id;
        UserName = userName;
        Password = password;
        Role = role;
        ConcurrencyStamp = concurrencyStamp;
        Roles = roles;
    }
}