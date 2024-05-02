using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerHackathon.DomainModel;
using ServerHackathon.Core.Enums;

namespace ServerHackathon.DataAccess.Configurations;

public class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        builder.HasKey(g => g.Id);

        var genders = Enum
            .GetValues<GenderEnum>()
            .Select(g => new Gender
            {
                Id = (int)g,
                Name = g.ToString()
            });

        builder.HasData(genders);
    }
}