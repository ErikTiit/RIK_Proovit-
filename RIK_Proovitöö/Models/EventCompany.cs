namespace RIK_Proovitöö.Models
{
    public class EventCompany
    {
        public int EventID { get; set; }
        public int CompanyID { get; set; }

        public Event? Event { get; set; }
        public Company? Company { get; set; }
    }
}