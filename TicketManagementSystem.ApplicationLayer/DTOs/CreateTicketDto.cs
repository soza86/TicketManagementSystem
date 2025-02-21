namespace TicketManagementSystem.ApplicationLayer.DTOs
{
    public class CreateTicketDto
    {
        public long Number { get; set; }

        public DateTime FlightDate { get; set; }

        public string? DepartureCity { get; set; }

        public string? DepartureCountry { get; set; }

        public string? ArrivalCity { get; set; }

        public string? ArrivalCountry { get; set; }
    }
}
