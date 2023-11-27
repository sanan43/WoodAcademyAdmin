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
    public class BrandController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BrandController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<IActionResult> Index()
        {
            ICollection<Brend> brands = await _context.Brends.OrderByDescending(s => s.Id)
                .ToListAsync()
                ;
            return View(brands);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBrandVM brands)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!brands.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", $"{brands.Photo.FileName} type must be image type");
                return View();
            }
            //if (!brands.Photo.CheckFileSize(400))
            //{
            //    ModelState.AddModelError("Photo", $"{brands.Photo.FileName} file type must be size less than 200kb");
            //    return View();
            //}
            string root = Path.Combine
                (_webHostEnvironment.WebRootPath, "assets", "imgs");

            string fileName = await brands.Photo.SaveAsync(root);



            Brend brend = new Brend()
            {
                Name = brands.BrandName,
                ImagePath = fileName

            };
            await _context.Brends.AddAsync(brend);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int id)
        {
            Brend? brand = _context.Brends.Find(id);

            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        [HttpPost]
        public IActionResult Update(Brend brands)
        {
            Brend? editedBrands = _context.Brends.Find(brands.Id);
            if (editedBrands == null)
            {
                return NotFound();
            }
            editedBrands.Name = brands.Name;
            _context.Brends.Update(editedBrands);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int Id)
        {
            Brend? brands = _context.Brends.Find(Id);
            if (brands == null)
            {
                return NotFound();
            }
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs", brands.ImagePath);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Exists(imagePath);
            }
            _context.Brends.Remove(brands);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}

