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
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webhost;
        public SliderController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webhost = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();

            return View(sliders);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider Slider = await _context.Sliders.FindAsync(id);
            if (Slider == null)
            {
                return NotFound();
            }
            _context.Sliders.Remove(Slider);
            Helper.DeleteImage(_webhost, "img", Slider.ImageUrl);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider Slider)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Slider.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }

            string filename = await Slider.Photo.SaveImage(_webhost, "img");
            Slider db = new Slider();
            db.ImageUrl = filename;
            db.Title = Slider.Title;
            db.Subtitle = Slider.Subtitle;
            db.Desc = Slider.Desc;
            await _context.Sliders.AddAsync(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider Slider = await _context.Sliders.FindAsync(id);
            if (Slider == null) return NotFound();
            return View(Slider);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Slider Slider, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!Slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Ancaq sekil sece bilersiniz");
            }
            if (Slider.Photo.CheckSize(8000))
            {
                ModelState.AddModelError("Photo", "Sekilin olcusu 8,b ola biler");
            }
            Slider existtitle = _context.Sliders.FirstOrDefault(c => c.Title.ToLower() == Slider.Title.ToLower());
            Slider db = await _context.Sliders.FindAsync(id);
            if (existtitle != null)
            {
                if (db != existtitle)
                {
                    ModelState.AddModelError("Title", "Title Already Exist");
                    return View();
                }
            }
            if (db == null)
            {
                return NotFound();
            }

            string filename = await Slider.Photo.SaveImage(_webhost, "img");
            db.ImageUrl = filename;
            db.Title = Slider.Title;
            db.Subtitle = Slider.Subtitle;
            db.Desc = Slider.Desc;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider Slider = await _context.Sliders.FindAsync(id);
            if (Slider == null)
            {
                return NotFound();
            }
            return View(Slider);
        }
    }
}
