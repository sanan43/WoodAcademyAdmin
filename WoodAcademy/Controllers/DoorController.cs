using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WoodAcademy.DAL;
using WoodAcademy.ViewModel;

namespace WoodAcademy.Controllers
{
    public class DoorController : Controller
    {
        private readonly AppDbContext _context;

        public DoorController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            DoorVM doorVM = new DoorVM()
            {
                images = await _context.DoorImages
                .OrderByDescending(s => s.Id)
                .ToListAsync(),

            };

            return View(doorVM);
        }
    }
}
