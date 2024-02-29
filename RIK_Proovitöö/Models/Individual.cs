using System.ComponentModel.DataAnnotations;

namespace RIK_Proovitöö.Models
{
    public enum PaymentType
    {
        Sularaha,
        Pangaülekanne
    }

    public class Individual
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter the first name.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter the last name.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter the personal code.")]
        [Range(10000000000, 99999999999, ErrorMessage = "Personal code must be 11 digits.")]
        public long PersonalCode { get; set; }

        [Required(ErrorMessage = "Please select a payment type.")]
        public PaymentType PaymentType { get; set; }

        [MaxLength(1500, ErrorMessage = "Extra info cannot exceed 1500 characters.")]
        public string? ExtraInfo { get; set; }

        public ICollection<EventIndividual>? EventIndividuals { get; set; }
    }
}
