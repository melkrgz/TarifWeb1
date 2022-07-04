using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarifWeb.Data;
using TarifWeb.Models;

namespace TarifWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Diger.Rol_Admin)]
    public class KullaniciController : Controller
    {
        private readonly ApplicationDbContext _context;
        public KullaniciController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var kullanicilar = _context.ApplicationUsers.ToList();
            return View(kullanicilar);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kullanici = await _context.ApplicationUsers
                .FirstOrDefaultAsync(y => y.Id == id.ToString());
            if (kullanici == null)
            {
                return NotFound();
            }

            return View(kullanici);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var kullanici = await _context.ApplicationUsers.FindAsync(id);
            _context.ApplicationUsers.Remove(kullanici);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
