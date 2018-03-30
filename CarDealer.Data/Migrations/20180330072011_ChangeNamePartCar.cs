using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CarDealer.Data.Migrations
{
    public partial class ChangeNamePartCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartCars_Cars_CarId",
                table: "PartCars");

            migrationBuilder.DropForeignKey(
                name: "FK_PartCars_Parts_PartId",
                table: "PartCars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartCars",
                table: "PartCars");

            migrationBuilder.RenameTable(
                name: "PartCars",
                newName: "PartCar");

            migrationBuilder.RenameIndex(
                name: "IX_PartCars_CarId",
                table: "PartCar",
                newName: "IX_PartCar_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartCar",
                table: "PartCar",
                columns: new[] { "PartId", "CarId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PartCar_Cars_CarId",
                table: "PartCar",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartCar_Parts_PartId",
                table: "PartCar",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartCar_Cars_CarId",
                table: "PartCar");

            migrationBuilder.DropForeignKey(
                name: "FK_PartCar_Parts_PartId",
                table: "PartCar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartCar",
                table: "PartCar");

            migrationBuilder.RenameTable(
                name: "PartCar",
                newName: "PartCars");

            migrationBuilder.RenameIndex(
                name: "IX_PartCar_CarId",
                table: "PartCars",
                newName: "IX_PartCars_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartCars",
                table: "PartCars",
                columns: new[] { "PartId", "CarId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PartCars_Cars_CarId",
                table: "PartCars",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartCars_Parts_PartId",
                table: "PartCars",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
