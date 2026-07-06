# SyncedHealth Center Platform (CortiSense) — REST API Technical Stories


## Overview


This document contains API-focused technical stories intended for frontend developers integrating with the CortiSense Platform REST API (ASP.NET Core 9).


Common conventions
- Base path: `/api/v1`
- All protected endpoints require a `Authorization: Bearer <token>` header obtained from the sign-in flow.
- List endpoints return `200 OK` with an empty array when no results are found.
- Create endpoints return `201 Created` with the created resource on success.
- Update/Patch endpoints return `200 OK` with the updated resource on success.
- `404 Not Found` is returned when a requested resource does not exist.
- `400 Bad Request` is returned on validation or business-rule violations.


---


## Bounded Context: IAM (Identity, Access & Organizations)


The IAM context manages organization registration, user accounts, invitations, and authentication.


### TS-IAM-001 — Sign In
As a frontend developer, I want to authenticate a user through the API so that I can obtain a JWT token and populate the client session with the user's identity and role.


Acceptance criteria:
- Scenario: Successful sign-in
    - Given a POST request to `/api/v1/authentication/sign-in` is received with body: `{ email, password }`
    - When the credentials are valid
    - Then the API responds `200 OK` and returns the authenticated user resource including the JWT token

- Scenario: Invalid credentials
    - Given a POST request to `/api/v1/authentication/sign-in` is received with wrong credentials
    - When the API cannot authenticate the user
    - Then the API responds `401 Unauthorized` or `400 Bad Request`


---


### TS-IAM-002 — Sign Up (Organization + Admin User)
As a frontend developer, I want to register a new organization and its administrator through the API so that I can implement the onboarding flow.


Acceptance criteria:
- Scenario: Successful registration
    - Given a POST request to `/api/v1/authentication/sign-up` is received with organization and admin user data
    - When the API validates and persists both entities
    - Then the API responds `201 Created` and returns the created user resource

- Scenario: Validation error
    - Given a POST request to `/api/v1/authentication/sign-up` with missing or invalid fields
    - Then the API responds `400 Bad Request` with a validation error payload


---


### TS-IAM-003 — Manage Organizations
As a frontend developer, I want to retrieve and update an organization so that I can implement the organization settings screen.


Acceptance criteria:
- Get all organizations
    - Given a GET to `/api/v1/organizations`
    - Then the API responds `200 OK` with an array of organization resources

- Get by id
    - Given a GET to `/api/v1/organizations/{id}`
    - Then the API responds `200 OK` with the organization resource, or `404 Not Found`

- Create organization
    - Given a POST to `/api/v1/organizations` with body: `{ name, ruc, email, phone, address }`
    - Then the API responds `201 Created` with the created organization resource

- Update organization
    - Given a PATCH to `/api/v1/organizations/{id}` with updatable fields
    - Then the API responds `200 OK` with the updated organization resource


---


### TS-IAM-004 — Manage Users
As a frontend developer, I want to retrieve and update users so that I can implement the user management screen for supervisors and admins.


Acceptance criteria:
- Get all users
    - Given a GET to `/api/v1/users`
    - Then the API responds `200 OK` with an array of user resources

- Get user by id
    - Given a GET to `/api/v1/users/{id}`
    - Then the API responds `200 OK` with the user resource, or `404 Not Found`

- Create user
    - Given a POST to `/api/v1/users` with body: `{ organizationId, firstName, lastName, email, password, role, workAreaId?, specialtyId? }`
    - Then the API responds `201 Created` with the created user resource

- Update user
    - Given a PATCH to `/api/v1/users/{id}` with updatable fields
    - Then the API responds `200 OK` with the updated user resource

- Update user role
    - Given a POST to `/api/v1/users/{id}/roles` with body: `{ role }`
    - Then the API responds `200 OK` and the user's role is updated; an audit log entry is recorded


---


### TS-IAM-005 — Invitation Lifecycle
As a frontend developer, I want to manage invitations so that I can implement the invite-a-doctor flow including sending, accepting, and cancelling invitations.


Acceptance criteria:
- Create invitation
    - Given a POST to `/api/v1/invitations` with body: `{ organizationId, email, role }`
    - Then the API responds `201 Created` with the invitation resource and a unique token

- Send invitation email
    - Given a POST to `/api/v1/invitations/send` with the invitation id
    - Then the API dispatches a transactional email via Resend and responds `200 OK`

