using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Domain;
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

        public static  Domain.User ToDomain(UserModel model) {
            return new Domain.User {
                Id = model.Id,
                Email = new Email(model.Email),
                Password = new Password(model.Password),
                Name = new Name(model.Name),
                Comments = model.Comments.Select(c => new Ref<Comment>(c)).ToList(),
                Reviews = model.Reviews.Select(c => new Ref<Review>(c)).ToList(),
                Movies = model.Movies.Select(c => new Ref<Movie>(c)).ToList(),
                CreatedAt = model.CreatedAt,
            };
        }
    }
}