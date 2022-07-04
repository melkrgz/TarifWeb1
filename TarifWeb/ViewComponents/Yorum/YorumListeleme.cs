using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarifWeb.Data;

namespace TarifWeb.ViewComponents.Yorum
{
    public class YorumListeleme : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public YorumListeleme(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
  
            var yorum = _context.Yorum.Include(x => x.Yemek).ToList();
            return View(yorum);
        }
    }
}
