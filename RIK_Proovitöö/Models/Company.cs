using System.ComponentModel.DataAnnotations;


namespace RIK_Proovitöö.Models
{



        public class Company
        {
            public int ID { get; set; }
            public string CompanyName { get; set; }
            public int RegistryCode { get; set; }
            public PaymentType PaymentType { get; set; }

            [MaxLength(5000)]
            public string ExtraInfo { get; set; }

            public ICollection<EventCompany>? EventCompanies { get; set; }
        }
}
