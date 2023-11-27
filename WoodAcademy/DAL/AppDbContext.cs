using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using WoodAcademy.Models;
using WoodAcademy.Models.Auth;

namespace WoodAcademy.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        public DbSet<Brend> Brends { get; set; }
        public DbSet<KitchenImages> KitchenImages { get; set; }
        public DbSet<BedImages> BedImages { get; set; }
        public DbSet<CorridorImages> CorImages { get; set; }
        public DbSet<CorridorImages> CorridorImages { get; set; }
        public DbSet<DoorImages> DoorImages { get; set; }
        public DbSet<TvImages> TvImages { get; set; }
        public DbSet<BathImages> BathImages { get; set; }


    }
}
