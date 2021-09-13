using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chaffeurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NationalInsurenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chaffeurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuelCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pin = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chassis = table.Column<int>(type: "int", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kilometers = table.Column<double>(type: "float", nullable: false),
                    ChaffeurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<int>(type: "int", nullable: false),
                    ChaffeurEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licenses_Chaffeurs_ChaffeurEntityId",
                        column: x => x.ChaffeurEntityId,
                        principalTable: "Chaffeurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthenticationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<int>(type: "int", nullable: false),
                    FuelCardEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthenticationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthenticationTypes_FuelCards_FuelCardEntityId",
                        column: x => x.FuelCardEntityId,
                        principalTable: "FuelCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChaffeurEntityFuelCardEntity",
                columns: table => new
                {
                    ChaffeursId = table.Column<int>(type: "int", nullable: false),
                    FuelCardsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChaffeurEntityFuelCardEntity", x => new { x.ChaffeursId, x.FuelCardsId });
                    table.ForeignKey(
                        name: "FK_ChaffeurEntityFuelCardEntity_Chaffeurs_ChaffeursId",
                        column: x => x.ChaffeursId,
                        principalTable: "Chaffeurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChaffeurEntityFuelCardEntity_FuelCards_FuelCardsId",
                        column: x => x.FuelCardsId,
                        principalTable: "FuelCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtraServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Service = table.Column<int>(type: "int", nullable: false),
                    FuelCardEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraServices_FuelCards_FuelCardEntityId",
                        column: x => x.FuelCardEntityId,
                        principalTable: "FuelCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarTypes_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChaffeurEntityVehicleEntity",
                columns: table => new
                {
                    ChaffeursId = table.Column<int>(type: "int", nullable: false),
                    VehiclesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChaffeurEntityVehicleEntity", x => new { x.ChaffeursId, x.VehiclesId });
                    table.ForeignKey(
                        name: "FK_ChaffeurEntityVehicleEntity_Chaffeurs_ChaffeursId",
                        column: x => x.ChaffeursId,
                        principalTable: "Chaffeurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChaffeurEntityVehicleEntity_Vehicles_VehiclesId",
                        column: x => x.VehiclesId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fuels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fuel = table.Column<int>(type: "int", nullable: false),
                    FuelCardEntityId = table.Column<int>(type: "int", nullable: false),
                    VehicleEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fuels_FuelCards_FuelCardEntityId",
                        column: x => x.FuelCardEntityId,
                        principalTable: "FuelCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fuels_Vehicles_VehicleEntityId",
                        column: x => x.VehicleEntityId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    ChaffeurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Chaffeurs_ChaffeurId",
                        column: x => x.ChaffeurId,
                        principalTable: "Chaffeurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Garage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintenances_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Repairments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repairments_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaintenanceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Maintenances_MaintenanceId",
                        column: x => x.MaintenanceId,
                        principalTable: "Maintenances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthenticationTypes_FuelCardEntityId",
                table: "AuthenticationTypes",
                column: "FuelCardEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CarTypes_VehicleId",
                table: "CarTypes",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_ChaffeurEntityFuelCardEntity_FuelCardsId",
                table: "ChaffeurEntityFuelCardEntity",
                column: "FuelCardsId");

            migrationBuilder.CreateIndex(
                name: "IX_ChaffeurEntityVehicleEntity_VehiclesId",
                table: "ChaffeurEntityVehicleEntity",
                column: "VehiclesId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraServices_FuelCardEntityId",
                table: "ExtraServices",
                column: "FuelCardEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Fuels_FuelCardEntityId",
                table: "Fuels",
                column: "FuelCardEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Fuels_VehicleEntityId",
                table: "Fuels",
                column: "VehicleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_MaintenanceId",
                table: "Invoices",
                column: "MaintenanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_ChaffeurEntityId",
                table: "Licenses",
                column: "ChaffeurEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_RequestId",
                table: "Maintenances",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairments_RequestId",
                table: "Repairments",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ChaffeurId",
                table: "Requests",
                column: "ChaffeurId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_VehicleId",
                table: "Requests",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthenticationTypes");

            migrationBuilder.DropTable(
                name: "CarTypes");

            migrationBuilder.DropTable(
                name: "ChaffeurEntityFuelCardEntity");

            migrationBuilder.DropTable(
                name: "ChaffeurEntityVehicleEntity");

            migrationBuilder.DropTable(
                name: "ExtraServices");

            migrationBuilder.DropTable(
                name: "Fuels");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "Repairments");

            migrationBuilder.DropTable(
                name: "FuelCards");

            migrationBuilder.DropTable(
                name: "Maintenances");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Chaffeurs");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
