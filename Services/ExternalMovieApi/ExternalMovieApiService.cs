using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain;
using Domain.ValueObjects;
using Domain.ValueObjects.Movie;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Services.ExternalMovieApi {
    public class ExternalMovieApiService : IExternalMovieApiService {
        private readonly HttpClient _httpClient = new();
        private readonly ExternalApiRoutes _externalApiRoutes;

        public ExternalMovieApiService(IConfiguration configuration) {
            var externalApiKey = configuration["ExternalAPI:api_key"];
            _externalApiRoutes = new ExternalApiRoutes(externalApiKey);
        }

        private async Task<JsonObjectType> _fetchAsJson(string route) {
            var raw = await _httpClient.GetAsync(route);
            var body = await raw.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JsonObjectType>(body);
        }

        public async Task<ImmutableList<Movie>> GetPopularMovies() {
            var data = await _fetchAsJson(_externalApiRoutes.GetPopularMovies);
            List<JsonObjectType> raw = data["results"].ToObject<List<JsonObjectType>>();

            List<Movie> movies = raw.Select(
                rawMovie => new Movie {
                    Genres = (rawMovie["genre_ids"] as List<int> ?? new List<int>())
                        .Select(id => ExternalApiMovieHelper.IdToGenre[id])
                        .ToList(),
                    Poster = new ImagePath(_externalApiRoutes.Image(rawMovie["backdrop_path"])),
                    Rating = new Rating(0),
                    Reviews = new List<Ref<Review>>(),
                    Title = new Title(rawMovie["title"]),
                    Year = new Year((rawMovie["release_date"] as string ?? "2000").Split("-")[0]),
                    KpId = rawMovie["id"]
                }
            ).ToList();

            return movies.ToImmutableList();
        }
    }

    public class JsonObjectType : Dictionary<string, dynamic> {
    }
}