﻿namespace ServerHackathon.DomainModel;

public class Place
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
    public int minuteStep { get; set; } = 60;

    public int UniversityId { get; set; }

    public virtual University University { get; set; }

    public virtual ICollection<PlaceType> Types { get; set; } = [];

    public virtual ICollection<Event> Events { get; set; } = [];

    public virtual ICollection<Booking> Bookings { get; set; } = [];
}