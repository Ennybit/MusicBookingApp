using Microsoft.AspNetCore.Identity;

namespace MusicBookingApp.Model
{
    public class Users : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