- Accept invitation
    - Given a POST to `/api/v1/invitations/accept` with body: `{ token, password, firstName, lastName }`
    - When the token is valid and not expired
    - Then the API responds `200 OK`, creates the user account, and marks the invitation as ACCEPTED

- Update invitation
    - Given a PATCH to `/api/v1/invitations/{id}` with updated fields
    - Then the API responds `200 OK` with the updated invitation resource

- Cancel invitation
    - Given a DELETE to `/api/v1/invitations/{id}`
    - Then the API responds `200 OK` and marks the invitation as CANCELLED

- Get all invitations
    - Given a GET to `/api/v1/invitations`
    - Then the API responds `200 OK` with an array of invitation resources


---


## Bounded Context: Shift Coordination


The Shift Coordination context manages care teams, team members, shift records, work areas, and medical specialties.


### TS-SHIFT-001 — Manage Care Teams
As a frontend developer, I want to create, read, update, and delete care teams so that supervisors can organize medical staff into working units.


Acceptance criteria:
- Get all care teams
    - Given a GET to `/api/v1/careTeams`
    - Then the API responds `200 OK` with an array of care team resources

- Get care team by id
    - Given a GET to `/api/v1/careTeams/{id}`
    - Then the API responds `200 OK` or `404 Not Found`

- Create care team
    - Given a POST to `/api/v1/careTeams` with body: `{ organizationId, name, workAreaId, supervisorId, status }`
    - Then the API responds `201 Created`; an audit log entry of type `TeamCreated` is recorded

- Update care team
    - Given a PATCH to `/api/v1/careTeams/{id}` with updated fields
    - Then the API responds `200 OK`; an audit log entry of type `TeamUpdated` is recorded

- Delete care team
    - Given a DELETE to `/api/v1/careTeams/{id}`
    - Then the API responds `200 OK` and the care team is removed; an audit log entry of type `TeamDeleted` is recorded


---


### TS-SHIFT-002 — Manage Team Members
As a frontend developer, I want to add and remove members from care teams so that supervisors can assign doctors to teams.


Acceptance criteria:
- Get all team members
    - Given a GET to `/api/v1/teamMembers`
    - Then the API responds `200 OK` with an array of team member resources

- Add team member
    - Given a POST to `/api/v1/teamMembers` with body: `{ teamId, userId }`
    - Then the API responds `201 Created` with the team member resource

- Remove team member
    - Given a DELETE to `/api/v1/teamMembers/{id}`
    - Then the API responds `200 OK` and the member is removed from the team


---


### TS-SHIFT-003 — Shift Record Lifecycle (Check-in / Check-out / Evaluate / Block / Reassign)
As a frontend developer, I want to manage shift records so that I can implement the full shift lifecycle: scheduling, check-in, risk evaluation, check-out, blocking due to fatigue, and reassignment.


Acceptance criteria:
- Get shift suggestions
    - Given a GET to `/api/v1/shiftRecords/suggestions`
    - Then the API responds `200 OK` with recommended doctors for available shift slots

- Get all shift records
    - Given a GET to `/api/v1/shiftRecords`
    - Then the API responds `200 OK` with an array of shift record resources

- Create shift record
    - Given a POST to `/api/v1/shiftRecords` with body: `{ organizationId, userId, workAreaId, type, status, scheduledStart, scheduledEnd }`
    - Then the API responds `201 Created`

- Update shift record
    - Given a PATCH to `/api/v1/shiftRecords/{id}` with updated fields
    - Then the API responds `200 OK`

- Check in
    - Given a POST to `/api/v1/shiftRecords/{id}/check-in`
    - When the shift status is SCHEDULED
    - Then the API responds `200 OK`, sets `CheckInAt`, updates status to `IN_PROGRESS`, and records a `ShiftCheckIn` audit log entry

- Check out
    - Given a POST to `/api/v1/shiftRecords/{id}/check-out`
    - When the shift status is IN_PROGRESS
    - Then the API responds `200 OK` and sets `CheckOutAt` and status to `COMPLETED`

- Evaluate risk after check-in
    - Given a POST to `/api/v1/shiftRecords/{id}/evaluate-risk`
    - When vital signs have been recorded for the user
    - Then the API responds `200 OK`, creates or updates a `RiskAssessment`, and may generate anomalies and clinical alerts

