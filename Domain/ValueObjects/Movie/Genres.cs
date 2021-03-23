using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace Domain.ValueObjects.Movie {

    public static class AvailableGenres {
        public static readonly string[] availableGenres = {
            "Action",
            "Adventure",
            "Animation",
            "Comedy",
            "Crime",
            "Documentary",
            "Drama",
            "Family",
            "Fantasy",
            "History",
            "Horror",
            "Music",
            "Mystery",
            "Romance",
            "Science Fiction",
            "TV Movie",
            "Thriller",
            "War",
            "Western"
        };
    }
    
    public sealed record MovieGenre {
        public string Value { get; }

        public static bool Validate(string value) {
            return AvailableGenres.availableGenres.Contains(value);
        }

        public MovieGenre(string value) {
            value = new CultureInfo("en-US").TextInfo.ToTitleCase(value);
            if (!Validate(value)) {
                throw new ArgumentException($"{value} is invalid MovieGenre");
            }
            Value = value;
        }
    }
}