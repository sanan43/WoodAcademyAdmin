using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodAcademy.DAL;
using WoodAcademy.ViewModel;

namespace WoodAcademy.Controllers
{
    public class TvController : Controller
    {
        private readonly AppDbContext _context;

        public TvController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            TvVM tvVM = new TvVM()
            {
                images = await _context.TvImages
                .OrderByDescending(s => s.Id)
                .ToListAsync(),

            };

            return View(tvVM);
        }
    }
}
