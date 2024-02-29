using System.ComponentModel.DataAnnotations;


namespace RIK_Proovitöö.Models
{

        public class Company
        {
            public int ID { get; set; }

            [Required(ErrorMessage = "Please enter the company name.")]
            [StringLength(100, ErrorMessage = "The company name cannot exceed 50 characters.")]
            public string CompanyName { get; set; }

            [Required(ErrorMessage = "Please enter the registry code.")]
            [Range(100000000000, 999999999999, ErrorMessage = "The registry code must be 12 digits.")]
            public long RegistryCode { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "The field must contain at least 1 attendee.")]
            public int CompanyAttendeeAmount { get; set; }


            [Required(ErrorMessage = "Please select a payment type.")]
            public PaymentType PaymentType { get; set; }

            [MaxLength(5000, ErrorMessage = "Extra info cannot exceed 5000 characters.")]
            public string ExtraInfo { get; set; }

            public ICollection<EventCompany>? EventCompanies { get; set; }
        }
}
