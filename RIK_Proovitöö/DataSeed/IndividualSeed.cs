using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;


public static class IndividualSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var individuals = new List<Individual>();
        var random = new Random();

        for (int i = 1; i <= 10; i++)
        {
            var buffer = new byte[8];
            random.NextBytes(buffer);
            long longRand = BitConverter.ToInt64(buffer, 0);
            long PersonalCode = Math.Abs(longRand % (99999999999 - 10000000000) + 10000000000);

            individuals.Add(new Individual
            {
                ID = i,
                FirstName = $"First{i}",
                LastName = $"Last{i}",
                PersonalCode = PersonalCode,
                PaymentType = (PaymentType)random.Next(0, 2),
                ExtraInfo = $"Test extra info {i}"
            });
        }

        modelBuilder.Entity<Individual>().HasData(individuals);

    }
}

