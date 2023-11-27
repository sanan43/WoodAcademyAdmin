using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodAcademy.DAL;
using WoodAcademy.ViewModel;

namespace WoodAcademy.Controllers
{
    public class KitchenController : Controller
    {
        private readonly AppDbContext _context;

        public KitchenController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            KitchenVM kitchenVM = new KitchenVM()
            {
                images = await _context.KitchenImages
                .OrderByDescending(s => s.Id)
                .ToListAsync(),

            };

            return View(kitchenVM);
        }
    }
}
