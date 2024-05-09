using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerHackathon.DomainModel;

namespace ServerHackathon.DataAccess.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(i => i.Id);

        builder.HasMany(u => u.Users)
            .WithMany(p => p.Events)
            .UsingEntity<EventParticipant>(
                r => r.HasOne<User>().WithMany().HasForeignKey(p => p.UserId),
                l => l.HasOne<Event>().WithMany().HasForeignKey(r => r.EventId));

        builder.HasOne(u => u.Place)
            .WithMany(u => u.Events)
            .HasForeignKey(u => u.PlaceId);

        builder.HasOne(e => e.Status)
            .WithMany(s => s.Events)
            .HasForeignKey(e => e.StatusId);
    }
}