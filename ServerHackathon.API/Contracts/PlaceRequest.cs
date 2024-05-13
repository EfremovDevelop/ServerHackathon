namespace ServerHackathon.API.Contracts
{
    public record PlaceRequest(DateTime data);

    public record PlaceCreateRequest(string Name, string Adress, string Location, string? Description, int? Capacity,
        DateTime? WorkFrom, DateTime? WorkTo, int minuteStep, int UniversityId, List<int> TypeIds);
    public record PlaceUpdateRequest(int Id, string? Name, string? Adress, string? Location, string? Description, int? Capacity,
        DateTime? WorkFrom, DateTime? WorkTo, int? UniversityId, int? minuteStep = 60);
}