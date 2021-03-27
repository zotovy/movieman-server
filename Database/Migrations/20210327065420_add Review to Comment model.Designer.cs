﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210327065420_add Review to Comment model")]
    partial class addReviewtoCommentmodel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "6.0.0-preview.2.21154.2")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Database.Comment.CommentModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("Author")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("Content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("Review")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Database.Movie.LinkToPopularMovieModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasKey("Id");

                    b.ToTable("LinksToPopularMovies");
                });

            modelBuilder.Entity("Database.Movie.MovieModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Genres")
                        .HasColumnType("text");

                    b.Property<long>("KpId")
                        .HasColumnType("bigint");

                    b.Property<string>("Poster")
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("Poster");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision");

                    b.Property<List<long>>("Reviews")
                        .HasColumnType("bigint[]");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("Title");

                    b.Property<string>("Year")
                        .HasColumnType("varchar(4)")
                        .HasColumnName("Year");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Database.Review.ReviewModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("Author")
                        .HasColumnType("bigint");

                    b.Property<List<long>>("Comments")
                        .HasColumnType("bigint[]");

                    b.Property<string>("Content")
                        .HasColumnType("varchar(2048)")
                        .HasColumnName("Content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("Movie")
                        .HasColumnType("bigint");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision")
                        .HasColumnName("Rating");

                    b.HasKey("Id");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Database.User.UserModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<List<long>>("Comments")
                        .HasColumnType("bigint[]");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("Email");

                    b.Property<List<long>>("Movies")
                        .HasColumnType("bigint[]");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("Password");

                    b.Property<string>("ProfileImagePath")
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("ProfileImagePath");

                    b.Property<List<long>>("Reviews")
                        .HasColumnType("bigint[]");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
