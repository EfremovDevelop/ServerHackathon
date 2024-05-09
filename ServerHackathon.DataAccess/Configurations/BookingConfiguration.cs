using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerHackathon.DomainModel;

namespace ServerHackathon.DataAccess.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasOne(u => u.Place)
            .WithMany(u => u.Bookings)
            .HasForeignKey(u => u.PlaceId);

        builder.HasOne(u => u.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(u => u.UserId);
    }
}