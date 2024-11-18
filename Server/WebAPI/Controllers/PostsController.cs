using ApiContracts;
using ApiContracts.Post;
using Entities;
using Microsoft.AspNetCore.Mvc;
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
    public Task<ActionResult<IEnumerable<PostDto>>> GetAllPosts()
    {
        IEnumerable<Post> posts = postRepo.GetMany();
        List<PostDto> dtos = posts.Select(p => new PostDto
        {
            Id = p.PostId,
            Title = p.Title,
            Content = p.Body,
            UserId = p.UserId
        }).ToList();
        return Task.FromResult<ActionResult<IEnumerable<PostDto>>>(Ok(dtos));
    }
    
    // GET: /Posts/{id}
       [HttpGet("{id}")]
       public async Task<ActionResult<PostDto>> GetSinglePost([FromRoute] int id)
       {
           try
           {
               Post post = await postRepo.GetSingleAsync(id);
               PostDto dto = new()
               {
                   Id = post.PostId,
                   Title = post.Title,
                   Content = post.Body,
                   UserId = post.UserId
               };
               return Ok(dto);
           }
           catch (Exception e)
           {
               Console.WriteLine(e);
               return StatusCode(500, $"An error occurred: {e.Message}");
           }
       }
       
   }
