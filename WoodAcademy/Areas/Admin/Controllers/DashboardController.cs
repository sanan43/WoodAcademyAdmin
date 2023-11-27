using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WoodAcademy.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        //[AllowAnonymous] //bura herkes gire biler
        public IActionResult Index()
        {
            return View();
        }
    }
}
