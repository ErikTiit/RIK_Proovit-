using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<EventAttendee> EventAttendees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        IndividualSeed.Seed(modelBuilder);
        CompanySeed.Seed(modelBuilder);
        EventSeed.Seed(modelBuilder);
        EventAttendeesSeed.Seed(modelBuilder);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EventAttendee>()
            .HasKey(ea => ea.Id);

        modelBuilder.Entity<EventAttendee>()
            .HasOne(ea => ea.Event)
            .WithMany(e => e.EventAttendees)
            .HasForeignKey(ea => ea.Id);

        modelBuilder.Entity<EventAttendee>()
            .HasOne(ea => ea.Individual)
            .WithMany(i => i.EventAttendees)
            .HasForeignKey(ea => ea.IndividualId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EventAttendee>()
            .HasOne(ea => ea.Company)
            .WithMany(c => c.EventAttendees)
            .HasForeignKey(ea => ea.CompanyId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }

}
