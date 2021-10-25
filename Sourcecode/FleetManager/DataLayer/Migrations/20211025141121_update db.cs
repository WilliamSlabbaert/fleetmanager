using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLicenses_Chaffeurs_ChaffeurId",
                table: "DrivingLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Chaffeurs_ChaffeurId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "ChaffeurEntityFuelCardEntity");

            migrationBuilder.DropTable(
                name: "ChaffeurEntityVehicleEntity");

            migrationBuilder.RenameColumn(
                name: "ChaffeurId",
                table: "Requests",
                newName: "ChauffeurId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ChaffeurId",
                table: "Requests",
                newName: "IX_Requests_ChauffeurId");

            migrationBuilder.RenameColumn(
                name: "ChaffeurId",
                table: "DrivingLicenses",
                newName: "ChauffeurId");

            migrationBuilder.RenameIndex(
                name: "IX_DrivingLicenses_ChaffeurId",
                table: "DrivingLicenses",
                newName: "IX_DrivingLicenses_ChauffeurId");

            migrationBuilder.CreateTable(
                name: "ChauffeurEntityFuelCardEntity",
                columns: table => new
                {
                    ChauffeurId = table.Column<int>(type: "int", nullable: false),
                    FuelCardId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChauffeurEntityFuelCardEntity", x => new { x.ChauffeurId, x.FuelCardId });
                    table.ForeignKey(
                        name: "FK_ChauffeurEntityFuelCardEntity_Chaffeurs_ChauffeurId",
                        column: x => x.ChauffeurId,
                        principalTable: "Chaffeurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChauffeurEntityFuelCardEntity_FuelCards_FuelCardId",
                        column: x => x.FuelCardId,
                        principalTable: "FuelCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChauffeurEntityVehicleEntity",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    ChauffeurId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChauffeurEntityVehicleEntity", x => new { x.ChauffeurId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_ChauffeurEntityVehicleEntity_Chaffeurs_ChauffeurId",
                        column: x => x.ChauffeurId,
                        principalTable: "Chaffeurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChauffeurEntityVehicleEntity_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChauffeurEntityFuelCardEntity_FuelCardId",
                table: "ChauffeurEntityFuelCardEntity",
                column: "FuelCardId");

            migrationBuilder.CreateIndex(
                name: "IX_ChauffeurEntityVehicleEntity_VehicleId",
                table: "ChauffeurEntityVehicleEntity",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingLicenses_Chaffeurs_ChauffeurId",
                table: "DrivingLicenses",
                column: "ChauffeurId",
                principalTable: "Chaffeurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Chaffeurs_ChauffeurId",
                table: "Requests",
                column: "ChauffeurId",
                principalTable: "Chaffeurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLicenses_Chaffeurs_ChauffeurId",
                table: "DrivingLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Chaffeurs_ChauffeurId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "ChauffeurEntityFuelCardEntity");

            migrationBuilder.DropTable(
                name: "ChauffeurEntityVehicleEntity");

            migrationBuilder.RenameColumn(
                name: "ChauffeurId",
                table: "Requests",
                newName: "ChaffeurId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ChauffeurId",
                table: "Requests",
                newName: "IX_Requests_ChaffeurId");

            migrationBuilder.RenameColumn(
                name: "ChauffeurId",
                table: "DrivingLicenses",
                newName: "ChaffeurId");

            migrationBuilder.RenameIndex(
                name: "IX_DrivingLicenses_ChauffeurId",
                table: "DrivingLicenses",
                newName: "IX_DrivingLicenses_ChaffeurId");

            migrationBuilder.CreateTable(
                name: "ChaffeurEntityFuelCardEntity",
                columns: table => new
                {
                    ChaffeurId = table.Column<int>(type: "int", nullable: false),
                    FuelCardId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChaffeurEntityFuelCardEntity", x => new { x.ChaffeurId, x.FuelCardId });
                    table.ForeignKey(
                        name: "FK_ChaffeurEntityFuelCardEntity_Chaffeurs_ChaffeurId",
                        column: x => x.ChaffeurId,
                        principalTable: "Chaffeurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChaffeurEntityFuelCardEntity_FuelCards_FuelCardId",
                        column: x => x.FuelCardId,
                        principalTable: "FuelCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChaffeurEntityVehicleEntity",
                columns: table => new
                {
                    ChaffeurId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChaffeurEntityVehicleEntity", x => new { x.ChaffeurId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_ChaffeurEntityVehicleEntity_Chaffeurs_ChaffeurId",
                        column: x => x.ChaffeurId,
                        principalTable: "Chaffeurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChaffeurEntityVehicleEntity_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChaffeurEntityFuelCardEntity_FuelCardId",
                table: "ChaffeurEntityFuelCardEntity",
                column: "FuelCardId");

            migrationBuilder.CreateIndex(
                name: "IX_ChaffeurEntityVehicleEntity_VehicleId",
                table: "ChaffeurEntityVehicleEntity",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingLicenses_Chaffeurs_ChaffeurId",
                table: "DrivingLicenses",
                column: "ChaffeurId",
                principalTable: "Chaffeurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Chaffeurs_ChaffeurId",
                table: "Requests",
                column: "ChaffeurId",
                principalTable: "Chaffeurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
