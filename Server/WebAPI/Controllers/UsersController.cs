using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    

// Create Endpoints 
// POST: /Users
    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto request)
    {
        await VerifyUserNameIsAvailableAsync(request.UserName);

        User user = Entities.User.Create(request.UserName, request.Password);
        User created = await userRepo.AddUserAsync(user);
        UserDto dto = new()
        {
            Id = created.UserId,
            UserName = created.UserName
        };
        return Created($"/Users/{dto.Id}", created);
    }

    //PUT: /Users/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserDto request)
    {
        try
        {
            User userToUpdate = await userRepo.GetSingleAsync(id);
            userToUpdate.UserName = request.UserName;
            userToUpdate.Password = request.Password;

            await userRepo.UpdateUserAsync(userToUpdate);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound($"User with ID {id} not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

    // DELETE: /Users/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        try
        {
            User userToDelete = await userRepo.GetSingleAsync(id);
            await userRepo.DeleteUserAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound($"User with ID {id} not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

    // GET: /Users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetSingleUser([FromRoute] int id)
    {
        try
        {
            User user = await userRepo.GetSingleAsync(id);
            UserDto dto = new()
            {
                Id = user.UserId,
                UserName = user.UserName
            };
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound(e.Message);
        }
    }

    [HttpGet]
    public  async Task<ActionResult<IEnumerable<UserDto>>> GetManyUsers()
    {
        IEnumerable<User> users = await userRepo.GetMany().ToListAsync();
        List<UserDto> dtos = users.Select(u => new UserDto
        {
            Id = u.UserId,
            UserName = u.UserName
        }).ToList();
        return Ok(dtos);
    }
    

    private async Task VerifyUserNameIsAvailableAsync(string? username)
    {
        if (await userRepo.GetMany().AnyAsync(u => u.UserName == username))
        {
            throw new InvalidOperationException($"Username '{username}' is already taken.");
        }
    }
}
