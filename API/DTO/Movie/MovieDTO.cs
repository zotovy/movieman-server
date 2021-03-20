using System.Collections.Generic;
using System.Linq;

namespace API.DTO.Movie {
    public class MovieDto {
        public long id { get; set; }
        public long kpId { get; set; }
        public IEnumerable<long> reviews { get; set; }
        public string poster { get; set; }
        public IEnumerable<string> genres { get; set; }
        public double rating { get; set; }
        public string title { get; set; }
        public string year { get; set; }

        public MovieDto() { }

        public MovieDto(Domain.Movie movie) {
            id = movie.Id;
            kpId = movie.KpId;
            reviews = movie.Reviews.Select(x => x.Id).ToList();
            poster = movie.Poster.Value;
            genres = movie.Genres.Select(x => x.Value).ToList();
            rating = movie.Rating.Value;
            title = movie.Title.Value;
            year = movie.Year.Value;
        }
    }
}