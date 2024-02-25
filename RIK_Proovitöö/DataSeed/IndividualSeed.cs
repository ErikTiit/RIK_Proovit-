using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;
using System.Collections.Generic;

public static class IndividualSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var individuals = new List<Individual>
        {
            new Individual
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                PersonalCode = 123456,
                PaymentType = PaymentType.Cash,
                ExtraInfo = "Test extra info 1"
            },
            new Individual
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe",
                PersonalCode = 654321,
                PaymentType = PaymentType.BankTransfer,
                ExtraInfo = "Test extra info 2"
            },

        };

        modelBuilder.Entity<Individual>().HasData(individuals);
    }
}