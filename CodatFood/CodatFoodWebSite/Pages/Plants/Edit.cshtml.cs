using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CodatFoodWebSite.Models;

namespace CodatFoodWebSite.Pages.Plants
{
    public class EditModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public EditModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Plant Plant { get; set; }

        public List<Category> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Plant = await _context.Plant.FirstOrDefaultAsync(m => m.Id == id);
            Categories = await _context.Category.ToListAsync();

            if (Plant == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Plant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantExists(Plant.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PlantExists(int id)
        {
            return _context.Plant.Any(e => e.Id == id);
        }
    }
}
