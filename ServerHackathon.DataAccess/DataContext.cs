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
        public virtual DbSet<University> University { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<EventParticipant> EventParticipant { get; set; }
        public virtual DbSet<EventStatus> EventStatus { get; set; }
        public virtual DbSet<PlaceType> PlaceType { get; set; }
        public virtual DbSet<PlaceTypeList> PlaceTypeList { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new EventStatusConfiguration());
            modelBuilder.ApplyConfiguration(new PlaceTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }