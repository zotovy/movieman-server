using Microsoft.Extensions.Configuration;

namespace Services.ExternalMovieApi {
    public class ExternalApiRoutes {
        private readonly string _apiKey;

        public ExternalApiRoutes(string apiKey) {
            _apiKey = apiKey;
        }

        private string _baseRoute => "http://api.tmdb.org/3";
        private string _apiKeyParam => $"?api_key={_apiKey}";
        private string _buildRoute(string route, string q = "") => $"{_baseRoute}{route}{_apiKeyParam}{q}";

        public string GetPopularMovies => _buildRoute("/discover/movie");
        public string Image(string name) => $"{_baseRoute}/t/p/w500{name}";
    }
}