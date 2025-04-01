using MusicBookingApp.Model;

namespace MusicBookingApp.DTOs
{
    public class CreateBookingsDTO
    {
        public string userId { get; set; }
        public int EventId { get; set; }
    }

    public class GetBookingsDTO : CreateBookingsDTO
    {

    }
    public class GetBookingsByIDDTO : CreateBookingsDTO
    {
        public string Status { get; set; }

    }
}
