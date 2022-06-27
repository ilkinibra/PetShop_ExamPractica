using Microsoft.AspNetCore.Mvc;
using Pet_Shop.DAL;
using Pet_Shop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pet_Shop.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    public class BioController : Controller
    {
        private readonly AppDbContext _context;

        public BioController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Bio> bios = _context.Bios.ToList();

            return View(bios);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Bio Bio = await _context.Bios.FindAsync(id);
            if (Bio == null)
            {
                return NotFound();
            }
            _context.Bios.Remove(Bio); 
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bio Bio)
        {
             
            Bio db = new Bio(); 
            db.Facebook = Bio.Facebook;
            db.Phone = Bio.Phone;
            db.Insta = Bio.Insta;
            db.Location = Bio.Location;
            db.Location = Bio.Location;
            db.Twitter = Bio.Twitter;
            db.Linkedin = Bio.Linkedin;
            await _context.Bios.AddAsync(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Bio Bio = await _context.Bios.FindAsync(id);
            if (Bio == null) return NotFound();
            return View(Bio);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Bio Bio, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            Bio existtitle = _context.Bios.FirstOrDefault(c => c.Phone.ToLower() == Bio.Phone.ToLower());
            Bio db = await _context.Bios.FindAsync(id);
            if (existtitle != null)
            {
                if (db != existtitle)
                {
                    ModelState.AddModelError("Phone", "Phone Already Exist");
                    return View();
                }
            }
            if (db == null)
            {
                return NotFound();
            } 
            db.Facebook = Bio.Facebook;
            db.Phone = Bio.Phone;
            db.Insta = Bio.Insta;
            db.Location = Bio.Location;
            db.Location = Bio.Location;
            db.Twitter = Bio.Twitter;
            db.Linkedin = Bio.Linkedin;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Bio Bio = await _context.Bios.FindAsync(id);
            if (Bio == null)
            {
                return NotFound();
            }
            return View(Bio);
        }
    }
}
