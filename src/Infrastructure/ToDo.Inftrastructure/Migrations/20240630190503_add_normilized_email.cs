using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Inftrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_normilized_email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "task_items",
                newName: "user_id");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "status",
                table: "task_items",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "task_items",
                newName: "UserId");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "task_items",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");
        }
    }
}
