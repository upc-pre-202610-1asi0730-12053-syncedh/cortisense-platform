$apiBase = "https://cortisense-platform-api.azurewebsites.net/api/v1"

$workAreas = @("UCI", "Urgencias", "Pediatría", "Cardiología", "Cirugía", "Laboratorio", "Radiología", "General")
$specialties = @("Cardiología", "Pediatría", "Medicina General", "Anestesiología", "Enfermería UCI", "Neurología", "Cirugía General", "Traumatología")

Write-Host "Seeding Work Areas..."
foreach ($wa in $workAreas) {
    $body = @{ name = $wa } | ConvertTo-Json
    try {
        Invoke-RestMethod -Method Post -Uri "$apiBase/workAreas" -ContentType "application/json" -Body $body | Out-Null
        Write-Host "Created Work Area: $wa"
    } catch {
        Write-Host "Failed Work Area: $wa - $($_.Exception.Message)"
    }
}

Write-Host "Seeding Specialties..."
foreach ($sp in $specialties) {
    $body = @{ name = $sp } | ConvertTo-Json
    try {
        Invoke-RestMethod -Method Post -Uri "$apiBase/specialties" -ContentType "application/json" -Body $body | Out-Null
        Write-Host "Created Specialty: $sp"
    } catch {
        Write-Host "Failed Specialty: $sp - $($_.Exception.Message)"
    }
}

Write-Host "Done."
