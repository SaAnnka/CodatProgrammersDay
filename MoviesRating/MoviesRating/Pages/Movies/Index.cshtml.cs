using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoviesRating.Data;
using MoviesRating.Models;

namespace MoviesRating.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly MoviesRating.Data.MoviesRatingContext _context;

        public IndexModel(MoviesRating.Data.MoviesRatingContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        public async Task OnGetAsync()
        {
            Movie = await _context.Movie.ToListAsync();
        }
    }
}
