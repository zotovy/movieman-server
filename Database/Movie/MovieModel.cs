using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Database.Movie {
    public sealed record MovieModel {
        [Key]
        public long Id { get; set; }
        public long KpId { get; set; }
        public List<long> Reviews { get; set; }
        [Column("Poster", TypeName = "varchar(1000)")]
        public string Poster { get; set; }
        public List<string> Genres { get; set; }
        public double Rating { get; set; }
        [Column("Title", TypeName = "varchar(1000)")]
        public string Title { get; set; }
        [Column("Year", TypeName = "varchar(4)")]
        public string Year { get; set; }

        public MovieModel() { }

        public MovieModel(Domain.Movie movie) {
            Id = movie.Id;
            Reviews = movie.Reviews.Select(x => x.Id).ToList();
            KpId = movie.KpId;
            Poster = movie.Poster.Value;
            Genres = movie.Genres.Select(x => x.Value).ToList();
            Rating = movie.Rating.Value;
            Title = movie.Title.Value;
            Year = movie.Year.Value;
        }
    }
}