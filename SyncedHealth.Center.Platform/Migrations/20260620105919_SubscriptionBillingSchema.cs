using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace SyncedHealth.Center.Platform.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionBillingSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "cancelled_at",
                table: "subscriptions",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "stripe_customer_id",
                table: "subscriptions",
                type: "varchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "stripe_subscription_id",
                table: "subscriptions",
                type: "varchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "billing_period",
                table: "plans",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "cancelled_at",
                table: "checkout_sessions",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "completed_at",
                table: "checkout_sessions",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "error_message",
                table: "checkout_sessions",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "failed_at",
                table: "checkout_sessions",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "stripe_customer_id",
                table: "checkout_sessions",
                type: "varchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "stripe_session_id",
                table: "checkout_sessions",
                type: "varchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "stripe_subscription_id",
                table: "checkout_sessions",
                type: "varchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "stripe_url",
                table: "checkout_sessions",
                type: "varchar(600)",
                maxLength: 600,
                nullable: true);
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cancelled_at",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "stripe_customer_id",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "stripe_subscription_id",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "cancelled_at",
                table: "checkout_sessions");

            migrationBuilder.DropColumn(
                name: "completed_at",
                table: "checkout_sessions");

            migrationBuilder.DropColumn(
                name: "error_message",
                table: "checkout_sessions");

            migrationBuilder.DropColumn(
                name: "failed_at",
                table: "checkout_sessions");

            migrationBuilder.DropColumn(
                name: "stripe_customer_id",
                table: "checkout_sessions");

            migrationBuilder.DropColumn(
                name: "stripe_session_id",
                table: "checkout_sessions");

            migrationBuilder.DropColumn(
                name: "stripe_subscription_id",
                table: "checkout_sessions");

            migrationBuilder.DropColumn(
                name: "stripe_url",
                table: "checkout_sessions");

            migrationBuilder.AlterColumn<string>(
                name: "billing_period",
                table: "plans",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);
        }
    }
}
