using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class updatedb20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChauffeurEntityFuelCardEntity_Chaffeurs_ChauffeurId",
                table: "ChauffeurEntityFuelCardEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ChauffeurEntityVehicleEntity_Chaffeurs_ChauffeurId",
                table: "ChauffeurEntityVehicleEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLicenses_Chaffeurs_ChauffeurId",
                table: "DrivingLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Chaffeurs_ChauffeurId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chaffeurs",
                table: "Chaffeurs");

            migrationBuilder.RenameTable(
                name: "Chaffeurs",
                newName: "Chauffeurs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chauffeurs",
                table: "Chauffeurs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChauffeurEntityFuelCardEntity_Chauffeurs_ChauffeurId",
                table: "ChauffeurEntityFuelCardEntity",
                column: "ChauffeurId",
                principalTable: "Chauffeurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChauffeurEntityVehicleEntity_Chauffeurs_ChauffeurId",
                table: "ChauffeurEntityVehicleEntity",
                column: "ChauffeurId",
                principalTable: "Chauffeurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingLicenses_Chauffeurs_ChauffeurId",
                table: "DrivingLicenses",
                column: "ChauffeurId",
                principalTable: "Chauffeurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Chauffeurs_ChauffeurId",
                table: "Requests",
                column: "ChauffeurId",
                principalTable: "Chauffeurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChauffeurEntityFuelCardEntity_Chauffeurs_ChauffeurId",
                table: "ChauffeurEntityFuelCardEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_ChauffeurEntityVehicleEntity_Chauffeurs_ChauffeurId",
                table: "ChauffeurEntityVehicleEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_DrivingLicenses_Chauffeurs_ChauffeurId",
                table: "DrivingLicenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Chauffeurs_ChauffeurId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chauffeurs",
                table: "Chauffeurs");

            migrationBuilder.RenameTable(
                name: "Chauffeurs",
                newName: "Chaffeurs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chaffeurs",
                table: "Chaffeurs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChauffeurEntityFuelCardEntity_Chaffeurs_ChauffeurId",
                table: "ChauffeurEntityFuelCardEntity",
                column: "ChauffeurId",
                principalTable: "Chaffeurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChauffeurEntityVehicleEntity_Chaffeurs_ChauffeurId",
                table: "ChauffeurEntityVehicleEntity",
                column: "ChauffeurId",
                principalTable: "Chaffeurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
