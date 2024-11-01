using ApiContracts.Post;

namespace BlazorApp.Services;

public interface IPostService
{
     public Task<PostDto> AddAsync(CreatePostDto request);
     public Task UpdateAsync( int id, UpdatePostDto request);
}