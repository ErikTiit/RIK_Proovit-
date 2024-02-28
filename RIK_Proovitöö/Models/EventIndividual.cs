namespace RIK_Proovitöö.Models
{
    public class EventIndividual
    {
        public int EventID { get; set; }
        public int IndividualID { get; set; }

        public Event? Event { get; set; }
        public Individual? Individual { get; set; }
    }
}
