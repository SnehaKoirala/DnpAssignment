using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }
}

// Create Endpoints 

  /*  [HttpPost]
    public async Task<ActionResult<CreateUserDto>> AddUser([FromBody] CreateUserDto request)
    {
        await VerifyUserNameIsAvailableAsync(request.UserName);
        
        User user = new(request.UserName, request.Password, request.UserId);
        User created = await userRepo.AddUserAsync(user);

       CreateUserDto dto = new CreateUserDto()
        {
            UserId = created.UserId,
            UserName = created.UserName
        };
        
        return Created($"/Users/{dto.UserId}", created);
    }

    private async Task VerifyUserNameIsAvailableAsync(string username)
    {
        var existingUser = await userRepo.GetUserByUsernameAndPasswordAsync(username, null);
        if (existingUser != null)
        {
            throw new InvalidOperationException($"Username ' {username}' is already taken. ");
        }
    }
}*/