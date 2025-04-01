namespace MusicBookingApp.Model
{
    public class Artists
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Genre { get; set; }
        public string ImageURL { get; set; }
        public Users User { get; set;}
        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
