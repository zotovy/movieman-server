using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using Database.Comment;
using Database.Movie;
using Database.User;
using Domain;
using Domain.ValueObjects;
using Domain.ValueObjects.Review;

namespace Database.Review {
    public class ReviewModel {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Movie")]
        public long MovieId { get; set; }
        public MovieModel? Movie { get; set; }
        [ForeignKey("Author")]
        public long AuthorId { get; set; }
        public UserModel? Author { get; set; }
        public List<long> CommentIds { get; set; }
        public List<CommentModel>? Comments { get; set; }
        [Column("Content", TypeName = "varchar(2048)")]
        public string Content { get; set; }
        [Column("Rating", TypeName = "double precision")]
        public double Rating { get; set; }
        public DateTime CreatedAt { get; set; }

        public Domain.Review ToDomain() {
            return new Domain.Review {
                Author = new Ref<Domain.User>(AuthorId, Author?.ToDomain()),
                Comments = Comments == null
                    ? CommentIds.Select(x => new Ref<Domain.Comment>(x)).ToList()
                    : Comments.Select(x => new Ref<Domain.Comment>(x.Id, x.ToDomain())).ToList(),
                Content = new ReviewContent(Content),
                Id = Id,
                Movie = new Ref<Domain.Movie>(MovieId, Movie?.ToDomain()),
                Rating = new Rating(Rating),
                CreatedAt = CreatedAt,
            };
        }

        public ReviewModel() { }

        public ReviewModel(Domain.Review review) {
            Id = review.Id;
            Movie = review.Movie.Model == null
                ? null
                : new MovieModel(review.Movie.Model);
            Author = review.Author.Model == null
                ? null
                : new UserModel(review.Author.Model);
            Comments = review.Comments.Select(x => new CommentModel(x.Model)).ToList();
            CommentIds = review.Comments.Select(x => x.Id).ToList();
            Content = review.Content.Value;
            Rating = review.Rating.Value;
            CreatedAt = review.CreatedAt;
            MovieId = review.Movie.Id;
            AuthorId = review.Author.Id;

        }
    }
}
