using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;

public static class CompanySeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var companies = new List<Company>();
        var random = new Random();

        for (int i = 1; i <= 10; i++)
        {
            var buffer = new byte[8];
            random.NextBytes(buffer);
            long longRand = BitConverter.ToInt64(buffer, 0);
            long RegistryCode = Math.Abs(longRand % (99999999999 - 10000000000) + 10000000000);

            companies.Add(new Company
            {
                ID = i,
                CompanyName = $"Company {i}",
                RegistryCode = RegistryCode,
                PaymentType = (PaymentType)random.Next(0, 2),
                ExtraInfo = $"Test extra info {i}",
                CompanyAttendeeAmount = random.Next(1, 100)
            });
        }

        modelBuilder.Entity<Company>().HasData(companies);

    }
}

