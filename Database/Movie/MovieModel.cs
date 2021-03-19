using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Database.Movie {
    public sealed record MovieModel {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Review")]
        public List<long> Reviews { get; set; }

        public MovieModel() { }

        public MovieModel(Domain.Movie movie) {
            Id = movie.Id;
            Reviews = movie.Reviews.Select(x => x.Id).ToList();
        }
    }
}