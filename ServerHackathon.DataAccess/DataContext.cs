using Microsoft.EntityFrameworkCore;

namespace ServerHackathon.DataAccess;

public class DataContext(
    DbContextOptions<DataContext> options)
    : DbContext(options)
{
    // public virtual DbSet<сущность> 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new --());
        base.OnModelCreating(modelBuilder);
    }
}
