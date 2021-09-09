using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CodatFoodWebSite.Models;
using Microsoft.EntityFrameworkCore;

namespace CodatFoodWebSite.Pages.Plants
{
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public CreateModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            Categories = await _context.Category.ToListAsync();
            return Page();
        }

        [BindProperty]
        public Plant Plant { get; set; }
        public List<Category> Categories { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO Check category here

            _context.Plant.Add(Plant);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
