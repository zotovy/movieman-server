using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Domain;
using Domain.ValueObjects.Movie;
using Domain.ValueObjects.User;

namespace Database.User {
    public sealed record UserModel {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column("Name", TypeName = "varchar(1000)")]
        public string Name { get; init; }
        [Column("Email", TypeName = "varchar(1000)")]
        public string Email { get; init; }
        [Column("Password", TypeName = "varchar(1000)")]
        public string Password { get; init; }
        public DateTime CreatedAt { get; init; }
        public List<long> Reviews { get; init; }
        public List<long> Movies { get; init; }
        public List<long> Comments { get; init; }
        [Column("ProfileImagePath", TypeName = "varchar(1000)")]
        public string ProfileImagePath { get; init; }

        public UserModel() {}
        
        public UserModel(Domain.User user) {
            Id = user.Id;
            Name = user.Name.Value;
            Email = user.Email.Value;
            Password = user.Password.Value;
            CreatedAt = user.CreatedAt;
            Reviews = user.Reviews.Select(x => x.Id).ToList();
            Movies = user.Movies.Select(x => x.Id).ToList();
            Comments = user.Comments.Select(x => x.Id).ToList();
            ProfileImagePath = user.ProfileImagePath.Value;
        }
        
        public static Domain.User ToDomain(UserModel model) {
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

        public Domain.User ToDomain() {
            return UserModel.ToDomain(this);
        }

        public bool CompareUsingEmailAndPassword(UserModel model) {
            return Email == model.Email && Password == model.Password;
        }
        
        public bool CompareUsingEmailAndPassword(Domain.User model) {
            return Email == model.Email.Value && Password == model.Password.Value;
        }

        public bool CompareUsingEmailAndPassword(Email email, Password password) {
            return Email == email.Value && Password == password.Value;
        }
     }
}