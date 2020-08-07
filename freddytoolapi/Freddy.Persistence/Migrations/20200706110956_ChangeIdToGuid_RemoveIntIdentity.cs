using Microsoft.EntityFrameworkCore.Migrations;

namespace Freddy.Persistence.Migrations
{
    public partial class ChangeIdToGuid_RemoveIntIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "OldId",
                table: "Products",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "OldId");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "OldId", "Code", "Name", "Size" },
                values: new object[] { 1, "WRUP1LC001, P4", "Fáradt rózsaszín pamut nadrág- csípő", "M" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "OldId", "Code", "Name", "Size" },
                values: new object[] { 2, "WRUP1LC001, N", "Fekete pamut nadrág- csípő", "XL" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "OldId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "OldId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "OldId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Code", "Name", "Size" },
                values: new object[] { 1, "WRUP1LC001, P4", "Fáradt rózsaszín pamut nadrág- csípő", "M" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Code", "Name", "Size" },
                values: new object[] { 2, "WRUP1LC001, N", "Fekete pamut nadrág- csípő", "XL" });
        }
    }
}
