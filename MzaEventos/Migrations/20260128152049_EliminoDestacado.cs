using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MzaEventos.Migrations
{
    /// <inheritdoc />
    public partial class EliminoDestacado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destacado",
                table: "Eventos");

            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                table: "Eventos",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Activo",
                table: "Eventos",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Destacado",
                table: "Eventos",
                type: "bit",
                nullable: true);
        }
    }
}
