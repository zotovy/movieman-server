using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.ValueObjects.Movie;
using Domain.ValueObjects.User;

namespace Database.User {
    public sealed class UserModel {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public DateTime CreatedAt { get; init; }
        public List<long> Reviews { get; init; }
        public List<long> Movies { get; init; }
        public List<long> Comments { get; init; }
        public string ProfileImagePath { get; init; }

        public static  Domain.User ToDomain(UserModel model) {
            return new Domain.User {
                Id = model.Id,
                Email = new Email(model.Email),
                Password = new Password(model.Password),
                Name = new Name(model.Name),
                Comments = model.Comments.Select(c => new Ref<Domain.Comment>(c)).ToList(),
                Reviews = model.Reviews.Select(c => new Ref<Domain.Review>(c)).ToList(),
                Movies = model.Movies.Select(c => new Ref<Domain.Movie>(c)).ToList(),
                CreatedAt = model.CreatedAt,
                ProfileImagePath = new ImagePath(model.ProfileImagePath),
            };
        }
    }
}