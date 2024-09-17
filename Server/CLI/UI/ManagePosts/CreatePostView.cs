using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    private readonly User currentUser;

    public CreatePostView(IPostRepository postRepository, User currentUser)
    {
        this.postRepository = postRepository;
        this.currentUser = currentUser;
    }

    public async Task AddPostAsync()
    {
        Console.WriteLine("Enter the post title:");
        string? title1 = Console.ReadLine();
        
        Console.WriteLine("Enter post body:");
        string? body1 = Console.ReadLine();
        
        // Create a new post with the current user's ID
        Post newPost = new Post(title1, body1, currentUser.UserId);

        Post createdPost = await postRepository.AddAsync(newPost);

        if (createdPost != null)
        {
            Console.WriteLine("Post Created Successfully:");
            Console.WriteLine($"Title: {createdPost.Title}");
            Console.WriteLine($"Body: {createdPost.Body}");
            Console.WriteLine($"UserId: {createdPost.UserId}");
        }
        else
        {
            Console.WriteLine("Failed to create the post. Please try again.");
        }
    }
}
