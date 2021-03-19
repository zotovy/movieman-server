using System.Collections.Generic;
using Domain.ValueObjects.Movie;

namespace Services.ExternalMovieApi {
    public static class ExternalApiMovieHelper {
        public static Dictionary<int, MovieGenre> IdToGenre = new() {
            { 28, new MovieGenre("Action") },
            { 12, new MovieGenre("Adventure") },
            { 16, new MovieGenre("Animation") },
            { 35, new MovieGenre("Comedy") },
            { 80, new MovieGenre("Crime") },
            { 99, new MovieGenre("Documentary") },
            { 18, new MovieGenre("Drama") },
            { 10751, new MovieGenre("Family") },
            { 14, new MovieGenre("Fantasy") },
            { 36, new MovieGenre("History") },
            { 27, new MovieGenre("Horror") },
            { 10402, new MovieGenre("Music") },
            { 9648, new MovieGenre("Mystery") },
            { 10749, new MovieGenre("Romance") },
            { 878, new MovieGenre("Science Fiction") },
            { 10770, new MovieGenre("TV Movie") },
            { 53, new MovieGenre("Thriller") },
            { 10752, new MovieGenre("War") },
            { 37, new MovieGenre("Western") },
        };
    }
}