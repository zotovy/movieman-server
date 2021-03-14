namespace Database.Movie {
    public class MovieRepository: IMovieRepository {

        private readonly DatabaseContext _context;

        public MovieRepository(DatabaseContext context) {
            _context = context;
        }
    }
}