using System;
using System.Collections.Generic;
using System.Linq;
using API.DTO.Comment;
using API.DTO.User;

namespace API.DTO.Review {
    public sealed class DetailReviewWithoutMovieDto {
        public long id { get; set; }
        public long movie { get; set; }
        public UserDetailDto author { get; set; }
        public List<CommentDto> comments { get; set; }
        public string content { get; set; }
        public DateTime createdAt { get; set; }
        public double rating { get; set; }

        public DetailReviewWithoutMovieDto(Domain.Review review) {
            id = review.Id;
            movie = review.Movie.Id;
            author = new UserDetailDto(review.Author.Model);
            comments = review.Comments.Select(x => new CommentDto(x.Model)).ToList();
            content = review.Content.Value;
            createdAt = review.CreatedAt;
            rating = review.Rating.Value;
        }
    }
}