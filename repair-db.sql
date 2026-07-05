-- ============================================================
-- CORTISENSE PLATFORM - FULL DATABASE REPAIR SCRIPT
-- Fixes: passwords, care teams, team members, clinical alerts,
--        specialties/work areas for seeded users, duplicate org
-- ============================================================

-- 1. FIX PASSWORDS (bcrypt hashes for seeded users)
-- Admin123!, Supervisor123!, Doctor123!
UPDATE users SET password_hash = '$2a$11$7jYJbJNsTwCkQtZBj7.PUeKIYjD2MwgIU1THdt9FfGTeoE1llXHuO' WHERE id IN (101) AND LEFT(password_hash,7) != '$2a$11';
UPDATE users SET password_hash = '$2a$11$HtR/aC.XUPxkrCqFfMIiZuIrsLAlTqDVQGz49fAyY.UJXn7chff7O' WHERE id IN (102, 207) AND LEFT(password_hash,7) != '$2a$11';
UPDATE users SET password_hash = '$2a$11$C23CsBb5kbSTgWN6cDYktuql0KB4b1tyBJlRNiXTwWytvR..d6lxq' WHERE id IN (103, 104, 205, 206) AND LEFT(password_hash,7) != '$2a$11';

-- 2. ASSIGN WORK AREAS AND SPECIALTIES TO SEEDED USERS
UPDATE users SET work_area_id = 1, specialty_id = 3  WHERE id = 103;  -- Laura Gomez: UCI, Medicina Interna
UPDATE users SET work_area_id = 2, specialty_id = 4  WHERE id = 104;  -- Juan Perez: Emergencias, Urgencias
UPDATE users SET work_area_id = 3, specialty_id = 1  WHERE id = 205;  -- Ana Torres: Hospitalizacion, Cardiologia
UPDATE users SET work_area_id = 6, specialty_id = 2  WHERE id = 206;  -- Luis Ramirez: Pediatria, Pediatria
UPDATE users SET work_area_id = 1, specialty_id = 5  WHERE id = 207;  -- Maria Lopez: UCI, Neurologia

-- 3. CREATE CARE TEAMS FOR ORGANIZATION 101
INSERT IGNORE INTO care_teams (id, name, supervisor_id, work_area_id, status, organization_id, created_at, updated_at)
VALUES
  (201, 'Equipo UCI Crítico',    102, 1, 'ACTIVE', 101, NOW(), NOW()),
  (202, 'Equipo Emergencias',    207, 2, 'ACTIVE', 101, NOW(), NOW());

-- 4. CREATE TEAM MEMBERS FOR ORG 101
INSERT IGNORE INTO team_members (id, team_id, user_id, created_at, updated_at)
VALUES
  (201, 201, 103, NOW(), NOW()),  -- Laura en Equipo UCI (supervisado por Carlos)
  (202, 201, 104, NOW(), NOW()),  -- Juan en Equipo UCI (supervisado por Carlos)
  (203, 202, 205, NOW(), NOW()),  -- Ana en Equipo Emergencias (supervisado por Maria)
  (204, 202, 206, NOW(), NOW());  -- Luis en Equipo Emergencias (supervisado por Maria)

-- 5. CREATE CLINICAL ALERTS FOR ORG 101
INSERT IGNORE INTO clinical_alerts (id, user_id, organization_id, status, severity, message, created_at, updated_at)
VALUES
  (201, 103, 101, 'ACTIVE',   'HIGH',     'Fatiga extrema detectada - Laura Gomez supera umbral crítico',    NOW(), NOW()),
  (202, 104, 101, 'ACTIVE',   'MODERATE', 'Frecuencia cardíaca elevada detectada en Juan Perez',             NOW(), NOW()),
  (203, 205, 101, 'RESOLVED', 'MODERATE', 'Fatiga media resuelta - Ana Torres en recuperación',              NOW(), NOW()),
  (204, 206, 101, 'ACTIVE',   'LOW',      'Primer turno nocturno - monitoreo preventivo para Luis Ramirez',  NOW(), NOW());

-- 6. REMOVE DUPLICATE ORGANIZATION (id=1 is duplicate of id=101)
--    Only delete if nothing else references org 1 that we care about
-- (Skipping deletion - risky; just leave it, frontend filters by organizationId from auth token)

-- 7. NORMALIZE ROLE STRINGS for users 107/108 (old test users in org 102)
UPDATE users SET registration_status = 'COMPLETED' WHERE id IN (107, 108) AND (registration_status IS NULL OR registration_status = '');

SELECT 'Script completed successfully' AS status;
