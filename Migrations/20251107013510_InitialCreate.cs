using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W9_assignment_template.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Taunt = table.Column<int>(type: "int", nullable: true),
                    Shove = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AggressionLevel = table.Column<int>(type: "int", nullable: true),
                    Experience = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterAbilities",
                columns: table => new
                {
                    AbilitiesId = table.Column<int>(type: "int", nullable: false),
                    CharactersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterAbilities", x => new { x.AbilitiesId, x.CharactersId });
                    table.ForeignKey(
                        name: "FK_CharacterAbilities_Abilities_AbilitiesId",
                        column: x => x.AbilitiesId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterAbilities_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Abilities",
                columns: new[] { "Id", "Description", "Discriminator", "Name", "Taunt" },
                values: new object[,]
                {
                    { 3, "Distract enemies with insults", "GoblinAbility", "Annoying Taunt", 3 },
                    { 4, "Enrage enemies to attack you", "GoblinAbility", "Rage Taunt", 6 }
                });

            migrationBuilder.InsertData(
                table: "Abilities",
                columns: new[] { "Id", "Description", "Discriminator", "Name", "Shove" },
                values: new object[,]
                {
                    { 1, "Push enemies back with force", "PlayerAbility", "Mighty Shove", 5 },
                    { 2, "A powerful pushing attack", "PlayerAbility", "Power Push", 8 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "A dimly lit entrance hall", "Starting Chamber" },
                    { 2, "A foul-smelling cave", "Goblin Den" }
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "AggressionLevel", "Discriminator", "Level", "Name", "RoomId" },
                values: new object[,]
                {
                    { 3, 0, "Goblin", 2, "Goblin Scout", 2 },
                    { 4, 0, "Goblin", 4, "Goblin Chief", 2 }
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Discriminator", "Experience", "Level", "Name", "RoomId" },
                values: new object[,]
                {
                    { 1, "Player", 0, 5, "Hero", 1 },
                    { 2, "Player", 0, 3, "Warrior", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterAbilities_CharactersId",
                table: "CharacterAbilities",
                column: "CharactersId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_RoomId",
                table: "Characters",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterAbilities");

            migrationBuilder.DropTable(
                name: "Abilities");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
