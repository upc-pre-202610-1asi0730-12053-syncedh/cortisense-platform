using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SyncedHealth.Center.Platform.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "organizations",
                columns: new[] { "id", "activated_at", "address", "cancelled_at", "created_at", "email", "name", "phone", "registration_status", "ruc", "status", "updated_at" },
                values: new object[] { 101, null, "Av. Principal 123", null, null, "admin101@hospitalcentral.com", "Hospital Central CortiSense", "555-0101", "COMPLETED", "1012345678901", "ACTIVE", null });

            migrationBuilder.InsertData(
                table: "plans",
                columns: new[] { "id", "billing_period", "code", "created_at", "currency", "data_history_days", "disabled_modules", "enabled_modules", "feature_keys", "max_doctors", "max_supervisors", "max_teams", "max_work_areas", "monthly_invitations", "name", "price", "recommended", "support_level", "updated_at" },
                values: new object[,]
                {
                    { 101, "Monthly", "FREE_101", null, "USD", 7, "[]", "[]", "[]", null, 1, 1, 1, 5, "Free Trial Seed", 0m, false, "Standard", null },
                    { 102, "Yearly", "PREMIUM_101", null, "USD", 365, "[]", "[]", "[]", null, 50, 100, 50, 500, "Premium Enterprise Seed", 999.99m, true, "Priority", null }
                });

            migrationBuilder.InsertData(
                table: "recovery_plans",
                columns: new[] { "id", "created_at", "description", "medical_staff_id", "status", "suggested_rest_days", "updated_at" },
                values: new object[] { 101, null, "Alta Fatiga", 103, "PENDING", 1, null });

            migrationBuilder.InsertData(
                table: "risk_assessments",
                columns: new[] { "id", "created_at", "fatigue_level", "heart_rate", "hrv", "last_updated_at", "organization_id", "risk_level", "updated_at", "user_id" },
                values: new object[,]
                {
                    { 101, null, 85, 120, 20, new DateTimeOffset(new DateTime(2026, 7, 4, 17, 36, 53, 141, DateTimeKind.Unspecified).AddTicks(4165), new TimeSpan(0, 0, 0, 0, 0)), 101, "HIGH", null, 103 },
                    { 102, null, 20, 70, 60, new DateTimeOffset(new DateTime(2026, 7, 4, 17, 36, 53, 141, DateTimeKind.Unspecified).AddTicks(5935), new TimeSpan(0, 0, 0, 0, 0)), 101, "LOW", null, 104 }
                });

            migrationBuilder.InsertData(
                table: "shift_records",
                columns: new[] { "id", "check_in_at", "check_out_at", "created_at", "organization_id", "scheduled_end", "scheduled_start", "status", "type", "updated_at", "user_id", "work_area_id" },
                values: new object[,]
                {
                    { 101, null, null, null, 101, new DateTimeOffset(new DateTime(2026, 7, 6, 1, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(8351), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 5, 17, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(8082), new TimeSpan(0, 0, 0, 0, 0)), "SCHEDULED", "DAY", null, 103, 101 },
                    { 102, null, null, null, 101, new DateTimeOffset(new DateTime(2026, 7, 6, 21, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(9851), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 6, 13, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(9845), new TimeSpan(0, 0, 0, 0, 0)), "SCHEDULED", "NIGHT", null, 104, 101 },
                    { 103, new DateTimeOffset(new DateTime(2026, 7, 4, 7, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(9854), new TimeSpan(0, 0, 0, 0, 0)), null, null, 101, new DateTimeOffset(new DateTime(2026, 7, 4, 19, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(9853), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 7, 4, 7, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(9853), new TimeSpan(0, 0, 0, 0, 0)), "IN_PROGRESS", "EMERGENCY", null, 103, 102 }
                });

            migrationBuilder.InsertData(
                table: "subscriptions",
                columns: new[] { "id", "cancelled_at", "created_at", "organization_id", "plan_id", "started_at", "status", "stripe_customer_id", "stripe_subscription_id", "updated_at" },
                values: new object[] { 101, null, null, 101, 102, new DateTimeOffset(new DateTime(2026, 7, 4, 17, 36, 53, 140, DateTimeKind.Unspecified).AddTicks(6413), new TimeSpan(0, 0, 0, 0, 0)), "Active", null, null, null });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "activated_at", "cancelled_at", "created_at", "email", "first_name", "last_name", "organization_id", "password_hash", "phone", "registration_status", "role", "specialty_id", "status", "updated_at", "work_area_id" },
                values: new object[,]
                {
                    { 101, null, null, null, "admin101@hospitalcentral.com", "Admin", "User", 101, "dummyhash", null, "COMPLETED", "Admin", null, "ACTIVE", null, null },
                    { 102, null, null, null, "supervisor101@hospitalcentral.com", "Carlos", "Supervisor", 101, "dummyhash", null, "COMPLETED", "Clinical Supervisor", null, "ACTIVE", null, null },
                    { 103, null, null, null, "lgomez101@hospitalcentral.com", "Laura", "Gomez", 101, "dummyhash", null, "COMPLETED", "Medical Staff", null, "ACTIVE", null, null },
                    { 104, null, null, null, "jperez101@hospitalcentral.com", "Juan", "Perez", 101, "dummyhash", null, "COMPLETED", "Medical Staff", null, "ACTIVE", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "organizations",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "plans",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "plans",
                keyColumn: "id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "recovery_plans",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "risk_assessments",
                keyColumn: "id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "shift_records",
                keyColumn: "id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "subscriptions",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 104);
        }
    }
}
