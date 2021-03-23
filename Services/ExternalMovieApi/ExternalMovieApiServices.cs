using System;
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
    public class ExternalMovieApiServices : IExternalMovieApiServices {
        private readonly HttpClient _httpClient = new();
        private readonly ExternalApiRoutes _externalApiRoutes;

        public ExternalMovieApiServices(IConfiguration configuration) {
            var externalApiKey = configuration["ExternalAPI:api_key"];
            _externalApiRoutes = new ExternalApiRoutes(externalApiKey);
        }

        private async Task<JsonObjectType> _fetchAsJson(string route) {
            var raw = await _httpClient.GetAsync(route);
            var body = await raw.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JsonObjectType>(
                body,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
            );
        }

        private Domain.Movie JsonToMovie(JsonObjectType rawMovie) {
            return new() {
                Genres = (rawMovie["genre_ids"].ToObject<List<int>>() as List<int>)
                    .Select(id => ExternalApiMovieHelper.IdToGenre[id])
                    .ToList(),
                Poster = new ImagePath(_externalApiRoutes.Image(rawMovie["backdrop_path"] ?? rawMovie["poster_path"])),
                Rating = new Rating(0),
                Reviews = new List<Ref<Review>>(),
                Title = new Title(rawMovie["title"]),
                Year = new Year((rawMovie["release_date"] as string ?? "2000").Split("-")[0]),
                KpId = rawMovie["id"]
            };
        }

        public async Task<ImmutableList<Domain.Movie>> GetPopularMovies() {
            var data = await _fetchAsJson(_externalApiRoutes.GetPopularMovies);
            List<JsonObjectType> raw = data["results"].ToObject<List<JsonObjectType>>();
            var movies = raw.Select(JsonToMovie).ToList();
            return movies.ToImmutableList();
        }

        public async Task<ImmutableList<Domain.Movie>> SearchMovie(string name) {
            var data = await _fetchAsJson(_externalApiRoutes.SearchMovie(name));
            List<JsonObjectType> raw = data["results"].ToObject<List<JsonObjectType>>();
            var movies = raw.Where(j => j["backdrop_path"] != null).Select(JsonToMovie).ToList();
            return movies.ToImmutableList();
        }

        public async Task<ImmutableList<Domain.Movie>> GetMoviesByGenre(MovieGenre genre) {
            var genreId = ExternalApiMovieHelper.GetGenreId(genre);
            var data = await _fetchAsJson(_externalApiRoutes.GetByGenre(genreId));
            List<JsonObjectType> raw = data["results"].ToObject<List<JsonObjectType>>();
            var movies = raw.Where(j => j["backdrop_path"] != null).Select(JsonToMovie).ToList();
            return movies.ToImmutableList();
        }
    }

    public class JsonObjectType : Dictionary<string, dynamic> {
    }
}