namespace ServerHackathon.DomainModel;

public class PlaceType
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Place> Places { get; set; } = [];
}