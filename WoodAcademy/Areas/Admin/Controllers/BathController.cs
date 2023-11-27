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
    [Authorize(Roles ="Admin")]
    public class BathController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BathController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<IActionResult> Index()
        {
            ICollection<BathImages> bath = await _context.BathImages.OrderByDescending(s => s.Id)
                .ToListAsync()
                ;
            return View(bath);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatedBathVM bathVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!bathVM.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", $"{bathVM.Photo.FileName} type must be image type");
                return View();
            }
            //if (!brands.Photo.CheckFileSize(400))
            //{
            //    ModelState.AddModelError("Photo", $"{brands.Photo.FileName} file type must be size less than 200kb");
            //    return View();
            //}
            string root = Path.Combine
                (_webHostEnvironment.WebRootPath, "assets", "imgs", "Bath");

            string fileName = await bathVM.Photo.SaveAsync(root);



            BathImages bath = new BathImages()
            {

                ImagePath = fileName

            };
            await _context.BathImages.AddAsync(bath);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int id)
        {
            BathImages? bath = _context.BathImages.Find(id);

            if (bath == null)
            {
                return NotFound();
            }

            return View(bath);
        }


        public IActionResult Update(BathImages bed)
        {
            BathImages? editedBath = _context.BathImages.Find(bed.Id);
            if (editedBath == null)
            {
                return NotFound();
            }

            _context.BathImages.Update(editedBath);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int Id)
        {
            BathImages? bath = _context.BathImages.Find(Id);
            if (bath == null)
            {
                return NotFound();
            }
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs", "Bath", bath.ImagePath);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Exists(imagePath);
            }
            _context.BathImages.Remove(bath);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}

