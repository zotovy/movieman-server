using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Database.Movie;
using Services.ExternalMovieApi;

namespace Services.Movie {
    public sealed class MovieServices: IMovieServices {

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
    }
}