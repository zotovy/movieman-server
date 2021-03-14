namespace Database.Review {
    public sealed class ReviewRepository: IReviewRepository {
        private readonly DatabaseContext _context;

        public ReviewRepository(DatabaseContext context) {
            _context = context;
        }
    }
}