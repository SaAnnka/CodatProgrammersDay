using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodatFoodWebSite.Models;

namespace CodatFoodWebSite.Pages.Plants
{
    public class DetailsModel : PageModel
    {
        private readonly CodatFoodWebSite.Data.ApplicationDbContext _context;

        public DetailsModel(CodatFoodWebSite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Plant Plant { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Plant = await _context.Plant.FirstOrDefaultAsync(m => m.Id == id);

            if (Plant == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
