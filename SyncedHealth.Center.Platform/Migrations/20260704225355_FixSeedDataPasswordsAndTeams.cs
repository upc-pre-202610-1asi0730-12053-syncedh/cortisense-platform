using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncedHealth.Center.Platform.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedDataPasswordsAndTeams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // --- FIX 1: Real BCrypt passwords for seeded users ---
            // Admin123!
            migrationBuilder.Sql("UPDATE users SET password_hash = '$2a$11$7jYJbJNsTwCkQtZBj7.PUeKIYjD2MwgIU1THdt9FfGTeoE1llXHuO' WHERE id = 101 AND LEFT(password_hash,7) != '$2a$11';");
            // Supervisor123!
            migrationBuilder.Sql("UPDATE users SET password_hash = '$2a$11$HtR/aC.XUPxkrCqFfMIiZuIrsLAlTqDVQGz49fAyY.UJXn7chff7O' WHERE id IN (102, 207) AND LEFT(password_hash,7) != '$2a$11';");
            // Doctor123!
            migrationBuilder.Sql("UPDATE users SET password_hash = '$2a$11$C23CsBb5kbSTgWN6cDYktuql0KB4b1tyBJlRNiXTwWytvR..d6lxq' WHERE id IN (103, 104, 205, 206) AND LEFT(password_hash,7) != '$2a$11';");

            // --- FIX 2: Assign work areas and specialties to seeded Medical Staff ---
            migrationBuilder.Sql("UPDATE users SET work_area_id = 1, specialty_id = 3 WHERE id = 103 AND work_area_id IS NULL;");
            migrationBuilder.Sql("UPDATE users SET work_area_id = 2, specialty_id = 4 WHERE id = 104 AND work_area_id IS NULL;");
            migrationBuilder.Sql("UPDATE users SET work_area_id = 3, specialty_id = 1 WHERE id = 205 AND work_area_id IS NULL;");
            migrationBuilder.Sql("UPDATE users SET work_area_id = 6, specialty_id = 2 WHERE id = 206 AND work_area_id IS NULL;");
            migrationBuilder.Sql("UPDATE users SET work_area_id = 1, specialty_id = 5 WHERE id = 207 AND work_area_id IS NULL;");

            // --- FIX 3: Create care teams for Organization 101 ---
            migrationBuilder.Sql(@"INSERT IGNORE INTO care_teams (id, name, supervisor_id, work_area_id, status, organization_id, created_at, updated_at)
                VALUES (201, 'Equipo UCI Crítico', 102, 1, 'ACTIVE', 101, NOW(), NOW()),
                       (202, 'Equipo Emergencias', 207, 2, 'ACTIVE', 101, NOW(), NOW());");

            // --- FIX 4: Create team members for org 101 ---
            migrationBuilder.Sql(@"INSERT IGNORE INTO team_members (id, team_id, user_id, created_at, updated_at)
                VALUES (201, 201, 103, NOW(), NOW()),
                       (202, 201, 104, NOW(), NOW()),
                       (203, 202, 205, NOW(), NOW()),
                       (204, 202, 206, NOW(), NOW());");

            // --- FIX 5: Create clinical alerts for org 101 ---
            migrationBuilder.Sql(@"INSERT IGNORE INTO clinical_alerts (id, user_id, organization_id, status, severity, message, created_at, updated_at)
                VALUES (201, 103, 101, 'ACTIVE',   'HIGH',     'Fatiga extrema detectada - Laura Gomez supera umbral crítico', NOW(), NOW()),
                       (202, 104, 101, 'ACTIVE',   'MODERATE', 'Frecuencia cardíaca elevada detectada en Juan Perez',          NOW(), NOW()),
                       (203, 205, 101, 'RESOLVED', 'MODERATE', 'Fatiga media resuelta - Ana Torres en recuperación',           NOW(), NOW()),
                       (204, 206, 101, 'ACTIVE',   'LOW',      'Monitoreo preventivo para Luis Ramirez',                      NOW(), NOW());");

            // --- FIX 6: Fix registration_status for test users 107/108 ---
            migrationBuilder.Sql("UPDATE users SET registration_status = 'COMPLETED' WHERE id IN (107, 108) AND (registration_status IS NULL OR registration_status = '');");

            // EF-generated timestamp updates (unchanged)
            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 101,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 22, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(4506), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 102,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 22, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(5469), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 203,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 22, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(5471), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 204,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 22, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(5472), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 101,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 6, 6, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(509), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 22, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(258), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 102,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 7, 2, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(1889), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 18, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(1885), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 103,
                columns: new[] { "check_in_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 4, 12, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(1894), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 0, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(1893), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 4, 12, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(1892), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 204,
                columns: new[] { "check_in_at", "check_out_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 3, 22, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(3171), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 4, 6, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(3172), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 4, 6, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(3170), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 3, 22, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(3167), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 205,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 7, 6, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(4303), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 22, 53, 53, 168, DateTimeKind.Unspecified).AddTicks(4301), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "subscriptions",
                keyColumn: "id",
                keyValue: 101,
                column: "started_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 22, 53, 53, 167, DateTimeKind.Unspecified).AddTicks(8565), new TimeSpan(0, 0, 0, 0, 0)));
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 101,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 22, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(4493), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 102,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 22, 12, 1, 562, DateTimeKind.Unspecified).AddTicks(4429), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 203,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 22, 12, 1, 562, DateTimeKind.Unspecified).AddTicks(4435), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 204,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 22, 12, 1, 562, DateTimeKind.Unspecified).AddTicks(4436), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 101,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 6, 6, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(1750), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 22, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(1599), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 102,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 7, 2, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(2753), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 18, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(2750), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 103,
                columns: new[] { "check_in_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 4, 12, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(2756), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 0, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(2755), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 4, 12, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(2754), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 204,
                columns: new[] { "check_in_at", "check_out_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 3, 22, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(3546), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 4, 6, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(3547), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 4, 6, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(3545), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 3, 22, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(3544), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 205,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 7, 6, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(4365), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 22, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(4364), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "subscriptions",
                keyColumn: "id",
                keyValue: 101,
                column: "started_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 22, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(401), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
