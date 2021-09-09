using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodatFoodWebSite.Models;

namespace CodatFoodWebSite.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly CodatFoodWebSite.Data.ApplicationDbContext _context;

        public IndexModel(CodatFoodWebSite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Category.ToListAsync();
        }
    }
}
