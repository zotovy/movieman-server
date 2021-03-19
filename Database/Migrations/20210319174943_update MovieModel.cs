using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class updateMovieModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Author = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<string>(type: "varchar(1000)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinksToPopularMovies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinksToPopularMovies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KpId = table.Column<long>(type: "bigint", nullable: false),
                    Reviews = table.Column<List<long>>(type: "bigint[]", nullable: true),
                    Poster = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Genres = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    Title = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Year = table.Column<string>(type: "varchar(4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Movie = table.Column<long>(type: "bigint", nullable: false),
                    Author = table.Column<long>(type: "bigint", nullable: false),
                    Comments = table.Column<List<long>>(type: "bigint[]", nullable: true),
                    Content = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Email = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Password = table.Column<string>(type: "varchar(1000)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Reviews = table.Column<List<long>>(type: "bigint[]", nullable: true),
                    Movies = table.Column<List<long>>(type: "bigint[]", nullable: true),
                    Comments = table.Column<List<long>>(type: "bigint[]", nullable: true),
                    ProfileImagePath = table.Column<string>(type: "varchar(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "LinksToPopularMovies");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
