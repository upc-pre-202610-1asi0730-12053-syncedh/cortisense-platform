workspace "SyncedHealth Center Platform - Vue.js / ASP.NET Core Stack" {

    model {
        supervisor = person "Clinical Supervisor" "Monitors medical staff fatigue levels, manages care teams, and responds to clinical alerts."
        doctor     = person "Doctor / Medical Staff" "Registers vital signs, accepts or rejects shift assignments, and follows recovery plans."
        admin      = person "Organization Admin" "Manages the organization account, subscriptions, and invites new users."

        softwareSystem = softwareSystem "SyncedHealth Center Platform (CortiSense)" "Real-time clinical fatigue monitoring platform for healthcare organizations." {

            webApplication = container "CortiSense Web Application (CDN)" "Serves the compiled static assets and the Vue.js SPA to the browser." {
                tags "webapp"
                technology "HTML, CSS, JavaScript — Azure Static Web Apps"
            }

            singlePageApplication = container "CortiSense Single Page Application" "Provides the browser-based interface for monitoring fatigue, managing teams, and reviewing clinical events." {
                tags "spa"
                technology "JavaScript, Vue 3, Vite, Pinia, Vue-Router, Vue-i18n, Axios"
            }

            apiApplication = container "CortiSense Platform API" "Exposes the RESTful API that implements the clinical fatigue monitoring domain logic." {
                tags "api"
                technology "C#, ASP.NET Core 9, Entity Framework Core, JWT Bearer"

                iam           = component "IAM Bounded Context"                "Manages organizations, users, roles, and invitation-based onboarding flows." "ASP.NET Component"
                clinicalRisk  = component "Clinical Risk Assessment BC"         "Processes vital-sign readings, detects anomalies, generates clinical alerts, and evaluates risk." "ASP.NET Component"
                shiftCoord    = component "Shift Coordination BC"               "Manages care teams, team members, shift records, work areas, and specialties." "ASP.NET Component"
                staffRecovery = component "Staff Recovery BC"                   "Issues and tracks recovery plans and preventive actions for fatigued medical staff." "ASP.NET Component"
                auditComp     = component "Audit & Compliance BC"               "Records immutable audit log entries for all significant platform actions." "ASP.NET Component"
                subscription  = component "Subscription & Billing BC"           "Manages subscription plans, Stripe checkout sessions, and billing webhooks." "ASP.NET Component"
                shared        = component "Shared Bounded Context"              "Provides cross-cutting abstractions: base repository, unit-of-work, auditable-entity interface, and AppDbContext." "ASP.NET Component"
            }

            database = container "Database" "Persists all domain aggregates and read models." {
                tags "database"
                technology "Azure SQL Server (via Entity Framework Core Migrations)"
            }

            stripeGateway = container "Stripe Payment Gateway" "External SaaS used for subscription billing and checkout session management." {
                tags "external"
                technology "Stripe API"
            }

            resendService = container "Resend Email Service" "External transactional email provider used for invitation delivery." {
                tags "external"
                technology "Resend API"
            }
        }


        # Context level relationships
        supervisor         -> softwareSystem "Monitors fatigue, manages teams, resolves clinical alerts"
        doctor             -> softwareSystem "Records vital signs, accepts shifts, follows recovery plans"
        admin              -> softwareSystem "Manages organization, billing, and user invitations"

        # Container level relationships
        supervisor         -> webApplication  "Accesses via browser" "HTTPS"
        doctor             -> webApplication  "Accesses via browser" "HTTPS"
        admin              -> webApplication  "Accesses via browser" "HTTPS"
        webApplication     -> singlePageApplication "Serves SPA bundle to browser"
        singlePageApplication -> apiApplication "Makes API calls to" "JSON/HTTPS"
        apiApplication     -> database        "Reads from and writes to" "EF Core"
        apiApplication     -> stripeGateway   "Creates checkout sessions and handles webhook events" "HTTPS"
        apiApplication     -> resendService   "Sends invitation emails" "HTTPS"

        # Component-level relationships (within API Application)
        iam           -> database        "Reads and writes Organizations, Users, Invitations"
        clinicalRisk  -> database        "Reads and writes VitalSignReadings, Anomalies, ClinicalAlerts, RiskAssessments"
        shiftCoord    -> database        "Reads and writes CareTeams, TeamMembers, ShiftRecords, WorkAreas, Specialties"
        staffRecovery -> database        "Reads and writes RecoveryPlans, PreventiveActions"
        auditComp     -> database        "Writes AuditLog entries"
        subscription  -> database        "Reads and writes Plans, Subscriptions, CheckoutSessions"
        shared        -> database        "Configures ORM mapping rules and manages AppDbContext"

        # Cross-BC inter-dependencies
        clinicalRisk  -> auditComp       "Creates audit log on anomaly detection, alert creation, and risk evaluation"
        shiftCoord    -> auditComp       "Creates audit log on shift check-in and team changes"
        staffRecovery -> auditComp       "Creates audit log on recovery plan status transitions"
        iam           -> auditComp       "Creates audit log on user invitation and role changes"
        subscription  -> auditComp       "Creates audit log on subscription activation"
        staffRecovery -> shiftCoord      "Reads ShiftRecord to block shift when recovery plan is issued"
        iam           -> resendService   "Sends invitation email via Resend outbound service"
        subscription  -> stripeGateway   "Creates and validates Stripe checkout sessions"

        # All BCs depend on Shared
        iam           -> shared          "Implements IBaseRepository / IAuditableEntity / IUnitOfWork"
        clinicalRisk  -> shared          "Implements IBaseRepository / IAuditableEntity / IUnitOfWork"
        shiftCoord    -> shared          "Implements IBaseRepository / IAuditableEntity / IUnitOfWork"
        staffRecovery -> shared          "Implements IBaseRepository / IAuditableEntity / IUnitOfWork"
        auditComp     -> shared          "Implements IBaseRepository / IAuditableEntity / IUnitOfWork"
        subscription  -> shared          "Implements IBaseRepository / IAuditableEntity / IUnitOfWork"

        # SPA -> Component relationships
        singlePageApplication -> iam           "Sign-Up, Sign-In, Invitations, User Management"
        singlePageApplication -> clinicalRisk  "Record vital signs, view anomalies and clinical alerts"
        singlePageApplication -> shiftCoord    "Manage care teams, assign shifts, perform check-in"
        singlePageApplication -> staffRecovery "View and respond to recovery plans, manage preventive actions"
        singlePageApplication -> auditComp     "View audit trail of all platform actions"
        singlePageApplication -> subscription  "View and manage subscription plan, initiate billing checkout"
    }


    views {
        systemContext softwareSystem "SystemContext" {
            include *
            autoLayout
        }

        container softwareSystem "Containers" {
            include *
            autoLayout
        }

        component apiApplication "Components" {
            include *
            autoLayout
        }

        theme default

        styles {
            element "person" {
                shape Person
                background #08427B
                color #ffffff
            }

            element "webapp" {
                shape Box
                background #1168BD
                color #ffffff
            }

            element "spa" {
                shape WebBrowser
                background #00BB7A
                color #ffffff
            }

            element "api" {
                shape Box
                background #85BBF0
                color #000000
            }

            element "database" {
                shape Cylinder
                background #438DD5
                color #ffffff
            }

            element "external" {
                shape Box
                background #999999
                color #ffffff
            }
        }
    }
}
