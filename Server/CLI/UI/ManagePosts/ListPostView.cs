using RepositoryContracts;



namespace CLI.UI.ManagePosts;

public class ListPostView
{
    private readonly IPostRepository postRepository;

    public ListPostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task DisplayPostsAsync()
    {
        var posts = postRepository.GetMany().ToList();

        if (posts.Any())
        {
            Console.WriteLine("Posts Overview:");
            foreach (var post in posts)
            {
                Console.WriteLine($"ID: {post.PostId}, Title: {post.Title}");
            }
        }
        else
        {
            Console.WriteLine("No posts available.");
        }
    }
}