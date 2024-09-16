using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    public CreateCommentView(ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository)
    {
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
        this.userRepository = userRepository;
    }

    public async Task AddCommentAsync()
    {
        Console.WriteLine("Do you want to comment based on (1) Post ID or (2) User ID?");
        string choice = Console.ReadLine();

        int postId = 0;
        int userId = 0;

        if (choice == "1")
        {
            Console.WriteLine("Enter the post ID to comment on:");
            postId = int.Parse(Console.ReadLine());

            var post = await postRepository.GetSingleAsync(postId);
            if (post == null)
            {
                throw new InvalidOperationException($"Post with ID '{postId}' not found.");
            }

            userId = post.UserId;
        }
        else if (choice == "2")
        {
            Console.WriteLine("Enter the user ID to comment as:");
            userId = int.Parse(Console.ReadLine());

            var user = await userRepository.GetSingleAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID '{userId}' not found.");
            }

            Console.WriteLine("Enter the post ID to comment on:");
            postId = int.Parse(Console.ReadLine());

            var post = await postRepository.GetSingleAsync(postId);
            if (post == null)
            {
                throw new InvalidOperationException($"Post with ID '{postId}' not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        Console.WriteLine("Enter your comment:");
        string? commentText = Console.ReadLine();

        Comment newComment = new Comment(commentText, postId, userId);

        Comment createdComment = await commentRepository.AddCommentAsync(newComment);

        if (createdComment != null)
        {
            Console.WriteLine("Comment Added Successfully:");
            Console.WriteLine($"Post ID: {createdComment.PostId}");
            Console.WriteLine($"User ID: {createdComment.UserId}");
            Console.WriteLine($"Comment: {createdComment.Body}");
        }
        else
        {
            Console.WriteLine("Failed to add the comment. Please try again.");
        }
    }
}