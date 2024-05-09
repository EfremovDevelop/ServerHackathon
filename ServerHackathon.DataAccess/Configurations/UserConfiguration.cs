using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerHackathon.DomainModel;

namespace ServerHackathon.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasOne(u => u.Gender)
               .WithMany(g => g.Users)
               .HasForeignKey(u => u.GenderId);

        builder.HasOne(u => u.University)
            .WithMany(u => u.Users)
            .HasForeignKey(u => u.UniversityId);

        builder.HasMany(u => u.Events)
            .WithMany(p => p.Users)
            .UsingEntity<EventParticipant>(
                r => r.HasOne<Event>().WithMany().HasForeignKey(p => p.EventId),
                l => l.HasOne<User>().WithMany().HasForeignKey(r => r.UserId));
    }
}
