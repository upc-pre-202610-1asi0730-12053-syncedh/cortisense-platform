using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace SyncedHealth.Center.Platform.Migrations
{
    /// <inheritdoc />
    public partial class ClinicalRiskAssessmentSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clinical_alerts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    organization_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    severity = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    status = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    message = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    resolved_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    resolved_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_clinical_alerts", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "risk_assessments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    organization_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    fatigue_level = table.Column<int>(type: "int", nullable: false),
                    risk_level = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    heart_rate = table.Column<int>(type: "int", nullable: false),
                    hrv = table.Column<int>(type: "int", nullable: false),
                    last_updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_risk_assessments", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
            

            migrationBuilder.CreateTable(
                name: "vital_sign_anomalies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    organization_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    severity = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    status = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    value = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    threshold = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    message = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    detected_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    reviewed_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    reviewed_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_vital_sign_anomalies", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "vital_sign_readings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    organization_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    heart_rate = table.Column<int>(type: "int", nullable: false),
                    hrv = table.Column<int>(type: "int", nullable: false),
                    fatigue_level = table.Column<int>(type: "int", nullable: false),
                    cortisol_level = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    sensor_status = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    recorded_at = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_vital_sign_readings", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clinical_alerts");

            migrationBuilder.DropTable(
                name: "risk_assessments");

            migrationBuilder.DropTable(
                name: "vital_sign_anomalies");

            migrationBuilder.DropTable(
                name: "vital_sign_readings");
        }
    }
}
