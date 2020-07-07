using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Freddy.Persistance.Migrations
{
    public partial class ChangeIdToGuid_AddGuidIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Products",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Code", "Name", "Size" },
                values: new object[] { new Guid("e8e060d6-5cfc-4009-b150-c0870cc45464"), "WRUP1LC001, P4", "Fáradt rózsaszín pamut nadrág- csípő", "M" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Code", "Name", "Size" },
                values: new object[] { new Guid("3b50451a-05d1-4e96-a2d7-7ff1e2cca09f"), "WRUP1LC001, N", "Fekete pamut nadrág- csípő", "XL" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3b50451a-05d1-4e96-a2d7-7ff1e2cca09f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e8e060d6-5cfc-4009-b150-c0870cc45464"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "OldId",
                table: "Products",
                type: "int",
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
    }
}
