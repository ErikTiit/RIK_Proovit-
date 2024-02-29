using System.ComponentModel.DataAnnotations;


namespace RIK_Proovitöö.Models
{
    public class Event
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter the event name.")]
        [StringLength(50, ErrorMessage = "The event name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the event date.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please enter the event location.")]
        [StringLength(100, ErrorMessage = "The event location cannot exceed 100 characters.")]
        public string Location { get; set; }

        [StringLength(1000, ErrorMessage = "The extra info cannot exceed 1000 characters.")]
        public string? ExtraInfo { get; set; }

        public ICollection<EventIndividual>? EventIndividuals { get; set; }
        public ICollection<EventCompany>? EventCompanies { get; set; }
    }
}
