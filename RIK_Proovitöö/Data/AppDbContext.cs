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
    public DbSet<EventCompany> EventCompanies { get; set; }

    public DbSet<EventIndividual> EventIndividuals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        EventCompaniesSeed.Seed(modelBuilder);
        EventIndividualsSeed.Seed(modelBuilder);
        CompanySeed.Seed(modelBuilder);
        IndividualSeed.Seed(modelBuilder);
        EventSeed.Seed(modelBuilder);


        modelBuilder.Entity<EventIndividual>()
            .HasKey(ei => new { ei.EventID, ei.IndividualID });

        modelBuilder.Entity<EventIndividual>()
            .HasOne(ei => ei.Event)
            .WithMany(e => e.EventIndividuals)
            .HasForeignKey(ei => ei.EventID);

        modelBuilder.Entity<EventIndividual>()
            .HasOne(ei => ei.Individual)
            .WithMany(i => i.EventIndividuals)
            .HasForeignKey(ei => ei.IndividualID);

        modelBuilder.Entity<EventCompany>()
            .HasKey(ec => new { ec.EventID, ec.CompanyID });

        modelBuilder.Entity<EventCompany>()
            .HasOne(ec => ec.Event)
            .WithMany(e => e.EventCompanies)
            .HasForeignKey(ec => ec.EventID);

        modelBuilder.Entity<EventCompany>()
            .HasOne(ec => ec.Company)
            .WithMany(c => c.EventCompanies)
            .HasForeignKey(ec => ec.CompanyID);
    }
}
