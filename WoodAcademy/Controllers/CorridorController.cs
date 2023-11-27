using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodAcademy.DAL;
using WoodAcademy.ViewModel;

namespace WoodAcademy.Controllers
{
    public class CorridorController : Controller
    {
        private readonly AppDbContext _context;

        public CorridorController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            CorVM corVM = new CorVM()
            {
                images = await _context.CorImages
                .OrderByDescending(s => s.Id)
                .ToListAsync(),

            };

            return View(corVM);
        }
    }
}
