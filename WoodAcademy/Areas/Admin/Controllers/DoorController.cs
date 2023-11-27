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
    public class DoorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DoorController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public async Task<IActionResult> Index()
        {
            ICollection<DoorImages> cor = await _context.DoorImages.OrderByDescending(s => s.Id)
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
        public async Task<IActionResult> Create(CreateDoorVM doorVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!doorVM.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", $"{doorVM.Photo.FileName} type must be image type");
                return View();
            }
            //if (!brands.Photo.CheckFileSize(400))
            //{
            //    ModelState.AddModelError("Photo", $"{brands.Photo.FileName} file type must be size less than 200kb");
            //    return View();
            //}
            string root = Path.Combine
                (_webHostEnvironment.WebRootPath, "assets", "imgs", "Door");

            string fileName = await doorVM.Photo.SaveAsync(root);



            DoorImages door = new DoorImages()
            {

                ImagePath = fileName

            };
            await _context.DoorImages.AddAsync(door);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int id)
        {
            DoorImages? door = _context.DoorImages.Find(id);

            if (door == null)
            {
                return NotFound();
            }

            return View(door);
        }


        public IActionResult Update(DoorImages door)
        {
            DoorImages? editedDoor = _context.DoorImages.Find(door.Id);
            if (editedDoor == null)
            {
                return NotFound();
            }

            _context.DoorImages.Update(editedDoor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int Id)
        {
            DoorImages? door = _context.DoorImages.Find(Id);
            if (door == null)
            {
                return NotFound();
            }
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "imgs", "Door", door.ImagePath);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Exists(imagePath);
            }
            _context.DoorImages.Remove(door);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}

