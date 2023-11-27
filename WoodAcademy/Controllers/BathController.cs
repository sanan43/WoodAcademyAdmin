using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodAcademy.DAL;
using WoodAcademy.ViewModel;

namespace WoodAcademy.Controllers
{
    public class BathController : Controller
    {
        private readonly AppDbContext _context;

        public BathController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            BathVM bathVM = new BathVM()
            {
                images = await _context.BathImages
                .OrderByDescending(s => s.Id)
                .ToListAsync(),

            };

            return View(bathVM);
        }
    }
}
