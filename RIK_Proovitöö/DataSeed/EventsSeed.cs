using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;


public static class EventSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var events = new List<Event>();
        var random = new Random();

        for (int i = 1; i <= 50; i++)
        {
            events.Add(new Event
            {
                ID = i,
                Name = $"Event{i}",
                Date = DateTime.Now.AddDays(random.Next(-365, 365)),
                Location = $"Location{i}"
            });
        }

        modelBuilder.Entity<Event>().HasData(events);
    }
}
