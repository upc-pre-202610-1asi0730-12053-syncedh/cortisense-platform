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
            migrationBuilder.Sql("""
                INSERT INTO `plans` (
                    `id`, `code`, `name`, `price`, `currency`, `billing_period`,
                    `max_doctors`, `max_supervisors`, `max_teams`, `max_work_areas`,
                    `monthly_invitations`, `data_history_days`, `support_level`, `recommended`,
                    `feature_keys`, `enabled_modules`, `disabled_modules`, `created_at`, `updated_at`
                )
                VALUES (
                    1, 'basic', 'Basic', 79.0, 'USD', 'Monthly',
                    30, 6, 6, 6,
                    80, 45, 'Standard', FALSE,
                    '["subscription.plans.features.basic.doctors","subscription.plans.features.basic.supervisors","subscription.plans.features.basic.teams","subscription.plans.features.common.admin-dashboard","subscription.plans.features.common.staff-management","subscription.plans.features.common.team-management","subscription.plans.features.common.shift-management"]',
                    '["ADMIN_DASHBOARD","STAFF_MANAGEMENT","TEAM_MANAGEMENT","INVITATIONS","DOCTOR_HEALTH","VITAL_SIGNS","SHIFT_MANAGEMENT","BASIC_RISK_ASSESSMENT","BASIC_CLINICAL_ALERTS","BASIC_REPORTS"]',
                    '["VITAL_SIGN_ANOMALIES","PREVENTIVE_ACTIONS","DOCTOR_RECOVERY","ADMIN_AUDIT","ADVANCED_REPORTS","MULTI_SITE","EXTERNAL_INTEGRATIONS","ENTERPRISE_API"]',
                    '2026-06-20 00:00:00',
                    NULL
                )
                ON DUPLICATE KEY UPDATE
                    `code` = VALUES(`code`),
                    `name` = VALUES(`name`),
                    `price` = VALUES(`price`),
                    `currency` = VALUES(`currency`),
                    `billing_period` = VALUES(`billing_period`),
                    `max_doctors` = VALUES(`max_doctors`),
                    `max_supervisors` = VALUES(`max_supervisors`),
                    `max_teams` = VALUES(`max_teams`),
                    `max_work_areas` = VALUES(`max_work_areas`),
                    `monthly_invitations` = VALUES(`monthly_invitations`),
                    `data_history_days` = VALUES(`data_history_days`),
                    `support_level` = VALUES(`support_level`),
                    `recommended` = VALUES(`recommended`),
                    `feature_keys` = VALUES(`feature_keys`),
                    `enabled_modules` = VALUES(`enabled_modules`),
                    `disabled_modules` = VALUES(`disabled_modules`),
                    `updated_at` = CURRENT_TIMESTAMP;
                """);

            migrationBuilder.Sql("""
                INSERT INTO `plans` (
                    `id`, `code`, `name`, `price`, `currency`, `billing_period`,
                    `max_doctors`, `max_supervisors`, `max_teams`, `max_work_areas`,
                    `monthly_invitations`, `data_history_days`, `support_level`, `recommended`,
                    `feature_keys`, `enabled_modules`, `disabled_modules`, `created_at`, `updated_at`
                )
                VALUES (
                    2, 'professional', 'Professional', 149.0, 'USD', 'Monthly',
                    100, 20, 20, 20,
                    300, 180, 'Priority', TRUE,
                    '["subscription.plans.features.professional.doctors","subscription.plans.features.professional.supervisors","subscription.plans.features.professional.teams","subscription.plans.features.common.admin-dashboard","subscription.plans.features.common.staff-management","subscription.plans.features.common.team-management","subscription.plans.features.common.shift-management","subscription.plans.features.professional.risk-assessment","subscription.plans.features.professional.clinical-alerts","subscription.plans.features.professional.recovery"]',
                    '["ADMIN_DASHBOARD","STAFF_MANAGEMENT","TEAM_MANAGEMENT","INVITATIONS","DOCTOR_HEALTH","VITAL_SIGNS","SHIFT_MANAGEMENT","BASIC_RISK_ASSESSMENT","BASIC_CLINICAL_ALERTS","BASIC_REPORTS","VITAL_SIGN_ANOMALIES","PREVENTIVE_ACTIONS","DOCTOR_RECOVERY","ADVANCED_REPORTS"]',
                    '["ADMIN_AUDIT","MULTI_SITE","EXTERNAL_INTEGRATIONS","ENTERPRISE_API"]',
                    '2026-06-20 00:00:00',
                    NULL
                )
                ON DUPLICATE KEY UPDATE
                    `code` = VALUES(`code`),
                    `name` = VALUES(`name`),
                    `price` = VALUES(`price`),
                    `currency` = VALUES(`currency`),
                    `billing_period` = VALUES(`billing_period`),
                    `max_doctors` = VALUES(`max_doctors`),
                    `max_supervisors` = VALUES(`max_supervisors`),
                    `max_teams` = VALUES(`max_teams`),
                    `max_work_areas` = VALUES(`max_work_areas`),
                    `monthly_invitations` = VALUES(`monthly_invitations`),
                    `data_history_days` = VALUES(`data_history_days`),
                    `support_level` = VALUES(`support_level`),
                    `recommended` = VALUES(`recommended`),
                    `feature_keys` = VALUES(`feature_keys`),
                    `enabled_modules` = VALUES(`enabled_modules`),
                    `disabled_modules` = VALUES(`disabled_modules`),
                    `updated_at` = CURRENT_TIMESTAMP;
                """);

            migrationBuilder.Sql("""
                INSERT INTO `plans` (
                    `id`, `code`, `name`, `price`, `currency`, `billing_period`,
                    `max_doctors`, `max_supervisors`, `max_teams`, `max_work_areas`,
                    `monthly_invitations`, `data_history_days`, `support_level`, `recommended`,
                    `feature_keys`, `enabled_modules`, `disabled_modules`, `created_at`, `updated_at`
                )
                VALUES (
                    3, 'enterprise', 'Enterprise', 299.0, 'USD', 'Monthly',
                    500, 100, 100, 100,
                    1000, 365, 'Dedicated', FALSE,
                    '["subscription.plans.features.enterprise.doctors","subscription.plans.features.enterprise.supervisors","subscription.plans.features.enterprise.teams","subscription.plans.features.common.admin-dashboard","subscription.plans.features.common.staff-management","subscription.plans.features.common.team-management","subscription.plans.features.common.shift-management","subscription.plans.features.enterprise.audit","subscription.plans.features.enterprise.integrations","subscription.plans.features.enterprise.api"]',
                    '["ADMIN_DASHBOARD","STAFF_MANAGEMENT","TEAM_MANAGEMENT","INVITATIONS","DOCTOR_HEALTH","VITAL_SIGNS","SHIFT_MANAGEMENT","BASIC_RISK_ASSESSMENT","BASIC_CLINICAL_ALERTS","BASIC_REPORTS","VITAL_SIGN_ANOMALIES","PREVENTIVE_ACTIONS","DOCTOR_RECOVERY","ADMIN_AUDIT","ADVANCED_REPORTS","MULTI_SITE","EXTERNAL_INTEGRATIONS","ENTERPRISE_API"]',
                    '[]',
                    '2026-06-20 00:00:00',
                    NULL
                )
                ON DUPLICATE KEY UPDATE
                    `code` = VALUES(`code`),
                    `name` = VALUES(`name`),
                    `price` = VALUES(`price`),
                    `currency` = VALUES(`currency`),
                    `billing_period` = VALUES(`billing_period`),
                    `max_doctors` = VALUES(`max_doctors`),
                    `max_supervisors` = VALUES(`max_supervisors`),
                    `max_teams` = VALUES(`max_teams`),
                    `max_work_areas` = VALUES(`max_work_areas`),
                    `monthly_invitations` = VALUES(`monthly_invitations`),
                    `data_history_days` = VALUES(`data_history_days`),
                    `support_level` = VALUES(`support_level`),
                    `recommended` = VALUES(`recommended`),
                    `feature_keys` = VALUES(`feature_keys`),
                    `enabled_modules` = VALUES(`enabled_modules`),
                    `disabled_modules` = VALUES(`disabled_modules`),
                    `updated_at` = CURRENT_TIMESTAMP;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DELETE FROM `plans`
                WHERE `id` IN (1, 2, 3)
                AND `code` IN ('basic', 'professional', 'enterprise');
                """);
        }
    }
}