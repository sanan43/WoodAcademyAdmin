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
    public class TvController : Controller
    {
       
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TvController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<IActionResult> Index()
        {
            ICollection<TvImages> tv = await _context.TvImages.OrderByDescending(s => s.Id)
                .ToListAsync()
                ;
            return View(tv);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatedTvVM tvVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!tvVM.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", $"{tvVM.Photo.FileName} type must be image type");
                return View();
            }
            //if (!brands.Photo.CheckFileSize(400))
            //{
            //    ModelState.AddModelError("Photo", $"{brands.Photo.FileName} file type must be size less than 200kb");
            //    return View();
            //}
            string root = Path.Combine
                (_webHostEnvironment.WebRootPath, "assets", "imgs", "Tv");

            string fileName = await tvVM.Photo.SaveAsync(root);



            TvImages tv = new TvImages()
            {

                ImagePath = fileName

            };
            await _context.TvImages.AddAsync(tv);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int id)
        {
            TvImages? tv = _context.TvImages.Find(id);

            if (tv == null)
            {
                return NotFound();
            }

            return View(tv);
        }


        public IActionResult Update(TvImages tv)
        {
            TvImages? editedTv = _context.TvImages.Find(tv.Id);
            if (editedTv == null)
            {
                return NotFound();
            }

            _context.TvImages.Update(editedTv);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int Id)
        {
            TvImages? tv = _context.TvImages.Find(Id);
            if (tv == null)
            {
                return NotFound();
            }
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs", "Tv", tv.ImagePath);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Exists(imagePath);
            }
            _context.TvImages.Remove(tv);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}

