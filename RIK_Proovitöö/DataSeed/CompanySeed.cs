using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;
using System.Collections.Generic;

public static class CompanySeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var companies = new List<Company>
        {
            new Company
            {
                Id = 1,
                CompanyName = "Company 1",
                RegisteryCode = 123456,
                AttendeeAmount = 10,
                PaymentType = PaymentType.Cash,
                ExtraInfo = "Test extra info 1"
            },
            new Company
            {
                Id = 2,
                CompanyName = "Company 2",
                RegisteryCode = 654321,
                AttendeeAmount = 20,
                PaymentType = PaymentType.BankTransfer,
                ExtraInfo = "Test extra info 2"
            },
        };

        modelBuilder.Entity<Company>().HasData(companies);
    }
}
