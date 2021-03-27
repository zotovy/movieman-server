using System;
using System.Collections.Generic;
using Domain;
using Domain.ValueObjects;
using Domain.ValueObjects.Review;
using FluentValidation;

namespace API.DTO.Movie {
    public sealed class WriteReviewRequestDto {
        public long movie { get; set; }
        public long author { get; set; }
        public string content { get; set; }
        public double rating { get; set; }

        #nullable enable
        public ValidateErrorDto? Validate() {
            var validator = new WriteReviewRequestValidator();
            var validation = validator.Validate(this);
            if (validation.IsValid) return null;
            return new ValidateErrorDto(validation.Errors);
        }

        public Domain.Review ToDomain() {
            return new() {
                Author = new Ref<Domain.User>(author),
                Movie = new Ref<Domain.Movie>(movie),
                Comments = new List<Ref<Domain.Comment>>(),
                Content = new ReviewContent(content),
                Rating = new Rating(rating),
                CreatedAt = DateTime.Now,
            };
        }
    }

    public sealed class WriteReviewRequestValidator : AbstractValidator<WriteReviewRequestDto> {
        public WriteReviewRequestValidator() {
            RuleFor(x => x.movie).NotEmpty();
            RuleFor(x => x.author).NotEmpty();
            RuleFor(x => x.content).NotEmpty().MaximumLength(2048);
            RuleFor(x => x.rating).NotEmpty().LessThanOrEqualTo(5).GreaterThanOrEqualTo(0);
        }
    }

}