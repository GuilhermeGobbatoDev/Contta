using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contta.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarUsuarioIdDespesas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Despesas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_UsuarioId",
                table: "Despesas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_AspNetUsers_UsuarioId",
                table: "Despesas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_AspNetUsers_UsuarioId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_UsuarioId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Despesas");
        }
    }
}
