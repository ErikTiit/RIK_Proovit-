using Microsoft.EntityFrameworkCore;
using RIK_Proovitöö.Models;


public static class EventAttendeesSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var eventAttendees = new List<EventAttendee>
{
    new EventAttendee
    {
        Id = 1,
        EventId = 1,
        IndividualId = 1,
        CompanyId = null
    },
    new EventAttendee
    {
        Id = 2,
        EventId = 1,
        IndividualId = null,
        CompanyId = 1
    },
    // Add more event attendees as needed...
};

        modelBuilder.Entity<EventAttendee>().HasData(eventAttendees);
    }
}
