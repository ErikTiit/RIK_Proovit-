using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;


public static class IndividualSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var individuals = new List<Individual>();
        var random = new Random();

        for (int i = 1; i <= 50; i++)
        {
            individuals.Add(new Individual
            {
                ID = i,
                FirstName = $"First{i}",
                LastName = $"Last{i}",
                PersonalCode = random.Next(100000, 999999),
                PaymentType = (PaymentType)random.Next(0, 2),
                ExtraInfo = $"Test extra info {i}"
            });
        }

        modelBuilder.Entity<Individual>().HasData(individuals);
    }
}
