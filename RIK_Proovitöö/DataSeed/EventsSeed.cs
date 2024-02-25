using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;
using System;
using System.Collections.Generic;

public static class EventSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var events = new List<Event>
        {
            new Event
            {
                Id = 1,
                Name = "Event 1",
                Date = new DateTime(2022, 1, 1),
                Location = "Location 1",
                ExtraInfo = "Test extra info 1"
            },
            new Event
            {
                Id = 2,
                Name = "Event 2",
                Date = new DateTime(2022, 2, 2),
                Location = "Location 2",
                ExtraInfo = "Test extra info 2"
            },

        };

        modelBuilder.Entity<Event>().HasData(events);
    }
}
