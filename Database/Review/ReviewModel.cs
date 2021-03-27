using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Domain;
using Domain.ValueObjects;
using Domain.ValueObjects.Review;

namespace Database.Review {
    public sealed record ReviewModel {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Movie")]
        public long Movie { get; set; }
        [ForeignKey("User")]
        public long Author { get; set; }
        [ForeignKey("Comment")]
        public List<long> Comments { get; set; }
        [Column("Content", TypeName = "varchar(2048)")]
        public string Content { get; set; }
        [Column("Rating", TypeName = "double precision")]
        public double Rating { get; set; }
        public DateTime CreatedAt { get; set; }

        public Domain.Review ToDomain() {
            return new Domain.Review {
                Author = new Ref<Domain.User>(Author),
                Comments = Comments.Select(x => new Ref<Domain.Comment>(x)).ToList(),
                Content = new ReviewContent(Content),
                Id = Id,
                Movie = new Ref<Domain.Movie>(Movie),
                Rating = new Rating(Rating),
                CreatedAt = CreatedAt,
            };
        }

        public ReviewModel() { }

        public ReviewModel(Domain.Review review) {
            Id = review.Id;
            Movie = review.Movie.Id;
            Author = review.Author.Id;
            Comments = review.Comments.Select(x => x.Id).ToList();
            Content = review.Content.Value;
            Rating = review.Rating.Value;
            CreatedAt = review.CreatedAt;
        }
    }
}