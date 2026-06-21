using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncedHealth.Center.Platform.Migrations
{
    /// <inheritdoc />
    public partial class SeedSubscriptionPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "plans",
                columns:
                [
                    "id",
                    "code",
                    "name",
                    "price",
                    "currency",
                    "billing_period",
                    "max_doctors",
                    "max_supervisors",
                    "max_teams",
                    "max_work_areas",
                    "monthly_invitations",
                    "data_history_days",
                    "support_level",
                    "recommended",
                    "feature_keys",
                    "enabled_modules",
                    "disabled_modules",
                    "created_at",
                    "updated_at"
                ],
                values: new object[,]
                {
                    {
                        1,
                        "basic",
                        "Basic",
                        79.00m,
                        "USD",
                        "Monthly",
                        30,
                        6,
                        6,
                        6,
                        80,
                        45,
                        "Standard",
                        false,
                        "[\"subscription.plans.features.basic.doctors\",\"subscription.plans.features.basic.supervisors\",\"subscription.plans.features.basic.teams\",\"subscription.plans.features.common.admin-dashboard\",\"subscription.plans.features.common.staff-management\",\"subscription.plans.features.common.team-management\",\"subscription.plans.features.common.shift-management\"]",
                        "[\"ADMIN_DASHBOARD\",\"STAFF_MANAGEMENT\",\"TEAM_MANAGEMENT\",\"INVITATIONS\",\"DOCTOR_HEALTH\",\"VITAL_SIGNS\",\"SHIFT_MANAGEMENT\",\"BASIC_RISK_ASSESSMENT\",\"BASIC_CLINICAL_ALERTS\",\"BASIC_REPORTS\"]",
                        "[\"VITAL_SIGN_ANOMALIES\",\"PREVENTIVE_ACTIONS\",\"DOCTOR_RECOVERY\",\"ADMIN_AUDIT\",\"ADVANCED_REPORTS\",\"MULTI_SITE\",\"EXTERNAL_INTEGRATIONS\",\"ENTERPRISE_API\"]",
                        new DateTimeOffset(new DateTime(2026, 6, 20, 0, 0, 0, DateTimeKind.Unspecified), TimeSpan.Zero),
                        null
                    },
                    {
                        2,
                        "professional",
                        "Professional",
                        149.00m,
                        "USD",
                        "Monthly",
                        100,
                        20,
                        20,
                        20,
                        300,
                        180,
                        "Priority",
                        true,
                        "[\"subscription.plans.features.professional.doctors\",\"subscription.plans.features.professional.supervisors\",\"subscription.plans.features.professional.teams\",\"subscription.plans.features.common.admin-dashboard\",\"subscription.plans.features.common.staff-management\",\"subscription.plans.features.common.team-management\",\"subscription.plans.features.common.shift-management\",\"subscription.plans.features.professional.risk-assessment\",\"subscription.plans.features.professional.clinical-alerts\",\"subscription.plans.features.professional.recovery\"]",
                        "[\"ADMIN_DASHBOARD\",\"STAFF_MANAGEMENT\",\"TEAM_MANAGEMENT\",\"INVITATIONS\",\"DOCTOR_HEALTH\",\"VITAL_SIGNS\",\"SHIFT_MANAGEMENT\",\"BASIC_RISK_ASSESSMENT\",\"BASIC_CLINICAL_ALERTS\",\"BASIC_REPORTS\",\"VITAL_SIGN_ANOMALIES\",\"PREVENTIVE_ACTIONS\",\"DOCTOR_RECOVERY\",\"ADVANCED_REPORTS\"]",
                        "[\"ADMIN_AUDIT\",\"MULTI_SITE\",\"EXTERNAL_INTEGRATIONS\",\"ENTERPRISE_API\"]",
                        new DateTimeOffset(new DateTime(2026, 6, 20, 0, 0, 0, DateTimeKind.Unspecified), TimeSpan.Zero),
                        null
                    },
                    {
                        3,
                        "enterprise",
                        "Enterprise",
                        299.00m,
                        "USD",
                        "Monthly",
                        500,
                        100,
                        100,
                        100,
                        1000,
                        365,
                        "Dedicated",
                        false,
                        "[\"subscription.plans.features.enterprise.doctors\",\"subscription.plans.features.enterprise.supervisors\",\"subscription.plans.features.enterprise.teams\",\"subscription.plans.features.common.admin-dashboard\",\"subscription.plans.features.common.staff-management\",\"subscription.plans.features.common.team-management\",\"subscription.plans.features.common.shift-management\",\"subscription.plans.features.enterprise.audit\",\"subscription.plans.features.enterprise.integrations\",\"subscription.plans.features.enterprise.api\"]",
                        "[\"ADMIN_DASHBOARD\",\"STAFF_MANAGEMENT\",\"TEAM_MANAGEMENT\",\"INVITATIONS\",\"DOCTOR_HEALTH\",\"VITAL_SIGNS\",\"SHIFT_MANAGEMENT\",\"BASIC_RISK_ASSESSMENT\",\"BASIC_CLINICAL_ALERTS\",\"BASIC_REPORTS\",\"VITAL_SIGN_ANOMALIES\",\"PREVENTIVE_ACTIONS\",\"DOCTOR_RECOVERY\",\"ADMIN_AUDIT\",\"ADVANCED_REPORTS\",\"MULTI_SITE\",\"EXTERNAL_INTEGRATIONS\",\"ENTERPRISE_API\"]",
                        "[]",
                        new DateTimeOffset(new DateTime(2026, 6, 20, 0, 0, 0, DateTimeKind.Unspecified), TimeSpan.Zero),
                        null
                    }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "plans",
                keyColumn: "id",
                keyValues: [1, 2, 3]);
        }
    }
}