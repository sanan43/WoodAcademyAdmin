using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Web.Helpers;
using WoodAcademy.DAL;
using WoodAcademy.Models;
using WoodAcademy.ViewModel;

namespace WoodAcademy.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                brends = await _context.Brends
                .OrderByDescending(s => s.Id)
                .Where(s => !s.IsDeleted)
                .ToListAsync(),

            };

            return View(homeVM);
        }

        public IActionResult Send()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult Send(Email mailim)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add("wood.academyy@gmail.com");
            mailMessage.From = new MailAddress("woodacademy35@gmail.com");
            mailMessage.Subject = "mesajin var";
            mailMessage.Body = "Ad:" + " " + mailim.ad + " " + "Email:" + " " + mailim.mail + " " + "Mesaj:" + " " + mailim.mesaj;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("woodacademy35@gmail.com", "fbdo ppge yavh qxof");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;


            smtp.Send(mailMessage);

            return RedirectToAction(nameof(Index));
        }

    }
}