using System.ComponentModel.DataAnnotations;

namespace RIK_Proovitöö.Models
{


    public class Company
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int RegisteryCode { get; set; }
        public int AttendeeAmount { get; set; }
        public PaymentType PaymentType { get; set; }

        [MaxLength(5000)]
        public string ExtraInfo { get; set; }

        public ICollection<EventAttendee> EventAttendees { get; set; }
    }
}
