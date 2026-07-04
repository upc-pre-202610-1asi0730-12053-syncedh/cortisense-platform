INSERT INTO users (first_name, last_name, email, password_hash, role, status, organization_id, specialty_id, work_area_id) VALUES
('Ana', 'Gomez', 'ana@example.com', 'hash', 'DOCTOR', 'ACTIVE', 102, 1, 1),
('Carlos', 'Ruiz', 'carlos@example.com', 'hash', 'DOCTOR', 'ACTIVE', 102, 1, 2);

SELECT LAST_INSERT_ID() INTO @ana_id;
SET @carlos_id = @ana_id + 1;

INSERT INTO care_teams (organization_id, name, work_area_id, supervisor_id, status, created_at, updated_at) VALUES
(102, 'Equipo Alfa', 1, 106, 'ACTIVE', NOW(), NOW());

SELECT LAST_INSERT_ID() INTO @team_id;

INSERT INTO team_members (team_id, user_id, created_at, updated_at) VALUES
(@team_id, @ana_id, NOW(), NOW()),
(@team_id, @carlos_id, NOW(), NOW());

INSERT INTO risk_assessments (organization_id, user_id, fatigue_level, risk_level, heart_rate, hrv, last_updated_at) VALUES
(102, @ana_id, 85, 'HIGH', 110, 30, NOW()),
(102, @carlos_id, 40, 'MODERATE', 85, 55, NOW());

INSERT INTO clinical_alerts (organization_id, user_id, severity, status, message, created_at, resolved_at, resolved_by) VALUES
(102, @ana_id, 'HIGH', 'ACTIVE', 'Fatiga extrema detectada en Ana', NOW(), NULL, NULL),
(102, @carlos_id, 'MODERATE', 'RESOLVED', 'Frecuencia cardiaca elevada', DATE_SUB(NOW(), INTERVAL 1 DAY), NOW(), 106);

INSERT INTO vital_sign_anomalies (organization_id, user_id, type, severity, status, value, threshold, message, detected_at, reviewed_at, reviewed_by) VALUES
(102, @ana_id, 'HEART_RATE_SPIKE', 'HIGH', 'OPEN', '115 bpm', '100 bpm', 'Pico de HR detectado', NOW(), NULL, NULL);
