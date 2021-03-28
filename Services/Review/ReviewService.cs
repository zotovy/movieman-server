using System.Collections.Immutable;
using Database.Comment;
using Database.Review;
using Domain;

namespace Services.Review {
    public class ReviewService: IReviewService {
        private readonly IReviewRepository _reviewRepository;
        private readonly ICommentRepository _commentRepository;

        public ReviewService(IReviewRepository reviewRepository, ICommentRepository commentRepository) {
            _reviewRepository = reviewRepository;
            _commentRepository = commentRepository;
        }

        public bool Exists(long id) => _reviewRepository.Exists(id);

        public void WriteComment(long id, Comment comment) {
            var modelId = _commentRepository.AddModel(comment);
            _reviewRepository.AddCommentToReview(id, modelId);
        }

        public ImmutableList<Comment> GetReviewComments(long id) => _commentRepository.GetReviewComments(id);

        public Domain.Review GetReview(long id) => _reviewRepository.GetReview(id);
    }
}