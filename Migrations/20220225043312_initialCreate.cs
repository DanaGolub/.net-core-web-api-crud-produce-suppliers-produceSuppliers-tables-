using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentBackEnd1.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produces",
                columns: table => new
                {
                    ProduceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produces", x => x.ProduceID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "ProduceSuppliers",
                columns: table => new
                {
                    ProduceID = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduceSuppliers", x => new { x.ProduceID, x.SupplierID });
                    table.ForeignKey(
                        name: "FK_ProduceSuppliers_Produces_ProduceID",
                        column: x => x.ProduceID,
                        principalTable: "Produces",
                        principalColumn: "ProduceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProduceSuppliers_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Produces",
                columns: new[] { "ProduceID", "Description" },
                values: new object[,]
                {
                    { 1, "Oranges" },
                    { 2, "Apples" },
                    { 3, "Peaches" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierID", "SupplierName" },
                values: new object[,]
                {
                    { 1, "Kin's Market" },
                    { 2, "Fresh Street Market" }
                });

            migrationBuilder.InsertData(
                table: "ProduceSuppliers",
                columns: new[] { "ProduceID", "SupplierID", "Qty" },
                values: new object[] { 1, 1, 25 });

            migrationBuilder.InsertData(
                table: "ProduceSuppliers",
                columns: new[] { "ProduceID", "SupplierID", "Qty" },
                values: new object[] { 3, 1, 30 });

            migrationBuilder.InsertData(
                table: "ProduceSuppliers",
                columns: new[] { "ProduceID", "SupplierID", "Qty" },
                values: new object[] { 2, 2, 12 });

            migrationBuilder.CreateIndex(
                name: "IX_ProduceSuppliers_SupplierID",
                table: "ProduceSuppliers",
                column: "SupplierID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduceSuppliers");

            migrationBuilder.DropTable(
                name: "Produces");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
