using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerHackathon.DomainModel;
using ServerHackathon.Core.Enums;

namespace ServerHackathon.DataAccess.Configurations;

public class EventStatusConfiguration : IEntityTypeConfiguration<EventStatus>
{
    public void Configure(EntityTypeBuilder<EventStatus> builder)
    {
        builder.HasKey(g => g.Id);

        var statuses = Enum
            .GetValues<EventStatusEnum>()
            .Select(g => new EventStatus
            {
                Id = (int)g,
                Name = g.ToString()
            });

        builder.HasData(statuses);
    }
}