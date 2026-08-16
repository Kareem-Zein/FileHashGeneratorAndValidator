namespace FileHashGeneratorAndValidator.Models
{
    public sealed record HashOperationResult(bool IsSuccess, string Title, string Message, string HashData);
}
