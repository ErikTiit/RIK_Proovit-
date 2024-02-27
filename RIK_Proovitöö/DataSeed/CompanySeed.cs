using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;

public static class CompanySeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var companies = new List<Company>();
        var random = new Random();

        for (int i = 1; i <= 50; i++)
        {
            companies.Add(new Company
            {
                ID = i,
                CompanyName = $"Company {i}",
                RegistryCode = random.Next(100000, 999999),
                PaymentType = (PaymentType)random.Next(0, 2),
                ExtraInfo = $"Test extra info {i}"
            });
        }

        modelBuilder.Entity<Company>().HasData(companies);
    }
}
