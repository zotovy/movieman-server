using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Entity;
using System.Linq;
using Database.Comment;
using Domain;
using Domain.ValueObjects;
using Domain.ValueObjects.Review;

namespace Database.Review {
    public sealed class ReviewRepository : IReviewRepository {
        private readonly DatabaseContext _context;

        public ReviewRepository(DatabaseContext context) {
            _context = context;
        }

        public bool Exists(long id) {
            return _context.Reviews.FirstOrDefault(x => x.Id == id) != null;
        }

        public ImmutableList<Domain.Review> GetMoviesReviews(long movieId) {
            var reviews = (from review in _context.Reviews
                    where review.MovieId == movieId
                    join author in _context.Users on review.Author equals author
                    join movie in _context.Movies on review.Movie equals movie
                    select new {
                        Author = author,
                        Id = review.Id,
                        Movie = movie.Id,
                        Comments = review.CommentIds,
                        Content = review.Content,
                        Rating = review.Rating,
                        CreatedAt = review.CreatedAt
                    }
                ).ToList();

            return reviews.Select(x => new Domain.Review {
                Author = new Ref<Domain.User>(x.Author.Id, x.Author.ToDomain()),
                Comments = x.Comments?.Select(b => new Ref<Domain.Comment>(b))?.ToList(),
                Content = new ReviewContent(x.Content),
                Id = x.Id,
                Movie = new Ref<Domain.Movie>(x.Movie),
                Rating = new Rating(x.Rating),
                CreatedAt = x.CreatedAt,
            }).ToImmutableList();
        }

        public void AddReview(Domain.Review review) {
            var model = new ReviewModel(review);
            _context.Reviews.Add(model);
            
            var movie = _context.Movies.FirstOrDefault(x => x.Id == review.Movie.Id);

            // Throw error if no movie found
            if (movie == null || movie.Reviews == null) throw new ArgumentException("No movie found");
            
            var amountOfReviews = movie.ReviewIds.Select(x => x).ToList().Count;
            if (amountOfReviews == 0) amountOfReviews = 1;
            
            // Calculate new rating 
            var average = movie.Rating * (amountOfReviews - 1);
            var newAverage = average + review.Rating.Value;
            var newRating = newAverage / amountOfReviews;
            
            movie.ReviewIds.Add(review.Id);
            movie.Rating = newRating;
            
            _context.SaveChanges();
        }

        public void AddCommentToReview(long id, long commentId) {
            var model = _context.Reviews.First(x => x.Id == id);
            model.CommentIds.Add(commentId);
            _context.SaveChanges();
        }

        public Domain.Review? GetReview(long id) {
            var model = (from review in _context.Reviews
                where review.Id == id
                join author in _context.Users on review.Author equals author
                join movie in _context.Movies on review.Movie equals movie
                select new {
                    Author = author,
                    Movie = movie,
                    Comments = (
                        from comment in _context.Comments
                        where review.CommentIds.Contains(comment.Id)
                        join commentAuthor in _context.Users on comment.Author equals commentAuthor
                        select new CommentModel {
                            Author = commentAuthor,
                            Content = comment.Content,
                            Id = comment.Id,
                            Review = comment.Review,
                            ReviewId = comment.ReviewId,
                            AuthorId = comment.AuthorId,
                            CreatedAt = comment.CreatedAt,
                        }
                    ).ToList(),
                    review.Id,
                    review.Content,
                    review.Rating,
                    review.CreatedAt,
                }).FirstOrDefault();

            if (model == null) return null;

            model.Movie.Reviews = null;
            
            return new Domain.Review {
                Author = new Ref<Domain.User>(model.Author.Id, model.Author.ToDomain()),
                Comments = model.Comments.Select(x => new Ref<Domain.Comment>(x.Id, x.ToDomain())).ToList(),
                Content = new ReviewContent(model.Content),
                Id = model.Id,
                Movie = new Ref<Domain.Movie>(model.Movie.Id, model.Movie.ToDomain()),
                Rating = new Rating(model.Rating),
                CreatedAt = model.CreatedAt,
            };
        }
    }
}