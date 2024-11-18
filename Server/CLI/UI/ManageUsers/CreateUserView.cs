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

            // Create a new user without specifying the userId
            User newUser = User.Create(username, password);

            try
            {
                User createdUser = await userRepository.AddUserAsync(newUser);

                if (createdUser != null)
                {
                    Console.WriteLine("User Created Successfully:");
                    Console.WriteLine($"Username: {createdUser.UserName}");
                    Console.WriteLine($"You know your password right?");
                    Console.WriteLine($"UserId: {createdUser.UserId}");
                }
                else
                {
                    Console.WriteLine("Failed to create the user. Please try again.");
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Exiting the program.");
                Environment.Exit(0);
            }
        }
    }
}