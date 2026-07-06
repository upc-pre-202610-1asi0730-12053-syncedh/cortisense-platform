using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncedHealth.Center.Platform.Migrations
{
    /// <inheritdoc />
    public partial class FixDynamicSeedDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 101,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 102,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 203,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 204,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 101,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 7, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 102,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 8, 4, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 7, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 103,
                columns: new[] { "check_in_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 2, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 204,
                columns: new[] { "check_in_at", "check_out_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 205,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 8, 8, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "subscriptions",
                keyColumn: "id",
                keyValue: 101,
                column: "started_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 101,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 19, 50, 11, 746, DateTimeKind.Unspecified).AddTicks(8589), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 102,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 19, 50, 11, 746, DateTimeKind.Unspecified).AddTicks(9606), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 203,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 19, 50, 11, 746, DateTimeKind.Unspecified).AddTicks(9607), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 204,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 19, 50, 11, 746, DateTimeKind.Unspecified).AddTicks(9608), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 101,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 8, 3, 50, 11, 745, DateTimeKind.Unspecified).AddTicks(7679), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 7, 19, 50, 11, 745, DateTimeKind.Unspecified).AddTicks(7434), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 102,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 8, 23, 50, 11, 746, DateTimeKind.Unspecified).AddTicks(3811), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 8, 15, 50, 11, 746, DateTimeKind.Unspecified).AddTicks(3795), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 103,
                columns: new[] { "check_in_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 6, 9, 50, 11, 746, DateTimeKind.Unspecified).AddTicks(3821), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 21, 50, 11, 746, DateTimeKind.Unspecified).AddTicks(3820), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 9, 50, 11, 746, DateTimeKind.Unspecified).AddTicks(3818), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 204,
                columns: new[] { "check_in_at", "check_out_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 5, 19, 50, 11, 746, DateTimeKind.Unspecified).AddTicks(6781), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 3, 50, 11, 746, DateTimeKind.Unspecified).AddTicks(6783), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 3, 50, 11, 746, DateTimeKind.Unspecified).AddTicks(6779), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 19, 50, 11, 746, DateTimeKind.Unspecified).AddTicks(6775), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 205,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 9, 3, 50, 11, 746, DateTimeKind.Unspecified).AddTicks(8202), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 8, 19, 50, 11, 746, DateTimeKind.Unspecified).AddTicks(8201), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "subscriptions",
                keyColumn: "id",
                keyValue: 101,
                column: "started_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 19, 50, 11, 745, DateTimeKind.Unspecified).AddTicks(539), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
