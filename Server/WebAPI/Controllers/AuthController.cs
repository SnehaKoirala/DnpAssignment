using ApiContracts;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using LoginRequest = ApiContracts.LoginDto.LoginRequest;
namespace WebAPI.Controllers;

[ApiController]
[Route("auth/login")] 

public class AuthController: ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    } 
    
    [HttpPost]
    public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        // Find user by username
        var user = await _userRepository.GetUserByUsernameAndPasswordAsync(loginRequest.UserName,loginRequest.Password);
        
       //Check if User exists
       if (user == null)
        {
            return Unauthorized("Invalid Username or Password");
        }
       
       // Check if password is correct
       if (user.Password != loginRequest.Password)
       {
           return Unauthorized("Incorrect Password");
       }

       var userDto = new UserDto() 
       {  
           Id = user.UserId,
           UserName = user.UserName
       }; 
       return Ok(userDto); 
    }   
}