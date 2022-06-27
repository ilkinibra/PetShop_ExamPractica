using Microsoft.AspNetCore.Mvc;
using Pet_Shop.DAL;

namespace Pet_Shop.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    public class DashBoardController : Controller
    {
        private readonly AppDbContext _context;
        public DashBoardController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
