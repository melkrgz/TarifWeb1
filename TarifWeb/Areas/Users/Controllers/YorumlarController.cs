using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TarifWeb.Data;
using TarifWeb.Models;

namespace TarifWeb.Areas.Users.Controllers
{
    [Area("Users")]
    
    public class YorumlarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YorumlarController(ApplicationDbContext context)
        {
            _context = context;
        }
  
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Yorum.Include(c => c.Yemek);
            return View(await applicationDbContext.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var comment = await _context.Yorum
                .Include(c => c.Yemek)
                .FirstOrDefaultAsync(m => m.YorumId == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        
        public IActionResult Create()
        {
            ViewData["YemekId"] = new SelectList(_context.Yemek, "YemekId", "YemekAd");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("YorumId,YorumBasligi,YorumAciklama,YemekDerecesi,YemekId")] Yorum yorum)
        {
            if (ModelState.IsValid)
            {               
                _context.Add(yorum);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["YemekId"] = new SelectList(_context.Yemek, "YemekId", "YorumAciklama", yorum.YemekId);
            return View(yorum);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yorum = await _context.Yorum.FindAsync(id);
            if (yorum == null)
            {
                return NotFound();
            }
            ViewData["YemekID"] = new SelectList(_context.Yemek, "YemekId", "YemekAd", yorum.YemekId);
            return View(yorum);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("YorumId,YorumBasligi,YorumAciklama,YemekDerecesi,YemekId")] Yorum yorum)
        {
            if (id != yorum.YorumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yorum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YorumExists(yorum.YorumId))
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
            ViewData["YemekId"] = new SelectList(_context.Yemek, "YemekId", "YemekAciklama", yorum.YemekId);
            return View(yorum);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yorum = await _context.Yorum
                .Include(c => c.Yemek)
                .FirstOrDefaultAsync(m => m.YorumId == id);
            if (yorum == null)
            {
                return NotFound();
            }

            return View(yorum);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yorum = await _context.Yorum.FindAsync(id);
            _context.Yorum.Remove(yorum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YorumExists(int id)
        {
            return _context.Yorum.Any(e => e.YorumId == id);
        }
    }
}
