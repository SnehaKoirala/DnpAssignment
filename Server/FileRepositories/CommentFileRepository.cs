using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<Comment> AddCommentAsync(Comment comment)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;
        int maxId = comments.Count > 0 ? comments.Max(c => c.CommentId) : 1;
        comment.CommentId = maxId + 1;
        comments.Add(comment);
        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentAsJson);
        return comment;

    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;

        Comment? existingComment = comments.SingleOrDefault(c => c.CommentId == comment.CommentId);

        if (existingComment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{comment.CommentId}' not found");
        }

        existingComment.Body = comment.Body;
        existingComment.PostId = comment.PostId;
        existingComment.UserId = comment.UserId;

        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentAsJson);

    }

    public async Task DeleteCommentAsync(int id)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;

        Comment? commentToRemove = comments.SingleOrDefault(c => c.CommentId == id);

        if (commentToRemove is null)
        {
            throw new InvalidOperationException($"Comment with ID'{id}' not found");
        }

        comments.Remove(commentToRemove);

        commentAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentAsJson);
    }

    public async Task<Comment> GetSingleAsync(int commentId)
    {
        string commentAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;

        Comment? comment = comments.SingleOrDefault(c => c.CommentId == commentId);

        if (comment is null)
        {
            throw new InvalidOperationException($"Comment with ID '{commentId}' not found");

        }

        return comment;
    }

    public IQueryable<Comment> GetMany()
    {
        string commentAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentAsJson)!;
        return comments.AsQueryable();

    }
}