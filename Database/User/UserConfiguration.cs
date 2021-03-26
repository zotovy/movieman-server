using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.User {
    public sealed class UserConfiguration : IEntityTypeConfiguration<UserModel> {
        public void Configure(EntityTypeBuilder<UserModel> builder) {
            builder.ToTable("Users");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd().UseIdentityAlwaysColumn();
            
            builder.HasIndex(u => u.Email).IsUnique();
            // Convert Name: ValueObject --> EF string type
            // builder.OwnsOne(p => p.Name, a => {
            //     a.Property(u => u.Value)
            //         .HasColumnName(nameof(Name))
            //         .HasColumnType("varchar(64)")
            //         .HasMaxLength(64)
            //         .IsRequired();
            // });
            //
            // // Convert Email: ValueObject --> EF int type
            // builder.OwnsOne(p => p.Email, a => {
            //     a.Property(u => u.Value)
            //         .HasColumnName(nameof(Email))
            //         .HasColumnType("varchar(100)")
            //         .HasMaxLength(100)
            //         .IsRequired();
            // });
            //
            // // Convert Password: ValueObject --> EF int type
            // builder.OwnsOne(p => p.Password, a => {
            //     a.Property(u => u.Value)
            //         .HasColumnName(nameof(Password))
            //         .HasColumnType("varchar(100)")
            //         .HasMaxLength(100)
            //         .IsRequired();
            // });
        }
    }
}