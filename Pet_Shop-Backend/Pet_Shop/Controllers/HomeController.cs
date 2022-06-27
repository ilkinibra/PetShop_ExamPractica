using Microsoft.AspNetCore.Mvc;
using Pet_Shop.DAL;
using Pet_Shop.ViewModels;
using System.Linq;

namespace Pet_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Slider = _context.Sliders.ToList();
            homeVM.Team = _context.Teams.ToList();
            homeVM.Bio = _context.Bios.ToList();

            return View(homeVM);
        }
    }
}
