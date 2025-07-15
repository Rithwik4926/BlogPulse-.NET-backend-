using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class SeedAdminUserStaticHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8d0be6-9c08-422e-aac6-e54d02853c1f",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENTJTXhmo8pMLjKJXHTWtjWCl/sn3whi2vpWslju1gJzBYdR8DH+Lwggbk2jy29IKg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8d0be6-9c08-422e-aac6-e54d02853c1f",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIn2H+Hgrcd61zwXtNl6KXItZ4Uj8tERU2YTOGKeHoWzJ2fS7ofIbm6MLy1jkKaaDw==");
        }
    }
}
