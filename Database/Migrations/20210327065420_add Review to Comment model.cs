using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class addReviewtoCommentmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Reviews",
                type: "varchar(2048)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Review",
                table: "Comments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Review",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Reviews",
                type: "varchar(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2048)",
                oldNullable: true);
        }
    }
}
