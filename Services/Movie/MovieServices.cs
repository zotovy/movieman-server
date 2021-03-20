using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Database.Movie;
using Services.ExternalMovieApi;

namespace Services.Movie {
    public sealed class MovieServices : IMovieServices {
        private readonly IMovieRepository _movieRepository;
        private readonly IExternalMovieApiServices _externalMovieApiServices;

        public MovieServices(IMovieRepository movieRepository, IExternalMovieApiServices externalMovieApiServices) {
            _movieRepository = movieRepository;
            _externalMovieApiServices = externalMovieApiServices;
        }

        public async Task UpdatePopularMovies() {
            var movies = await _externalMovieApiServices.GetPopularMovies();
            _movieRepository.UpdatePopularMovies(movies);
            _movieRepository.SaveChanges();
        }

        public ImmutableList<Domain.Movie> GetPopularMovies() => _movieRepository.GetPopularMovies();

        public async Task<ImmutableList<Domain.Movie>> SearchMovie(string name) {
            // get movies from external api
            var movies = await _externalMovieApiServices.SearchMovie(name);

            // need this list to save new generated id of not saved in database movies 
            var newMovies = new List<Domain.Movie>();
            
            // go throw fetched movies and save not existing movies
            foreach (var movie in movies) {
                var model = _movieRepository.GetMovieByExternalId(movie.KpId);
                if (model == null) {
                    _movieRepository.AddMovie(movie);
                    _movieRepository.SaveChanges();
                    model = _movieRepository.GetMovieByExternalId(movie.KpId);
                    newMovies.Add(model);
                }
                else {
                    newMovies.Add(model);
                }
            }

            return newMovies.ToImmutableList();
        }

        public void CreateMovie(Domain.Movie movie) {
            _movieRepository.AddMovie(movie);
            _movieRepository.SaveChanges();
        }

        public Domain.Movie GetMovie(long id) => _movieRepository.GetMovie(id);
    }
}