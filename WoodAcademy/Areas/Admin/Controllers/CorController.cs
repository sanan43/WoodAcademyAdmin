using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WoodAcademy.Areas.Admin.ViewModels;
using WoodAcademy.DAL;
using WoodAcademy.Models;
using WoodAcademy.Utilities.Extensions;

namespace WoodAcademy.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CorController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<IActionResult> Index()
        {
            ICollection<CorridorImages> cor = await _context.CorImages.OrderByDescending(s => s.Id)
                .ToListAsync()
                ;
            return View(cor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCorVM corVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!corVM.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", $"{corVM.Photo.FileName} type must be image type");
                return View();
            }
            //if (!brands.Photo.CheckFileSize(400))
            //{
            //    ModelState.AddModelError("Photo", $"{brands.Photo.FileName} file type must be size less than 200kb");
            //    return View();
            //}
            string root = Path.Combine
                (_webHostEnvironment.WebRootPath, "assets", "imgs", "Cor");

            string fileName = await corVM.Photo.SaveAsync(root);



            CorridorImages cor = new CorridorImages()
            {

                ImagePath = fileName

            };
            await _context.CorImages.AddAsync(cor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int id)
        {
            CorridorImages? cor = _context.CorImages.Find(id);

            if (cor == null)
            {
                return NotFound();
            }

            return View(cor);
        }


        public IActionResult Update(CorridorImages cor)
        {
            CorridorImages? editedCor = _context.CorImages.Find(cor.Id);
            if (editedCor == null)
            {
                return NotFound();
            }

            _context.CorImages.Update(editedCor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int Id)
        {
            CorridorImages? cor = _context.CorImages.Find(Id);
            if (cor == null)
            {
                return NotFound();
            }
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs", "Cor", cor.ImagePath);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Exists(imagePath);
            }
            _context.CorImages.Remove(cor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}

