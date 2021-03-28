using System;
using System.Collections.Generic;
using System.Linq;
using Database.Comment;
using Database.Movie;
using Database.Review;
using Database.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Database {
    public sealed class DatabaseContext: DbContext {
        public DatabaseContext(DbContextOptions options) : base(options) {
            Database.EnsureCreated();
        }
        
        public DatabaseContext() {}

        public DbSet<UserModel> Users { get; set; }
        public DbSet<MovieModel> Movies { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<LinkToPopularMovieModel> LinkToPopularMovieModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=7852");
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            
            var valueComparer = new ValueComparer<List<string>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList());

            
            builder.Entity<MovieModel>()
                .Property(m => m.Genres)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                )
                .Metadata
                .SetValueComparer(valueComparer);

            // builder.Entity<CommentModel>()
            //     .HasOne(c => c.ReviewModel)
            //     .WithMany(r => r.CommentModels)
            //     .HasForeignKey(c => c.Review);

            builder.Entity<ReviewModel>()
                .HasMany(r => r.Comments)
                .WithOne(c => c.Review)
                .HasForeignKey(c => c.ReviewId);

            builder.Entity<ReviewModel>()
                .HasOne(r => r.Author)
                .WithMany()
                .HasForeignKey(x => x.AuthorId);

            builder.Entity<ReviewModel>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Reviews);

            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}