using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncedHealth.Center.Platform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLatestModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 101,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 19, 46, 6, 979, DateTimeKind.Unspecified).AddTicks(2126), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 102,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 19, 46, 6, 979, DateTimeKind.Unspecified).AddTicks(5107), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 203,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 19, 46, 6, 979, DateTimeKind.Unspecified).AddTicks(5110), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 204,
                column: "last_updated_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 19, 46, 6, 979, DateTimeKind.Unspecified).AddTicks(5111), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 101,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 8, 3, 46, 6, 978, DateTimeKind.Unspecified).AddTicks(4778), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 7, 19, 46, 6, 978, DateTimeKind.Unspecified).AddTicks(3193), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 102,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 8, 23, 46, 6, 978, DateTimeKind.Unspecified).AddTicks(7389), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 8, 15, 46, 6, 978, DateTimeKind.Unspecified).AddTicks(7381), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 103,
                columns: new[] { "check_in_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 6, 9, 46, 6, 978, DateTimeKind.Unspecified).AddTicks(7395), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 21, 46, 6, 978, DateTimeKind.Unspecified).AddTicks(7394), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 9, 46, 6, 978, DateTimeKind.Unspecified).AddTicks(7392), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 204,
                columns: new[] { "check_in_at", "check_out_at", "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 5, 19, 46, 6, 978, DateTimeKind.Unspecified).AddTicks(9558), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 3, 46, 6, 978, DateTimeKind.Unspecified).AddTicks(9559), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 3, 46, 6, 978, DateTimeKind.Unspecified).AddTicks(9555), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 19, 46, 6, 978, DateTimeKind.Unspecified).AddTicks(9551), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 205,
                columns: new[] { "scheduled_end", "scheduled_start" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 9, 3, 46, 6, 979, DateTimeKind.Unspecified).AddTicks(1695), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 8, 19, 46, 6, 979, DateTimeKind.Unspecified).AddTicks(1691), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "subscriptions",
                keyColumn: "id",
                keyValue: 101,
                column: "started_at",
                value: new DateTimeOffset(new DateTime(2026, 7, 6, 19, 46, 6, 977, DateTimeKind.Unspecified).AddTicks(3422), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
