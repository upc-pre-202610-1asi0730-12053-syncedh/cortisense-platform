using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SyncedHealth.Center.Platform.Migrations
{
    /// <inheritdoc />
    public partial class FixCareTeamSeedIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "care_teams",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "care_teams",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "team_members",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "team_members",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "team_members",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "team_members",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "work_areas",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "work_areas",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "work_areas",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "care_teams",
                columns: new[] { "id", "created_at", "name", "organization_id", "status", "supervisor_id", "updated_at", "work_area_id" },
                values: new object[,]
                {
                    { 1001, null, "Equipo de Emergencias Alpha", 101, "ACTIVE", 102, null, 1001 },
                    { 1002, null, "Equipo UCI Beta", 101, "ACTIVE", 207, null, 1002 }
                });

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 101,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 1, 37, 57, 400, DateTimeKind.Unspecified).AddTicks(871), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 102,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 1, 37, 57, 400, DateTimeKind.Unspecified).AddTicks(1317), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 203,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 1, 37, 57, 400, DateTimeKind.Unspecified).AddTicks(1318), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 204,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 1, 37, 57, 400, DateTimeKind.Unspecified).AddTicks(1318), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 101,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 7, 9, 37, 57, 399, DateTimeKind.Unspecified).AddTicks(9245), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 7, 1, 37, 57, 399, DateTimeKind.Unspecified).AddTicks(9127), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 102,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 8, 5, 37, 57, 399, DateTimeKind.Unspecified).AddTicks(9797), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 7, 21, 37, 57, 399, DateTimeKind.Unspecified).AddTicks(9795), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 103,
                columns: new[] { "check_in_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 5, 15, 37, 57, 399, DateTimeKind.Unspecified).AddTicks(9799), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 3, 37, 57, 399, DateTimeKind.Unspecified).AddTicks(9799), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 15, 37, 57, 399, DateTimeKind.Unspecified).AddTicks(9798), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 204,
                columns: new[] { "check_in_at", "check_out_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 5, 1, 37, 57, 400, DateTimeKind.Unspecified).AddTicks(287), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 9, 37, 57, 400, DateTimeKind.Unspecified).AddTicks(288), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 9, 37, 57, 400, DateTimeKind.Unspecified).AddTicks(287), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 1, 37, 57, 400, DateTimeKind.Unspecified).AddTicks(286), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 205,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 8, 9, 37, 57, 400, DateTimeKind.Unspecified).AddTicks(799), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 8, 1, 37, 57, 400, DateTimeKind.Unspecified).AddTicks(798), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "subscriptions",
                keyColumn: "id",
                keyValue: 101,
                column: "started_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 1, 37, 57, 399, DateTimeKind.Unspecified).AddTicks(7127), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "team_members",
                columns: new[] { "id", "created_at", "team_id", "updated_at", "user_id" },
                values: new object[,]
                {
                    { 1001, null, 1001, null, 103 },
                    { 1002, null, 1001, null, 104 },
                    { 1003, null, 1002, null, 205 },
                    { 1004, null, 1002, null, 206 }
                });

            migrationBuilder.InsertData(
                table: "work_areas",
                columns: new[] { "id", "created_at", "name", "updated_at" },
                values: new object[,]
                {
                    { 1001, null, "Emergencias", null },
                    { 1002, null, "Cuidados Intensivos (UCI)", null },
                    { 1003, null, "Cirugía", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "care_teams",
                keyColumn: "id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "care_teams",
                keyColumn: "id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "team_members",
                keyColumn: "id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "team_members",
                keyColumn: "id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "team_members",
                keyColumn: "id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "team_members",
                keyColumn: "id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "work_areas",
                keyColumn: "id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "work_areas",
                keyColumn: "id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "work_areas",
                keyColumn: "id",
                keyValue: 1003);

            migrationBuilder.InsertData(
                table: "care_teams",
                columns: new[] { "id", "created_at", "name", "organization_id", "status", "supervisor_id", "updated_at", "work_area_id" },
                values: new object[,]
                {
                    { 1, null, "Equipo de Emergencias Alpha", 101, "ACTIVE", 102, null, 1 },
                    { 2, null, "Equipo UCI Beta", 101, "ACTIVE", 207, null, 2 }
                });

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 101,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 1, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(6311), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 102,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 1, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(7237), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 203,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 1, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(7240), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 204,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 1, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(7348), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 101,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 7, 9, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(2916), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 7, 1, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(2689), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 102,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 8, 5, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(4084), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 7, 21, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(4081), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 103,
                columns: new[] { "check_in_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 5, 15, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(4091), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 3, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(4090), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 15, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(4088), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 204,
                columns: new[] { "check_in_at", "check_out_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 5, 1, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(5109), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 9, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(5110), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 9, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(5107), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 1, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(5105), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 205,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 8, 9, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(6158), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 8, 1, 33, 57, 761, DateTimeKind.Unspecified).AddTicks(6156), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "subscriptions",
                keyColumn: "id",
                keyValue: 101,
                column: "started_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 1, 33, 57, 760, DateTimeKind.Unspecified).AddTicks(7715), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "team_members",
                columns: new[] { "id", "created_at", "team_id", "updated_at", "user_id" },
                values: new object[,]
                {
                    { 1, null, 1, null, 103 },
                    { 2, null, 1, null, 104 },
                    { 3, null, 2, null, 205 },
                    { 4, null, 2, null, 206 }
                });

            migrationBuilder.InsertData(
                table: "work_areas",
                columns: new[] { "id", "created_at", "name", "updated_at" },
                values: new object[,]
                {
                    { 1, null, "Emergencias", null },
                    { 2, null, "Cuidados Intensivos (UCI)", null },
                    { 3, null, "Cirugía", null }
                });
        }
    }
}
