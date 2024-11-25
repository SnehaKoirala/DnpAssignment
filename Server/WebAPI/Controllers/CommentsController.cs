using ApiContracts.Comment;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository commentRepo;
    
    public CommentsController(ICommentRepository commentRepo)
    {
        this.commentRepo = commentRepo;
    }
    
    // Create Endpoints
    // POST: /Comments
    
    [HttpPost]
    public async Task<ActionResult<CommentDto>> AddComment([FromBody] CreateCommentDto request)
    {
      
        Comment comment = Comment.Create(request.Content, request.PostId, request.UserId);
        Comment created = await commentRepo.AddCommentAsync(comment);
        CommentDto dto = new()
        {
            Id = created.CommentId,
            Content = created.Body,
            PostId = created.PostId,
            UserId = created.UserId
        };
        return Created($"/Comments/{dto.Id}", dto);
    }
    
    // PUT: /Comments/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentDto request)
    {
        try
        {
            Comment commentToUpdate = await commentRepo.GetSingleAsync(id);
            commentToUpdate.Body = request.Content;
            
            await commentRepo.UpdateCommentAsync(commentToUpdate);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Comment with ID {id} not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }
    
    // DELETE: /Comments/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteComment([FromRoute] int id)
    {
        try
        {
            await commentRepo.DeleteCommentAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Comment with ID {id} not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }
    
    // GET: /Comments/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetSingleComment([FromRoute] int id)
    {
        try
        {
            Comment comment = await commentRepo.GetSingleAsync(id);
            CommentDto dto = new()
            {
                Id = comment.CommentId,
                Content = comment.Body,
                PostId = comment.PostId,
                UserId = comment.UserId
            };
            return Ok(dto);
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Comment with ID {id} not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }
    
    // GET: /Comments
    [HttpGet]
<<<<<<< HEAD
    public async  Task<ActionResult<IEnumerable<CommentDto>>> GetAllComments()
    {
        IEnumerable<Comment> comments = await commentRepo.GetMany().ToListAsync();
        List<CommentDto> dtos = comments.Select(c => new CommentDto
        {
            Id = c.CommentId,
            Content = c.Body,
            PostId = c.PostId,
            UserId = c.UserId
        }).ToList();
        return Ok(dtos);
    }
=======
    public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments([FromQuery] string? commentContentContains = null)
    {
        IList<Comment> comments = await commentRepo.GetMany()
            .Where(
                c => commentContentContains == null || c.Body.Contains(commentContentContains)
            ).ToListAsync();
        return Ok(comments);
    }
    
    // public Task<ActionResult<IEnumerable<CommentDto>>> GetAllComments()
    // {
    //     IEnumerable<Comment> comments =  commentRepo.GetMany();
    //     List<CommentDto> dtos = comments.Select(c => new CommentDto
    //     {
    //         Id = c.CommentId,
    //         Content = c.Body,
    //         PostId = c.PostId,
    //         UserId = c.UserId
    //     }).ToList();
    //     return Task.FromResult<ActionResult<IEnumerable<CommentDto>>>(Ok(dtos));
    // }
    

>>>>>>> 9b41c88e1eac0d347d5f99f743020e678ac6355c
}