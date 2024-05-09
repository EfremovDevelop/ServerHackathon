using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerHackathon.DomainModel;
using ServerHackathon.Core.Enums;

namespace ServerHackathon.DataAccess.Configurations;

public class PlaceTypeConfiguration : IEntityTypeConfiguration<PlaceType>
{
    public void Configure(EntityTypeBuilder<PlaceType> builder)
    {
        builder.HasKey(g => g.Id);

        var types = Enum
            .GetValues<PlaceTypeEnum>()
            .Select(g => new PlaceType
            {
                Id = (int)g,
                Name = g.ToString()
            });

        builder.HasData(types);

        builder.HasMany(u => u.Places)
            .WithMany(p => p.Types)
            .UsingEntity<PlaceTypeList>(
                r => r.HasOne<Place>().WithMany().HasForeignKey(p => p.PlaceId),
                l => l.HasOne<PlaceType>().WithMany().HasForeignKey(r => r.PlaceTypeId));
    }
}

public class PlaceConfiguration : IEntityTypeConfiguration<Place>
{
    public void Configure(EntityTypeBuilder<Place> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasMany(u => u.Types)
           .WithMany(p => p.Places)
           .UsingEntity<PlaceTypeList>(
               r => r.HasOne<PlaceType>().WithMany().HasForeignKey(p => p.PlaceTypeId),
               l => l.HasOne<Place>().WithMany().HasForeignKey(r => r.PlaceId));

        builder.HasOne(u => u.University)
            .WithMany(u => u.Places)
            .HasForeignKey(u => u.UniversityId);
    }
}