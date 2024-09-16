using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task DisplayPostDetailsAsync(int postId)
    {
        var post = await postRepository.GetSingleAsync(postId);
        if (post == null)
        {
            Console.WriteLine("Post not found.");
            return;
        }

        Console.WriteLine($"Title: {post.Title}");
        Console.WriteLine($"Body: {post.Body}");

        var comments = commentRepository.GetMany().Where(c => c.PostId == postId).ToList();
        if (comments.Any())
        {
            Console.WriteLine("Comments:");
            foreach (var comment in comments)
            {
                Console.WriteLine($"- {comment.Body}");
            }
        }
        else
        {
            Console.WriteLine("No comments available.");
        }
    }
}