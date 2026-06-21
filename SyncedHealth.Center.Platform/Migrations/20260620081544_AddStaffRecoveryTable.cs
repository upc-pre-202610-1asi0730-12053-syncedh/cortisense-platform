using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace SyncedHealth.Center.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddStaffRecoveryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                    name: "recovery_plans",
                    columns: table => new
                    {
                        id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                        medical_staff_id = table.Column<int>(type: "int", nullable: false),
                        description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                        suggested_rest_days = table.Column<int>(type: "int", nullable: false),
                        status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                        created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                        updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("p_k_recovery_plans", x => x.id);
                    })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recovery_plans");
        }
    }
}