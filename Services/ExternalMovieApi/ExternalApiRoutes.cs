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

        public string GetPopularMovies => _buildRoute("/discover/movie");
        public string SearchMovie(string name) => _buildRoute("/search/movie", $"&query={name}"); 
        public string Image(string name) => $"{_baseFileRoute}/t/p/w500{name}";
    }
}