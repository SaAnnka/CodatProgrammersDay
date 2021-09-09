using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodatFoodWebSite.Models;

namespace CodatFoodWebSite.Pages.Plants
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public IndexModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Plant> Plant { get; set; }
        public IList<Category> Category { get; set; }

        public async Task OnGetAsync()
        {
            Plant = await _context.Plant.ToListAsync();
            Category = await _context.Category.ToListAsync();
        }
    }
}
