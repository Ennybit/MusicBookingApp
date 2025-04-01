using MusicBookingApp.Model;

namespace MusicBookingApp.DTOs
{
    public class CreateArtistDTO : UpdateArtistDTO
    {

        public string UserId { get; set; }
    }

    public class UpdateArtistDTO
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Genre { get; set; }
        public string ImageURL { get; set; }
    }

    public class GetArtistDTO : UpdateArtistDTO
    {

    }



}
