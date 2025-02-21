namespace TicketManagementSystem.Core.Entities
{
    public class Ticket : BaseEntity
    {
        public long Number { get; set; }

        public DateTime FlightDate { get; set; }

        public string? DepartureCity { get; set; }

        public string? DepartureCountry { get; set; }

        public string? ArrivalCity { get; set; }

        public string? ArrivalCountry { get; set; }
    }
}
