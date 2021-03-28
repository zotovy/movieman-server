using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Database.Review;
using Domain;
using Domain.ValueObjects;
using Domain.ValueObjects.Movie;

namespace Database.Movie {
    public sealed record MovieModel {
        [Key]
        public long Id { get; set; }
        public long KpId { get; set; }
        public List<long> ReviewIds { get; set; }
        public List<ReviewModel>? Reviews { get; set; }
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
            Reviews = movie.Reviews.Select(x => new ReviewModel(x.Model)).ToList();
            KpId = movie.KpId;
            Poster = movie.Poster.Value;
            Genres = movie.Genres.Select(x => x.Value).ToList();
            Rating = movie.Rating.Value;
            Title = movie.Title.Value;
            Year = movie.Year.Value;
        }

        public Domain.Movie ToDomain() {
            return new () {
                Genres = Genres.Select(g => new MovieGenre(g)).ToList(),
                Id = Id,
                Poster = new ImagePath(Poster),
                Rating = new Rating(Rating),
                Reviews = Reviews == null 
                    ?  ReviewIds.Select(r => new Ref<Domain.Review>(r)).ToList() 
                    :  Reviews.Select(r => new Ref<Domain.Review>(r.Id, r.ToDomain())).ToList(),
                Title = new Title(Title),
                Year = new Year(Year),
                KpId = KpId
            };
        }
    }
}