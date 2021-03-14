using Database.Review;
using Database.User;
using Microsoft.EntityFrameworkCore;

namespace Database {
    public sealed class DatabaseContext: DbContext {
        public DatabaseContext(DbContextOptions options) : base(options) {
            Database.EnsureCreated();
        }
        
        public DatabaseContext() {}

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=7852");
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}