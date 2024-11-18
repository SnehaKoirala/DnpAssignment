using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;
    private readonly User currentUser;

    public CreateCommentView(ICommentRepository commentRepository, IPostRepository postRepository, User currentUser)
    {
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
        this.currentUser = currentUser;
    }

    public async Task AddCommentAsync()
    {
        Console.WriteLine("Enter the post ID to comment on:");
        int postId = int.Parse(Console.ReadLine());

        var post = await postRepository.GetSingleAsync(postId);
        if (post == null)
        {
            throw new InvalidOperationException($"Post with ID '{postId}' not found.");
        }

        Console.WriteLine("Enter your comment:");
        string? commentText = Console.ReadLine();

        Comment newComment = Comment.Create(commentText, postId, currentUser.UserId);

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