using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarifWeb.Data;

namespace TarifWeb.ViewComponents.KategoriList
{
    public class CategoryList :ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CategoryList(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var kategori = _context.Kategoriler.ToList();
            return View(kategori);
        }
    }
}
