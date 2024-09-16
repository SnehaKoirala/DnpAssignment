using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private IUserRepository UserRepository { get; set; }
    private ICommentRepository CommentRepository { get; set; }
    private IPostRepository PostRepository { get; set; }

    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        UserRepository = userRepository;
        CommentRepository = commentRepository;
        PostRepository = postRepository;
    }
    
    public async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("Welcome to the CLI App! Choose an option:");
            Console.WriteLine("1. Create a Post");
            Console.WriteLine("2. Exit");
            Console.WriteLine("3. Create User");
            Console.WriteLine("4. Add Comment");
            Console.WriteLine("5. Display Posts");
            Console.WriteLine("6. View Post Details");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await CreatePostAsync();
                    break;
                case "3":
                    await CreateUserAsync();
                    break;
                case "4":
                    await AddCommentAsync();
                    break;
                case "5":
                    await DisplayPostsAsync();
                    break;
                case "6":
                    await ViewPostDetailsAsync();
                    break;
                case "2":
                    Console.WriteLine("Exiting the app.");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    // Method to handle post creation
    private async Task CreatePostAsync()
    {
        var createPostView = new CreatePostView(PostRepository);
        await createPostView.AddPostAsync();
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
    }

    // Method to handle user creation
    private async Task CreateUserAsync()
    {
        var createUserView = new CreateUserView(UserRepository);
        await createUserView.CreateUser();
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
    }

    // Method to handle comment creation
    private async Task AddCommentAsync()
    {
        var createCommentView = new CreateCommentView(CommentRepository, PostRepository, UserRepository);
        await createCommentView.AddCommentAsync();
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
    }
    // Method to display posts
    private async Task DisplayPostsAsync()
    {
        var listPostView = new ListPostView(PostRepository);
        await listPostView.DisplayPostsAsync();
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
    }
    // Method to view specific post details
    private async Task ViewPostDetailsAsync()
    {
        Console.WriteLine("Enter the Post ID:");
        if (int.TryParse(Console.ReadLine(), out int postId))
        {
            var singlePostView = new SinglePostView(PostRepository, CommentRepository);
            await singlePostView.DisplayPostDetailsAsync(postId);
        }
        else
        {
            Console.WriteLine("Invalid Post ID.");
        }
        Console.WriteLine("Press Enter to return to the main menu.");
        Console.ReadLine();
    }
}