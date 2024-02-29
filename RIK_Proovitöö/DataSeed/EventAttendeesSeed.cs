using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;

public static class EventCompaniesSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var eventCompanies = new List<EventCompany>();

        for (int i = 1; i <= 10; i++)
        {
            for (int j = 1; j <= 10; j++)
            {
                eventCompanies.Add(new EventCompany
                {
                    EventID = i,
                    CompanyID = j
                });
            }
        }

        modelBuilder.Entity<EventCompany>().HasData(eventCompanies);
    }
}


public static class EventIndividualsSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var eventIndividuals = new List<EventIndividual>();

        for (int i = 1; i <= 10; i++)
        {
            for (int j = 1; j <= 10; j++)
            {
                eventIndividuals.Add(new EventIndividual
                {
                    EventID = i,
                    IndividualID = j
                });
            }
        }

        modelBuilder.Entity<EventIndividual>().HasData(eventIndividuals);
    }
}



