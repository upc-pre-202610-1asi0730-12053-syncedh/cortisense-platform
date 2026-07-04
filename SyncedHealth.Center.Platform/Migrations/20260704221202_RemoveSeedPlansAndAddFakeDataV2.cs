using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SyncedHealth.Center.Platform.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSeedPlansAndAddFakeDataV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "plans",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "plans",
                keyColumn: "id",
                keyValue: 102);

            migrationBuilder.InsertData(
                table: "recovery_plans",
                columns: new[] { "id", "created_at", "description", "medical_staff_id", "status", "suggested_rest_days", "updated_at" },
                values: new object[] { 202, null, "Fatiga media detectada", 205, "COMPLETED", 1, null });

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

            migrationBuilder.InsertData(
                table: "risk_assessments",
                columns: new[] { "id", "created_at", "fatigue_level", "heart_rate", "hrv", "last_updated_at", "organization_id", "risk_level", "updated_at", "user_id" },
                values: new object[,]
                {
                    { 203, null, 45, 95, 40, new DateTimeOffset(new DateTime(2026, 7, 4, 22, 12, 1, 562, DateTimeKind.Unspecified).AddTicks(4435), new TimeSpan(0, 0, 0, 0, 0)), 101, "MEDIUM", null, 205 },
                    { 204, null, 10, 65, 70, new DateTimeOffset(new DateTime(2026, 7, 4, 22, 12, 1, 562, DateTimeKind.Unspecified).AddTicks(4436), new TimeSpan(0, 0, 0, 0, 0)), 101, "LOW", null, 206 }
                });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 101,
                columns: new[] { "scheduled_end", "scheduled_start", "work_area_id" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 6, 6, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(1750), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 22, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(1599), new TimeSpan(0, 0, 0, 0, 0)), 2 });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 102,
                columns: new[] { "scheduled_end", "scheduled_start", "work_area_id" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 7, 2, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(2753), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 18, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(2750), new TimeSpan(0, 0, 0, 0, 0)), 1 });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 103,
                columns: new[] { "check_in_at", "scheduled_end", "scheduled_start", "work_area_id" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 4, 12, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(2756), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 0, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(2755), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 4, 12, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(2754), new TimeSpan(0, 0, 0, 0, 0)), 3 });

            migrationBuilder.InsertData(
                table: "shift_records",
                columns: new[] { "id", "check_in_at", "check_out_at", "created_at", "organization_id", "scheduled_end", "scheduled_start", "status", "type", "updated_at", "user_id", "work_area_id" },
                values: new object[,]
                {
                    { 204, new DateTimeOffset(new DateTime(2026, 7, 3, 22, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(3546), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 4, 6, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(3547), new TimeSpan(0, 0, 0, 0, 0)), null, 101, new DateTimeOffset(new DateTime(2026, 7, 4, 6, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(3545), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 3, 22, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(3544), new TimeSpan(0, 0, 0, 0, 0)), "COMPLETED", "DAY", null, 205, 2 },
                    { 205, null, null, null, 101, new DateTimeOffset(new DateTime(2026, 7, 7, 6, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(4365), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 22, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(4364), new TimeSpan(0, 0, 0, 0, 0)), "SCHEDULED", "DAY", null, 206, 1 }
                });

            migrationBuilder.UpdateData(
                table: "subscriptions",
                keyColumn: "id",
                keyValue: 101,
                columns: new[] { "plan_id", "started_at" },
                values: new object[] { 3, new DateTimeOffset(new DateTime(2026, 7, 4, 22, 12, 1, 561, DateTimeKind.Unspecified).AddTicks(401), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "activated_at", "cancelled_at", "created_at", "email", "first_name", "last_name", "organization_id", "password_hash", "phone", "registration_status", "role", "specialty_id", "status", "updated_at", "work_area_id" },
                values: new object[,]
                {
                    { 205, null, null, null, "atorres101@hospitalcentral.com", "Ana", "Torres", 101, "dummyhash", null, "COMPLETED", "Medical Staff", null, "ACTIVE", null, null },
                    { 206, null, null, null, "lramirez101@hospitalcentral.com", "Luis", "Ramirez", 101, "dummyhash", null, "COMPLETED", "Medical Staff", null, "ACTIVE", null, null },
                    { 207, null, null, null, "mlopez101@hospitalcentral.com", "Maria", "Lopez", 101, "dummyhash", null, "COMPLETED", "Clinical Supervisor", null, "ACTIVE", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "recovery_plans",
                keyColumn: "id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 207);

            migrationBuilder.InsertData(
                table: "plans",
                columns: new[] { "id", "billing_period", "code", "created_at", "currency", "data_history_days", "disabled_modules", "enabled_modules", "feature_keys", "max_doctors", "max_supervisors", "max_teams", "max_work_areas", "monthly_invitations", "name", "price", "recommended", "support_level", "updated_at" },
                values: new object[,]
                {
                    { 101, "Monthly", "FREE_101", null, "USD", 7, "[]", "[]", "[]", null, 1, 1, 1, 5, "Free Trial Seed", 0m, false, "Standard", null },
                    { 102, "Yearly", "PREMIUM_101", null, "USD", 365, "[]", "[]", "[]", null, 50, 100, 50, 500, "Premium Enterprise Seed", 999.99m, true, "Priority", null }
                });

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
                columns: new[] { "scheduled_end", "scheduled_start", "work_area_id" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 6, 5, 5, 49, 664, DateTimeKind.Unspecified).AddTicks(7159), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 21, 5, 49, 664, DateTimeKind.Unspecified).AddTicks(6742), new TimeSpan(0, 0, 0, 0, 0)), 101 });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 102,
                columns: new[] { "scheduled_end", "scheduled_start", "work_area_id" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 7, 1, 5, 49, 665, DateTimeKind.Unspecified).AddTicks(190), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 17, 5, 49, 665, DateTimeKind.Unspecified).AddTicks(180), new TimeSpan(0, 0, 0, 0, 0)), 101 });

            migrationBuilder.UpdateData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 103,
                columns: new[] { "check_in_at", "scheduled_end", "scheduled_start", "work_area_id" },
                values: new object[] { new DateTimeOffset(new DateTime(2026, 7, 4, 11, 5, 49, 665, DateTimeKind.Unspecified).AddTicks(196), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 4, 23, 5, 49, 665, DateTimeKind.Unspecified).AddTicks(194), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 4, 11, 5, 49, 665, DateTimeKind.Unspecified).AddTicks(193), new TimeSpan(0, 0, 0, 0, 0)), 102 });

            migrationBuilder.UpdateData(
                table: "subscriptions",
                keyColumn: "id",
                keyValue: 101,
                columns: new[] { "plan_id", "started_at" },
                values: new object[] { 102, new DateTimeOffset(new DateTime(2026, 7, 4, 21, 5, 49, 664, DateTimeKind.Unspecified).AddTicks(4360), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
