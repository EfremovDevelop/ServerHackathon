﻿using ServerHackathon.DomainModel;

namespace ServerHackathon.Core.DtoModels;

public class PlaceDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Adress { get; set; }

    public string Location { get; set; }

    public string? Description { get; set; }

    public int? Capacity { get; set; }

    public bool isBlocked { get; set; } = false;

    public DateTime? WorkFrom { get; set; }

    public DateTime? WorkTo { get; set;}
    
    public int minuteStep { get; set; }
    
    public UniversityDto University { get; set; }

    public ICollection<PlaceTypeDto> Types { get; set; }

    public PlaceDto() { }
    public PlaceDto(Place place)
    {
        Id = place.Id;
        Name = place.Name;
        Adress = place.Adress;
        Location = place.Location;
        Description = place.Description;
        Capacity = place.Capacity;
        isBlocked = place.isBlocked;
        WorkFrom = place.WorkFrom;
        WorkTo = place.WorkTo;
        minuteStep = place.minuteStep;
        University = new UniversityDto(place.University);
        Types = place.Types.Select(t => new PlaceTypeDto(t)).ToList();
    }
}
