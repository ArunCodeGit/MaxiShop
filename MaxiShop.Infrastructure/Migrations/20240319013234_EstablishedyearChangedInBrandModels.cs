using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaxiShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EstablishedyearChangedInBrandModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EstablishedYear",
                table: "Brand",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EstablishedYear",
                table: "Brand",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
