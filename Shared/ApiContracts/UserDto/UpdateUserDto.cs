namespace ApiContracts;

public class UpdateUserDto
{
    public required int UserId { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
}