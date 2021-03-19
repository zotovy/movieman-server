using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Database.Movie {
    public class MovieRepository: IMovieRepository {
        private readonly DatabaseContext _context;

        public MovieRepository(DatabaseContext context) {
            _context = context;
        }

        public void SaveChanges() => _context.SaveChanges();

        public void UpdatePopularMovies(IEnumerable<Domain.Movie> movies) {
            
            // Remove old popular movies
            _context.LinkToPopularMovieModels.RemoveRange(_context.LinkToPopularMovieModels.ToList());

            // Go throw movies and checked is this movie exists in db
            foreach (var movie in movies) {
                var founded = _context.Movies.FirstOrDefault(m => m.KpId == movie.KpId);
                
                // Create a MovieModel to work with db later
                var model = new MovieModel(movie);
                
                if (founded == null) {
                    // Add this movie to db context
                    _context.Movies.Add(model);
                }
                
                SaveChanges();

                // Extract Id and save this Id as a link to LinkToPopularMovieModel collection
                var id = model.Id;
                _context.LinkToPopularMovieModels.Add(new LinkToPopularMovieModel(id));
            }
        }
    }
}