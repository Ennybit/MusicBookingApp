using MusicBookingApp.Model;

namespace MusicBookingApp.DTOs
{
    public class CreateEventDTO : UpdateEventDTO
    {
        public int ArtistId { get; set; }
    }
    public class UpdateEventDTO
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public decimal TicketPrice { get; set; }
    }

    public class GetEventDTO : UpdateEventDTO
    {

    }
}