- Block shift
    - Given a POST to `/api/v1/shiftRecords/{id}/block`
    - When a recovery plan dictates rest
    - Then the API responds `200 OK` and sets the shift status to `BLOCKED`

- Reassign shift
    - Given a POST to `/api/v1/shiftRecords/{id}/reassign` with body: `{ newUserId }`
    - Then the API responds `200 OK`, updates the `UserId`, and resets the status to `SCHEDULED`


---


### TS-SHIFT-004 — Work Areas and Specialties
As a frontend developer, I want to manage work areas and specialties so that I can populate dropdowns during user creation and team assignment.


Acceptance criteria:
- Work areas — GET `/api/v1/workAreas` → `200 OK` array
- Work area by id — GET `/api/v1/workAreas/{id}` → `200 OK` or `404`
- Create work area — POST `/api/v1/workAreas` with `{ organizationId, name }` → `201 Created`
- Specialties — GET `/api/v1/specialties` → `200 OK` array
- Specialty by id — GET `/api/v1/specialties/{id}` → `200 OK` or `404`
- Create specialty — POST `/api/v1/specialties` with `{ organizationId, name }` → `201 Created`


---


## Bounded Context: Clinical Risk Assessment


The Clinical Risk Assessment context processes vital-sign data, detects anomalies, and generates clinical alerts and risk evaluations.


### TS-CRA-001 — Record a Vital Sign Reading
As a frontend developer (or IoT integration), I want to record a vital sign reading through the API so that the platform can continuously monitor medical staff fatigue.


Acceptance criteria:
- Scenario: Successful create
    - Given a POST to `/api/v1/vitalSignReadings` with body: `{ organizationId, userId, heartRate, hrv, fatigueLevel, cortisolLevel, sensorStatus, recordedAt? }`
    - When the API persists the reading
    - Then the API responds `201 Created` and automatically triggers risk evaluation if applicable

- Scenario: Validation error
    - Given a POST with missing required fields
    - Then the API responds `400 Bad Request`


---


### TS-CRA-002 — Vital Sign Anomaly Detection and Review
As a frontend developer, I want to retrieve and update vital sign anomalies so that supervisors can review and resolve detected anomalies.


Acceptance criteria:
- Get all anomalies
    - Given a GET to `/api/v1/vitalSignAnomalies`
    - Then the API responds `200 OK` with an array of anomaly resources

- Get anomaly by id
    - Given a GET to `/api/v1/vitalSignAnomalies/{id}`
    - Then the API responds `200 OK` or `404 Not Found`

- Create anomaly (internal / supervisor-initiated)
    - Given a POST to `/api/v1/vitalSignAnomalies` with body: `{ organizationId, userId, type, severity, status, value, threshold, message, detectedAt? }`
    - Then the API responds `201 Created`; an audit log entry of type `AnomalyCreated` is recorded (at most once per doctor per minute to prevent spam)

- Update anomaly status
    - Given a PATCH to `/api/v1/vitalSignAnomalies/{id}` with body: `{ status, reviewedBy? }`
    - When status is REVIEWED or RESOLVED
    - Then the API responds `200 OK`, sets `ReviewedAt` and `ReviewedBy`, and records a `AnomalyReviewed` audit log entry


---


### TS-CRA-003 — Clinical Alerts
As a frontend developer, I want to retrieve and update clinical alerts so that supervisors are informed of high-severity situations requiring immediate intervention.


Acceptance criteria:
- Get all alerts
    - Given a GET to `/api/v1/clinicalAlerts`
    - Then the API responds `200 OK` with an array of clinical alert resources

- Get alert by id
    - Given a GET to `/api/v1/clinicalAlerts/{id}`
    - Then the API responds `200 OK` or `404 Not Found`

- Create alert (supervisor-initiated)
    - Given a POST to `/api/v1/clinicalAlerts` with body: `{ organizationId, userId, severity, status, message, createdAt? }`
    - Then the API responds `201 Created`; an audit log entry of type `AlertCreated` is recorded

- Resolve alert
    - Given a PATCH to `/api/v1/clinicalAlerts/{id}` with body: `{ status: "RESOLVED", resolvedBy }`
    - Then the API responds `200 OK`, sets `ResolvedAt` and `ResolvedBy`, and records a `AlertResolved` audit log entry


