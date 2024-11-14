using ApiContracts;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using LoginRequest = ApiContracts.LoginDto.LoginRequest;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("auth/login")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult<LoginRequest>> Login([FromBody] LoginRequest loginRequest)
        {
            // Find user by username
            var user = await userRepository.GetUserByUsernameAndPasswordAsync(loginRequest.UserName, loginRequest.Password);

            // Check if User exists
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
}