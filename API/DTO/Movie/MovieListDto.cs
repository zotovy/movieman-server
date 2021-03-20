using System.Collections.Generic;

namespace API.DTO.Movie {
    public class MovieListDto {
        public bool success { get; set; }
        public IEnumerable<MovieDto> movies { get; set; }

        public MovieListDto(bool success) {
            this.success = success;
        }

        public MovieListDto(bool success, IEnumerable<MovieDto> movies) {
            this.success = success;
            this.movies = movies;
        }

        public MovieListDto(IEnumerable<MovieDto> movies) {
            this.success = true;
            this.movies = movies;
        }
    }
}