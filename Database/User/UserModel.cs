using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Domain;
using Domain.ValueObjects.Movie;
using Domain.ValueObjects.User;

namespace Database.User {
    [Table("Users")]
    public sealed record UserModel {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column("Name", TypeName = "varchar(1000)")]
        public string Name { get; set; }
        [Column("Email", TypeName = "varchar(1000)")]
        public string Email { get; set; }
        [Column("Password", TypeName = "varchar(1000)")]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<long> Reviews { get; set; }
        public List<long> Movies { get; set; }
        public List<long> Comments { get; set; }
        [Column("ProfileImagePath", TypeName = "varchar(1000)")]
        public string ProfileImagePath { get; set; }

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
            return new () {
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

        public void UseDataFrom(Domain.User user) {
            // this method will compare only email and name
            if (user.Email != null && user.Email.Value != Email) Email = user.Email.Value;
            if (user.Name != null && user.Name.Value != Name) Name = user.Name.Value;
        }
     }
}