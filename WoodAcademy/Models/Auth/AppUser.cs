using Microsoft.AspNetCore.Identity;

namespace WoodAcademy.Models.Auth
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
        public bool IsActivated { get; set; }
    }
}
