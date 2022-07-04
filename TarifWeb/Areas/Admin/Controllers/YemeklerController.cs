using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TarifWeb.Data;
using TarifWeb.Models;

namespace TarifWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Diger.Rol_Admin)]
    public class YemeklerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _he;
        public YemeklerController(ApplicationDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Yemek.Include(y => y.Kategori);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yemek = await _context.Yemek
                .Include(y => y.Kategori)
                .FirstOrDefaultAsync(y => y.YemekId == id);
            if (yemek == null)
            {
                return NotFound();
            }

            return View(yemek);
        }

        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriAd");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Yemek yemek)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(_he.WebRootPath, @"images\yemekler");
                    var ext = Path.GetExtension(files[0].FileName);
                    if (yemek.YemekFotografi != null)
                    {
                        var imagePath = Path.Combine(_he.WebRootPath, yemek.YemekFotografi.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    yemek.YemekFotografi = @"\images\yemekler\" + fileName + ext;
                }
                _context.Add(yemek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yemek);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yemek = await _context.Yemek.FindAsync(id);
            if (yemek == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriAd", yemek.KategoriId);
            return View(yemek);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("YemekId,YemekAd,YemekAciklamasi,YemekFotografi,KategoriId")] Yemek yemek)
        {
            if (id != yemek.YemekId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yemek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YemekExists(yemek.YemekId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "KategoriId", "KategoriAd", yemek.KategoriId);
            return View(yemek);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yemek = await _context.Yemek
                .Include(y => y.Kategori)
                .FirstOrDefaultAsync(y => y.YemekId == id);
            if (yemek == null)
            {
                return NotFound();
            }

            return View(yemek);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yemek = await _context.Yemek.FindAsync(id);
            _context.Yemek.Remove(yemek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YemekExists(int id)
        {
            return _context.Yemek.Any(e => e.YemekId == id);
        }
    }
}
