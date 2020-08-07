using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Freddy.Persistence.Migrations
{
    public partial class Add_Customer_To_Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "Name", "Phone" },
                values: new object[] { new Guid("2f4172f9-8537-4059-8567-31d5e36029a9"), "bujdosoreka@humail.hu", "Bujdosó Réka", "+36 123 90 8999" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("2f4172f9-8537-4059-8567-31d5e36029a9"));
        }
    }
}
