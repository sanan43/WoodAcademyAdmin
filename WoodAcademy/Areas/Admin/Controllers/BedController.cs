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
    public class BedController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BedController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<IActionResult> Index()
        {
            ICollection<BedImages> bed = await _context.BedImages.OrderByDescending(s => s.Id)
                .ToListAsync()
                ;
            return View(bed);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBedVM bedVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!bedVM.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", $"{bedVM.Photo.FileName} type must be image type");
                return View();
            }
            //if (!brands.Photo.CheckFileSize(400))
            //{
            //    ModelState.AddModelError("Photo", $"{brands.Photo.FileName} file type must be size less than 200kb");
            //    return View();
            //}
            string root = Path.Combine
                (_webHostEnvironment.WebRootPath, "assets", "imgs", "Bed");

            string fileName = await bedVM.Photo.SaveAsync(root);



            BedImages bed = new BedImages()
            {

                ImagePath = fileName

            };
            await _context.BedImages.AddAsync(bed);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int id)
        {
            BedImages? bed = _context.BedImages.Find(id);

            if (bed == null)
            {
                return NotFound();
            }

            return View(bed);
        }


        public IActionResult Update(BedImages bed)
        {
            BedImages? editedBed = _context.BedImages.Find(bed.Id);
            if (editedBed == null)
            {
                return NotFound();
            }

            _context.BedImages.Update(editedBed);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int Id)
        {
            BedImages? bed = _context.BedImages.Find(Id);
            if (bed == null)
            {
                return NotFound();
            }
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs", "Bed", bed.ImagePath);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Exists(imagePath);
            }
            _context.BedImages.Remove(bed);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}

