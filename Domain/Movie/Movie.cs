using System.Collections.Generic;
using Domain.Movie.ValueObjects;

namespace Domain.Movie {
    public sealed class Movie {
        public long Id { get; set; }
        public long KpId { get; set; }
        public dynamic Reviews { get; set; } // todo
        public ImagePath Poster { get; set; }
        public IEnumerable<MovieGenre> Genres { get; set; }
        public Rating Rating { get; set; }
        public Title Title { get; set; }
        public Year Year { get; set; }
    }
}