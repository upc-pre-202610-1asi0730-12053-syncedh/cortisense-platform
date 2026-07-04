using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncedHealth.Center.Platform.Migrations
{
    /// <inheritdoc />
    public partial class MakeWorkAreasGlobal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "i_x_work_areas_organization_id",
                table: "work_areas");

            migrationBuilder.DropColumn(
                name: "organization_id",
                table: "work_areas");

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 101,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 21, 5, 49, 665, DateTimeKind.Unspecified).AddTicks(5736), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 102,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 21, 5, 49, 665, DateTimeKind.Unspecified).AddTicks(7869), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 101,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 6, 5, 5, 49, 664, DateTimeKind.Unspecified).AddTicks(7159), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 21, 5, 49, 664, DateTimeKind.Unspecified).AddTicks(6742), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 102,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 7, 1, 5, 49, 665, DateTimeKind.Unspecified).AddTicks(190), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 17, 5, 49, 665, DateTimeKind.Unspecified).AddTicks(180), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 103,
                columns: new[] { "check_in_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 4, 11, 5, 49, 665, DateTimeKind.Unspecified).AddTicks(196), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 4, 23, 5, 49, 665, DateTimeKind.Unspecified).AddTicks(194), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 4, 11, 5, 49, 665, DateTimeKind.Unspecified).AddTicks(193), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "subscriptions",
                keyColumn: "id",
                keyValue: 101,
                column: "started_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 21, 5, 49, 664, DateTimeKind.Unspecified).AddTicks(4360), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "organization_id",
                table: "work_areas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 101,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 17, 36, 53, 141, DateTimeKind.Unspecified).AddTicks(4165), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 102,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 17, 36, 53, 141, DateTimeKind.Unspecified).AddTicks(5935), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 101,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 6, 1, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(8351), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 17, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(8082), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 102,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 6, 21, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(9851), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 13, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(9845), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 103,
                columns: new[] { "check_in_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 4, 7, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(9854), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 4, 19, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(9853), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 4, 7, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(9853), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "subscriptions",
                keyColumn: "id",
                keyValue: 101,
                column: "started_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 4, 17, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(6413), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "i_x_work_areas_organization_id",
                table: "work_areas",
                column: "organization_id");
        }
    }
}
