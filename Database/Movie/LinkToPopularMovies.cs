using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Movie {
    [Table("LinksToPopularMovies")]
    public sealed record LinkToPopularMovieModel {
        public long Id { get; set; }

        public LinkToPopularMovieModel() { }

        public LinkToPopularMovieModel(long id) {
            Id = id;
        }
    }
}