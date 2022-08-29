using Microsoft.EntityFrameworkCore.Migrations;

namespace ChaosBackend.Data.Migrations
{
    public partial class NewChangesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentText",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentText",
                table: "Comments");
        }
    }
}
