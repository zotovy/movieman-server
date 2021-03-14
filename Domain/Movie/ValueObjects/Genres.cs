using System;
using System.Linq;


namespace Domain.Movie.ValueObjects {

    public static class AvailableGenres {
        public static readonly string[] availableGenres = {
            "комедия",
            "мультфильм",
            "триллер",
            "ужасы",
            "фантастика",
            "аниме",
            "биография",
            "боевик",
            "вестерн",
            "военный",
            "детективы",
            "детский",
            "документальный",
            "драма",
            "игра",
            "исторический",
            "концерт",
            "короткометражка",
            "криминал",
            "мелодрама",
            "музыкальные",
            "мюзиклы",
            "новости",
            "приключения",
            "реальное ТВ",
            "семейный",
            "спортивное",
            "ток-шоу",
            "фильм-нуар",
            "фэнтези",
            "церемония",
        };
    }
    
    public sealed record MovieGenre {
        public string Value { get; }

        public static bool Validate(string value) {
            return AvailableGenres.availableGenres.Contains(value);
        }

        public MovieGenre(string value) {
            if (!Validate(value)) {
                throw new ArgumentException($"{value} is invalid MovieGenre");
            }
            Value = value;
        }
    }
}