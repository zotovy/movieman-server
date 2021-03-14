using System.Collections.Generic;
using Domain.ValueObjects;
using Domain.ValueObjects.Movie;

namespace Domain {
    public sealed class Movie {
        public long Id { get; set; }
        public long KpId { get; set; }
        public IEnumerable<Ref<Review>> Reviews { get; set; }
        public ImagePath Poster { get; set; }
        public IEnumerable<MovieGenre> Genres { get; set; }
        public Rating Rating { get; set; }
        public Title Title { get; set; }
        public Year Year { get; set; }
    }
}