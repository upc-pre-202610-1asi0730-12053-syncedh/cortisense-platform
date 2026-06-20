using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace SyncedHealth.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditComplianceAuditLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "audit_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    organization_id = table.Column<int>(type: "int", nullable: false),
                    actor_user_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    severity = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    resource_type = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    resource_id = table.Column<int>(type: "int", nullable: false),
                    source = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_audit_logs", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_logs");
        }
    }
}
