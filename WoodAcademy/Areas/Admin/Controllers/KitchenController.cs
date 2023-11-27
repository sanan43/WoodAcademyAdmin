using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WoodAcademy.Areas.Admin.ViewModels;
using WoodAcademy.DAL;
using WoodAcademy.Models;
using WoodAcademy.Utilities.Extensions;
using WoodAcademy.ViewModel;

namespace WoodAcademy.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class KitchenController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public KitchenController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<IActionResult> Index()
        {
            
            ICollection<KitchenImages> kitchen = await _context.KitchenImages
                .OrderByDescending(s => s.Id)
                .ToListAsync()
                ;
            return View(kitchen);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateKitchenVM ktcVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!ktcVM.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", $"{ktcVM.Photo.FileName} type must be image type");
                return View();
            }
            
            string root = Path.Combine
                (_webHostEnvironment.WebRootPath, "assets", "imgs","Kitchen");

            string fileName = await ktcVM.Photo.SaveAsync(root);



            KitchenImages ktc = new KitchenImages()
            {
                
                ImagePath = fileName

            };
            await _context.KitchenImages.AddAsync(ktc);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            KitchenImages? ktc = _context.KitchenImages.Find(id);

            if (ktc == null)
            {
                return NotFound();
            }

            return View(ktc);
        }

        [HttpPost]
        public IActionResult Update(KitchenImages ktc)
        {
            KitchenImages? editedKitchen = _context.KitchenImages.Find(ktc.Id);
            if (editedKitchen == null)
            {
                return NotFound();
            }

            _context.KitchenImages.Update(editedKitchen);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int Id)
        {
            KitchenImages? ktc = _context.KitchenImages.Find(Id);
            if (ktc == null)
            {
                return NotFound();
            }
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs","Kitchen", ktc.ImagePath);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Exists(imagePath);
            }
            _context.KitchenImages.Remove(ktc);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}

