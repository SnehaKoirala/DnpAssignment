using CLI.UI.ManagePosts;
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
            
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await CreatePostAsync();
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
    }
    
}
