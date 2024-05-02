using Microsoft.EntityFrameworkCore;
using ServerHackathon.DataAccess.Configurations;
using ServerHackathon.DomainModel;

namespace ServerHackathon.DataAccess;

public class DataContext(
    DbContextOptions<DataContext> options)
    : DbContext(options)
{
    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<Gender> Gender { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new GenderConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
