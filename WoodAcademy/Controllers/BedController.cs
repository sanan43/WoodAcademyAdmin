using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodAcademy.DAL;
using WoodAcademy.ViewModel;

namespace WoodAcademy.Controllers
{
    public class BedController : Controller
    {
        private readonly AppDbContext _context;

        public BedController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            BedVM bedVM = new BedVM()
            {
                images = await _context.BedImages
                .OrderByDescending(s => s.Id)
                .ToListAsync(),

            };

            return View(bedVM);
        }
    }
}
