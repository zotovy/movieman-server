using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Movie {
    public sealed class MovieConfiguration : IEntityTypeConfiguration<MovieModel> {
        public void Configure(EntityTypeBuilder<MovieModel> builder) {
            builder.ToTable("Moviews");

            builder.Property(m => m.Genres)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );

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