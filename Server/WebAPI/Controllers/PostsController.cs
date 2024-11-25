using ApiContracts;
using ApiContracts.Comment;
using ApiContracts.Post;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository postRepo;

    public PostsController(IPostRepository postRepo)
    {
        this.postRepo = postRepo;
    }

    // Create Endpoints
    // POST: /Posts

    [HttpPost]
    public async Task<ActionResult<PostDto>> AddPost([FromBody] CreatePostDto request)
    {
        //check if the post already exists with same title
        if (await postRepo.GetMany().AnyAsync(p => p.Title == request.Title))
        {
            return Conflict($"A post with the title '{request.Title}' already exists.");
        }

        Post post = Post.Create(request.Title, request.Content, request.UserId);
        Post created = await postRepo.AddAsync(post);
        PostDto dto = new()
        {
            Id = created.PostId,
            Title = created.Title,
            Content = created.Body,
            UserId = created.UserId
        };
        return Created($"/Posts/{dto.Id}", dto);
    }

    // PUT: /Posts/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePost([FromRoute] int id, [FromBody] UpdatePostDto request)
    {
        try
        {
            Post postToUpdate = await postRepo.GetSingleAsync(id);
            postToUpdate.Title = request.Title;
            postToUpdate.Body = request.Content;

            await postRepo.UpdateAsync(postToUpdate);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Post with ID {id} not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

    // DELETE: /Posts/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePost([FromRoute] int id)
    {
        try
        {
            await postRepo.DeleteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Post with ID {id} not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

    // GET: /Posts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetAllPosts()
    {
        IEnumerable<Post> posts = await postRepo.GetMany().ToListAsync();
        List<PostDto> dtos = posts.Select(p => new PostDto
        {
            Id = p.PostId,
            Title = p.Title,
            Content = p.Body,
            UserId = p.UserId
        }).ToList();
        return Ok(dtos);
    }

    // GET: /Posts/{id}
    [HttpGet("{id}")]
    public async Task<IResult> GetPost([FromRoute] int id, [FromQuery] bool includeAuthor,
        [FromQuery] bool includeComments)
    {
        IQueryable<Post> queryForPost = postRepo.GetMany().Where(p => p.PostId == id).AsQueryable();
        if (includeAuthor)
        {
            queryForPost = queryForPost.Include(p => p.User);
        }

        if (includeComments)
        {
            queryForPost = queryForPost.Include(p => p.Comments);
        }

        PostDto? dto = await queryForPost.Select(post => new PostDto()
        {
            Id = post.PostId, Title = post.Title, Content = post.Body, UserId = post.UserId,
            Author = includeAuthor ? new UserDto { Id = post.User.UserId, UserName = post.User.UserName } : null,
            Comments = includeComments
                ? post.Comments.Select(c => new CommentDto { Id = c.CommentId, Content = c.Body, AuthorUserId = c.UserId })
                    .ToList()
                : new()
        }).FirstOrDefaultAsync();
        return dto == null ? Results.NotFound() : Results.Ok(dto);
    }
}