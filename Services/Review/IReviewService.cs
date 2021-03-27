namespace Services.Review {
    public interface IReviewService {
        public bool Exists(long id);
        public void WriteComment(long id, Domain.Comment comment);
    }
}