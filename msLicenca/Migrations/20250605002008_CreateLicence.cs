using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace msLicenca.Migrations
{
    /// <inheritdoc />
    public partial class CreateLicence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Licenca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdEpresa = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IdTipoLicenca = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Data_emissao = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    Data_validade = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenca", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Licenca");
        }
    }
}
