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
    }
}
