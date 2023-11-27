using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using WoodAcademy.DAL;
using WoodAcademy.Models.Auth;

namespace WoodAcademy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddIdentity<AppUser, IdentityRole>()
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
                opt.Lockout.MaxFailedAccessAttempts = 3;
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}");
                endpoints.MapDefaultControllerRoute();
            });

            app.Run();
        }
    }
}