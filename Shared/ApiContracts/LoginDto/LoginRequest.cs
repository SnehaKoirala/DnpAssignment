namespace ApiContracts.LoginDto;

public class LoginRequest(string username, string password)
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}