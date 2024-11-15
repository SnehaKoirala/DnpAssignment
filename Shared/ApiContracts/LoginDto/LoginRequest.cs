using System.ComponentModel.DataAnnotations;

namespace ApiContracts.LoginDto
{
    public class LoginRequest
    {
        public LoginRequest(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}