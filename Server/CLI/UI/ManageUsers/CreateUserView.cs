using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers
{
    public class CreateUserView
    {
        private readonly IUserRepository userRepository;
        
        public CreateUserView(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        
        public async Task CreateUser()
        {
            Console.WriteLine("Enter your desired username:");
            string? username = Console.ReadLine();
            
            Console.WriteLine("Enter your password:");
            string? password = Console.ReadLine();
            
            Console.WriteLine("Enter userId:");
            int userId = int.Parse(Console.ReadLine() ?? "0");
            
            // Create a new user
            User newUser = new User(username, password, userId);

            User createdUser = await userRepository.AddUserAsync(newUser);

            if (createdUser != null)
            {
                Console.WriteLine("User Created Successfully:");
                Console.WriteLine($"Username: {createdUser.Username}");
                Console.WriteLine($"You know your password right?");
                Console.WriteLine($"UserId: {createdUser.UserId}");
            }
            else
            {
                Console.WriteLine("Failed to create the user. Please try again.");
            }
        }
    }
}