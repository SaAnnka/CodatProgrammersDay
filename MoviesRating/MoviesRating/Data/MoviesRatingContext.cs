using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesRating.Models;

namespace MoviesRating.Data
{
    public class MoviesRatingContext : DbContext
    {
        public MoviesRatingContext (DbContextOptions<MoviesRatingContext> options)
            : base(options)
        {
        }

        public DbSet<MoviesRating.Models.Movie> Movie { get; set; }
    }
}
