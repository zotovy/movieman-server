using System;
using System.Collections.Generic;
using System.Linq;
using API.DTO.Comment;
using API.DTO.Movie;
using API.DTO.User;

namespace API.DTO.Review {
    public sealed class ReviewDto {
        public long id { get; set; }
        public long movie { get; set; }
        public UserTileDTO author { get; set; }
        public List<long> comments { get; set; }
        public string content { get; set; }
        public DateTime createdAt { get; set; }
        public double rating { get; set; }

        public ReviewDto(Domain.Review review) {
            id = review.Id;
            movie = review.Movie.Id;
            author = new UserTileDTO(review.Author.Model);
            comments = review.Comments.Select(x => x.Id).ToList();
            content = review.Content.Value;
            createdAt = review.CreatedAt;
            rating = review.Rating.Value;
        }
    }
}
