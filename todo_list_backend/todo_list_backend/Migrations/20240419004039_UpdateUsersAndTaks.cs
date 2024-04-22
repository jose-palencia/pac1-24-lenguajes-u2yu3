using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_list_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsersAndTaks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "first_name",
                schema: "security",
                table: "users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                schema: "security",
                table: "users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                schema: "transactional",
                table: "tasks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tasks_user_id",
                schema: "transactional",
                table: "tasks",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_users_user_id",
                schema: "transactional",
                table: "tasks",
                column: "user_id",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_users_user_id",
                schema: "transactional",
                table: "tasks");

            migrationBuilder.DropIndex(
                name: "IX_tasks_user_id",
                schema: "transactional",
                table: "tasks");

            migrationBuilder.DropColumn(
                name: "first_name",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "last_name",
                schema: "security",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                schema: "transactional",
                table: "tasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
