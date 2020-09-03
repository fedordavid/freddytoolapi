using System;
using Freddy.Persistence.Events;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Freddy.Persistence.Migrations
{
    public partial class AddEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Stream = table.Column<string>(maxLength: 128, nullable: false),
                    StreamVersion = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Payload = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_Stream",
                table: "Events",
                column: nameof(EventDescriptorEntity.Stream));
            
            migrationBuilder.CreateIndex(
                name: "IX_Events_Stream_StreamVersion",
                table: "Events",
                columns: new [] { nameof(EventDescriptorEntity.Stream), nameof(EventDescriptorEntity.StreamVersion) },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
