using MusicBookingApp.Model;

namespace MusicBookingApp.TokenRepository
{
    public interface ITokenRepo
    {
        string CreateJWTToken(Users user, List<string> roles);
    }
}