---


### TS-CRA-004 — Risk Assessments
As a frontend developer, I want to retrieve risk assessments so that I can display a doctor's current fatigue and risk level on the dashboard.


Acceptance criteria:
- Get all risk assessments
    - Given a GET to `/api/v1/riskAssessments`
    - Then the API responds `200 OK` with an array of risk assessment resources

- Get risk assessment by id
    - Given a GET to `/api/v1/riskAssessments/{id}`
    - Then the API responds `200 OK` or `404 Not Found`

- Create risk assessment (system-initiated after vital sign reading)
    - Given a POST to `/api/v1/riskAssessments` with body: `{ organizationId, userId, fatigueLevel, riskLevel, heartRate, hrv, lastUpdatedAt? }`
    - Then the API responds `201 Created`; an audit log entry of type `RiskAssessmentEvaluated` is recorded


---


## Bounded Context: Staff Recovery


The Staff Recovery context manages recovery plans issued when a doctor's fatigue level is critical, and preventive actions that guide rest and rehabilitation.


### TS-RECOVERY-001 — Recovery Plan Lifecycle
As a frontend developer, I want to manage recovery plans so that I can implement the flow where supervisors issue rest recommendations and doctors can accept, reject, or confirm completion.


Acceptance criteria:
- Get all recovery plans
    - Given a GET to `/api/v1/recovery-plans`
    - Then the API responds `200 OK` with an array of recovery plan resources

- Get recovery plan by id
    - Given a GET to `/api/v1/recovery-plans/{id}`
    - Then the API responds `200 OK` or `404 Not Found`

- Issue recovery plan
    - Given a POST to `/api/v1/recovery-plans` with body: `{ medicalStaffId, description, suggestedRestDays }`
    - Then the API responds `201 Created`; the affected shift record is automatically blocked

- Accept recovery plan (doctor accepts rest)
    - Given a PATCH to `/api/v1/recovery-plans/{id}/accept`
    - Then the API responds `200 OK` and the plan status transitions to `ACCEPTED`; an audit log entry of type `PreventiveActionAccepted` is recorded

- Reject recovery plan (doctor rejects rest)
    - Given a PATCH to `/api/v1/recovery-plans/{id}/reject`
    - Then the API responds `200 OK` and the plan status transitions to `REJECTED`

- Confirm completion
    - Given a PATCH to `/api/v1/recovery-plans/{id}/confirm`
    - Then the API responds `200 OK` and the plan status transitions to `COMPLETED`; an audit log entry of type `PreventiveActionCompleted` is recorded


---


### TS-RECOVERY-002 — Preventive Actions
As a frontend developer, I want to manage preventive actions so that supervisors can create, assign, and track specific steps within a recovery plan.


Acceptance criteria:
- Get all preventive actions
    - Given a GET to `/api/v1/preventiveActions`
    - Then the API responds `200 OK` with an array of preventive action resources

- Get preventive action by id
    - Given a GET to `/api/v1/preventiveActions/{id}`
    - Then the API responds `200 OK` or `404 Not Found`

- Create preventive action (supervisor-initiated)
    - Given a POST to `/api/v1/preventiveActions` with body containing action details
    - Then the API responds `201 Created`; an audit log entry of type `PreventiveActionCreated` is recorded

- Update preventive action status
    - Given a PATCH to `/api/v1/preventiveActions/{id}` with updated status
    - Then the API responds `200 OK` with the updated preventive action resource


---


## Bounded Context: Audit & Compliance


The Audit & Compliance context provides immutable traceability records for all significant platform events.


### TS-AUDIT-001 — View Audit Logs
As a frontend developer, I want to retrieve audit logs so that I can implement the admin audit trail screen.


Acceptance criteria:
- Get all audit logs
    - Given a GET to `/api/v1/auditLogs`
    - Then the API responds `200 OK` with an array of audit log resources including type, severity, resourceType, source, actorUserId, and description

- Get audit log by id
    - Given a GET to `/api/v1/auditLogs/{auditLogId}`
    - Then the API responds `200 OK` or `404 Not Found`

- Get audit logs by organization
    - Given a GET to `/api/v1/auditLogs/organizations/{organizationId}`
    - Then the API responds `200 OK` with all audit logs for that organization

