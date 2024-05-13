namespace ServerHackathon.API.Contracts.EventComment
{
    public record EventCommentResponse(string Text, DateTime createdAt, string Login);
}