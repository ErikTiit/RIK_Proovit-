using System.ComponentModel.DataAnnotations;

namespace RIK_Proovitöö.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }

        [MaxLength(1000)]
        public string ExtraInfo { get; set; }

        public ICollection<EventAttendee> EventAttendees { get; set; }

    }
}