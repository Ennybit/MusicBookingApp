namespace MusicBookingApp.Model
{
    public class Bookings
    {
        public int Id { get; set; }
        public Users user { get; set; }
        public string userId { get; set; }
        public Events Event { get; set; }
        public int EventId { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
    }
}