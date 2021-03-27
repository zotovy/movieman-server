using System;
using System.Collections.Generic;
using System.Linq;
using API.DTO.Comment;
using API.DTO.Movie;
using API.DTO.User;

namespace API.DTO.Review {
    public sealed class DetailReviewDto {
        public long id { get; set; }
        public MovieDto movie { get; set; }
        public UserDetailDto author { get; set; }
        public List<CommentDto> comments { get; set; }
        public string content { get; set; }
        public DateTime createdAt { get; set; }

        public DetailReviewDto(Domain.Review review) {
            id = review.Id;
            movie = new MovieDto(review.Movie.Model);
            author = new UserDetailDto(review.Author.Model);
            comments = review.Comments.Select(x => new CommentDto(x.Model)).ToList();
            content = review.Content.Value;
            createdAt = review.CreatedAt;
        }
    }
}