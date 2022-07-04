using TarifWeb.Data;
using TarifWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TarifWeb.Areas.Users.Controllers
{
    [Area("Users")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public IActionResult CategoryDetails(int? id)
        {
            var yemekler = _context.Yemek.Where(x => x.KategoriId == id).ToList();
            ViewBag.KategoriId = id;
            return View(yemekler);
        }
        public IActionResult Index()
        {
            var yemekListesi = _context.Yemek.ToList();
            return View(yemekListesi);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yemek = await _context.Yemek
                .Include(m => m.Kategori)
                .FirstOrDefaultAsync(m => m.YemekId == id);
            if (yemek == null)
            {
                return NotFound();
            }

            return View(yemek);
        }
              

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
