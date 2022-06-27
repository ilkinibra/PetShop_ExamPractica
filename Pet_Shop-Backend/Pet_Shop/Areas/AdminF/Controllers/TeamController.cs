using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Pet_Shop.DAL;
using Pet_Shop.Extensions;
using Pet_Shop.Helpers;
using Pet_Shop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public TeamController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Team> teams = _context.Teams.ToList();
            return View(teams);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Team Team = await _context.Teams.FindAsync(id);
            if (Team == null)
            {
                return NotFound();
            }
            _context.Teams.Remove(Team);
            Helper.DeleteImage(_webhost, "img", Team.ImageUrl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team Team)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Team.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Team.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }

            string filename = await Team.Photo.SaveImage(_webhost, "img");
            Team db = new Team();
            db.ImageUrl = filename;
            db.Face = Team.Face;
            db.Name = Team.Name;
            db.Insta = Team.Insta;
            db.Position = Team.Position;
            db.Twitter = Team.Twitter;
            db.Linkedin = Team.Linkedin;
            await _context.Teams.AddAsync(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Team Team = await _context.Teams.FindAsync(id);
            if (Team == null) return NotFound();
            return View(Team);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Team Team, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Team.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Team.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }
            Team existtitle = _context.Teams.FirstOrDefault(c => c.Name.ToLower() == Team.Name.ToLower());
            Team db = await _context.Teams.FindAsync(id);
            if (existtitle != null)
            {
                if (db != existtitle)
                {
                    ModelState.AddModelError("Name", "name Already Exist");
                    return View();
                }
            }
            if (db == null)
            {
                return NotFound();
            }

            string filename = await Team.Photo.SaveImage(_webhost, "img");
            db.ImageUrl = filename;
            db.Face = Team.Face;
            db.Name = Team.Name;
            db.Insta = Team.Insta;
            db.Position = Team.Position;
            db.Twitter = Team.Twitter;
            db.Linkedin = Team.Linkedin;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Team Team = await _context.Teams.FindAsync(id);
            if (Team == null)
            {
                return NotFound();
            }
            return View(Team);
        }
    }
}
