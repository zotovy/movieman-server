using Microsoft.EntityFrameworkCore;

namespace Database.User {
    public sealed class UserContext: DbContext  {
        public UserContext(DbContextOptions options) : base(options) {
            Database.EnsureCreated();
        }

        public DbSet<UserModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=7852");
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}