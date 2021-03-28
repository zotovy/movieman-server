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

        public ReviewModel AddReview(Domain.Review review) {
            var model = new ReviewModel(review);
            _context.Reviews.Add(model);
            return model;
        }

        public void AddCommentToReview(long id, Domain.Comment comment) {
            var model = _context.Reviews.First(x => x.Id == id);

            var commentModel = new CommentModel { Id = comment.Id };
            model.Comments.Add(commentModel);
        }

        #nullable enable
        public Domain.Review? GetReview(long id) {
            var model = (from review in _context.Reviews
                where review.Id == id
                join author in _context.Users on review.Author equals author
                join movie in _context.Movies on review.Movie equals movie
                select new {
                    Author = author,
                    Movie = movie,
                    Comments = (from comment in _context.Comments
                        where review.CommentIds.Contains(comment.Id)
                        select comment).ToList(),
                    review.Id,
                    review.Content,
                    review.Rating,
                    review.CreatedAt,
                }).FirstOrDefault();

            if (model == null) return null;

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