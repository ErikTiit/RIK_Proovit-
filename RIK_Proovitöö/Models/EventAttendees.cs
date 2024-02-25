namespace RIK_Proovitöö.Models
{
    public class EventAttendee
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int? IndividualId { get; set; }
        public Individual Individual { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }


        public bool IsValid => (IndividualId.HasValue != CompanyId.HasValue);
    }
}
