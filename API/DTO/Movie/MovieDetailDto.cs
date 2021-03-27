using System.Collections.Generic;
using System.Linq;
using API.DTO.Review;

namespace API.DTO.Movie {
    public sealed class MovieDetailDto {
        public long id { get; set; }
        public long kpId { get; set; }
        public List<ReviewDto> reviews { get; set; }
        public string poster { get; set; }
        public List<string> genres { get; set; }
        public double rating { get; set; }
        public string title { get; set; }
        public string year { get; set; }

        public MovieDetailDto() { }

        public MovieDetailDto(Domain.Movie movie) {
            id = movie.Id;
            kpId = movie.KpId;
            reviews = movie.Reviews.Select(x => new ReviewDto(x.Model)).ToList();
            poster = movie.Poster.Value;
            genres = movie.Genres.Select(x => x.Value).ToList();
            rating = movie.Rating.Value;
            title = movie.Title.Value;
            year = movie.Year.Value;
        }
    }
}