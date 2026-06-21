using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncedHealth.Center.Platform.Migrations
{
    /// <inheritdoc />
    public partial class StaffRecoveryAlignmentSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "i_x_recovery_plans_medical_staff_id",
                table: "recovery_plans",
                column: "medical_staff_id");

            migrationBuilder.CreateIndex(
                name: "i_x_recovery_plans_status",
                table: "recovery_plans",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "i_x_recovery_plans_suggested_rest_days",
                table: "recovery_plans",
                column: "suggested_rest_days");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "i_x_recovery_plans_medical_staff_id",
                table: "recovery_plans");

            migrationBuilder.DropIndex(
                name: "i_x_recovery_plans_status",
                table: "recovery_plans");

            migrationBuilder.DropIndex(
                name: "i_x_recovery_plans_suggested_rest_days",
                table: "recovery_plans");
        }
    }
}
