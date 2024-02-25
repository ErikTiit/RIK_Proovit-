using System.ComponentModel.DataAnnotations;

namespace RIK_Proovitöö.Models
{
    public enum PaymentType
    {
        Cash,
        BankTransfer
    }

    public class Individual
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PersonalCode { get; set; }
        public PaymentType PaymentType { get; set; }

        [MaxLength(1500)]
        public string ExtraInfo { get; set; }

        public ICollection<EventAttendee> EventAttendees { get; set; }
    }
}
