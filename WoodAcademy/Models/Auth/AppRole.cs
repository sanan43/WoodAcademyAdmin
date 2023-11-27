using Microsoft.AspNetCore.Identity;

namespace WoodAcademy.Models.Auth
{
    public class AppRole:IdentityRole
    {
        public bool IsActivated { get; set; } 
    }
}
