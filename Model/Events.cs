namespace MusicBookingApp.Model
{
    public class Events
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Venue { get; set; }
        public decimal TicketPrice { get; set; }
        public Artists Artist { get; set; }
        public int ArtistId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
