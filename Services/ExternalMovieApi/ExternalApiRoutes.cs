using Microsoft.Extensions.Configuration;

namespace Services.ExternalMovieApi {
    public class ExternalApiRoutes {
        private readonly string _apiKey;

        public ExternalApiRoutes(string apiKey) {
            _apiKey = apiKey;
        }

        private string _baseApiRoute => "http://api.tmdb.org/3";
        private string _baseFileRoute => "http://image.tmdb.org";
        private string _apiKeyParam => $"?api_key={_apiKey}";
        private string _buildRoute(string route, string q = "") => $"{_baseApiRoute}{route}{_apiKeyParam}{q}";

        public string GetPopularMovies => _buildRoute("/movie/popular");
        public string SearchMovie(string name) => _buildRoute("/search/movie", $"&query={name}"); 
        public string GetByGenre(int genreCode) => _buildRoute("/discover/movie", $"&with_genres={genreCode}"); 
        public string Image(string name) => $"{_baseFileRoute}/t/p/w500{name}";
    }
}