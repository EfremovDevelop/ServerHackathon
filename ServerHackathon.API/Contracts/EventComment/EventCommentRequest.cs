namespace ServerHackathon.API.Contracts.EventComment
{
    public record EventCommentRequest(Guid eventId, string Text);
}