- Get audit logs by actor
    - Given a GET to `/api/v1/auditLogs/actors/{actorUserId}`
    - Then the API responds `200 OK` with all audit logs generated by that user

- Create audit log (internal use by other BCs)
    - Given a POST to `/api/v1/auditLogs` with body: `{ organizationId, actorUserId, type, severity, resourceType, resourceId, source, description }`
    - Then the API responds `201 Created`

- Purge audit logs (admin only)
    - Given a DELETE to `/api/v1/auditLogs/purge`
    - Then the API responds `200 OK` and removes all audit log entries (use with caution)


---


## Bounded Context: Subscription & Billing


The Subscription & Billing context manages plan catalogue, organization subscriptions, and Stripe-powered checkout flows.


### TS-SUB-001 — View Plans
As a frontend developer, I want to retrieve subscription plans so that I can implement the pricing screen and allow organizations to choose a plan.


Acceptance criteria:
- Get all plans
    - Given a GET to `/api/v1/plans`
    - Then the API responds `200 OK` with an array of plan resources including price, billing period, limits (maxDoctors, maxTeams, etc.), and feature keys

- Get plan by id
    - Given a GET to `/api/v1/plans/{id}`
    - Then the API responds `200 OK` or `404 Not Found`


---


### TS-SUB-002 — Manage Subscriptions
As a frontend developer, I want to manage subscriptions so that I can show the current plan status and allow upgrades, downgrades, or cancellations.


Acceptance criteria:
- Get all subscriptions
    - Given a GET to `/api/v1/subscriptions`
    - Then the API responds `200 OK` with an array of subscription resources

- Create subscription
    - Given a POST to `/api/v1/subscriptions` with body: `{ organizationId, planId, startedAt? }`
    - Then the API responds `201 Created`

- Update subscription (plan change)
    - Given a PATCH to `/api/v1/subscriptions/{id}` with body: `{ planId }`
    - Then the API responds `200 OK` with the updated subscription

- Cancel subscription
    - Given a DELETE to `/api/v1/subscriptions/{id}`
    - Then the API responds `200 OK` and sets the subscription status to `CANCELLED`

- Get access status
    - Given a GET to `/api/v1/subscriptions/{id}/access-status`
    - Then the API responds `200 OK` with a boolean or access summary indicating whether the organization has an active subscription

- Get subscription summary
    - Given a GET to `/api/v1/subscriptions/{id}/summary`
    - Then the API responds `200 OK` with a summary object containing plan details and current usage limits


---


### TS-SUB-003 — Stripe Billing Flow (Checkout Session)
As a frontend developer, I want to initiate and manage Stripe checkout sessions so that I can redirect users to Stripe to complete payment for their selected plan.


Acceptance criteria:
- Create checkout session
    - Given a POST to `/api/v1/billing/create-checkout-session` with body: `{ organizationId, planId }`
    - Then the API responds `201 Created` with a Stripe session URL to redirect the user

- Check checkout session status
    - Given a GET to `/api/v1/billing/checkout-session-status?sessionId={stripeSessionId}`
    - Then the API responds `200 OK` with the session status (PENDING, COMPLETED, EXPIRED)

- Cancel checkout session
    - Given a POST to `/api/v1/billing/cancel-checkout-session` with body: `{ sessionId }`
    - Then the API responds `200 OK` and marks the session as EXPIRED

- Stripe webhook handler
    - Given a POST to `/api/v1/billing/webhook` from Stripe with a signed event payload
    - When the event type is `checkout.session.completed`
    - Then the API activates the organization's subscription and records a `SubscriptionActivated` audit log entry

- Manage checkout sessions directly
    - GET `/api/v1/checkoutSessions` → `200 OK` with array of checkout session resources
    - POST `/api/v1/checkoutSessions` → `201 Created`


---


## Notes & Next Steps
- All endpoints are documented based on the actual controller routes and business rules in this repository.
- Audit log entries are created automatically by the domain services — frontend developers do NOT need to call `/api/v1/auditLogs` directly except to read the audit trail.
- Anti-spam logic is in place: anomaly creation events (`AnomalyCreated`) are recorded at most once per doctor per minute to prevent flooding the audit log.
- For Stripe integration, ensure the `STRIPE_WEBHOOK_SECRET` environment variable is configured; the webhook endpoint validates the signature before processing.
