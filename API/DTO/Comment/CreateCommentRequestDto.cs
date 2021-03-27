using System;
using Domain;
using Domain.ValueObjects.Comment;
using FluentValidation;

namespace API.DTO.Comment {
    public sealed class CreateCommentRequestDto {
        public long review { get; set; }
        public long author { get; set; }
        public string content { get; set; }

        public Domain.Comment ToDomain() {
            return new() {
                Author = new Ref<Domain.User>(author),
                Content = new CommentContent(content),
                Review = new Ref<Domain.Review>(review),
                CreatedAt = DateTime.Now,
            };
        }
        
        #nullable enable
        public ValidateErrorDto? Validate() {
            var validator = new CreateCommentRequestDtoValidator();
            var validation = validator.Validate(this);
            if (validation.IsValid) return null;
            return new ValidateErrorDto(validation.Errors);
        }
    }

    public sealed class CreateCommentRequestDtoValidator : AbstractValidator<CreateCommentRequestDto> {
        public CreateCommentRequestDtoValidator() {
            RuleFor(x => x.review).NotEmpty();
            RuleFor(x => x.author).NotEmpty();
            RuleFor(x => x.content).NotEmpty().MaximumLength(1000);
        }
    }
}