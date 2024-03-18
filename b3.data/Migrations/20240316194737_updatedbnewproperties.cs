using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace telefones_data.Migrations
{
    public partial class updatedbnewproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "celular",
                table: "Tarefas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cpf",
                table: "Tarefas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "responsavel",
                table: "Tarefas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "celular",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "cpf",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "responsavel",
                table: "Tarefas");
        }
    }
}
