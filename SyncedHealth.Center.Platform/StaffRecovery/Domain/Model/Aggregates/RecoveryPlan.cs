using System;
using SyncedHealth.Center.Platform.Shared.Domain.Model.Entities;
using SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Commands;

namespace SyncedHealth.Center.Platform.StaffRecovery.Domain.Model.Aggregates;

public class RecoveryPlan : IAuditableEntity
{
    public int Id { get; private set; }
    public int MedicalStaffId { get; private set; }
    public string Description { get; private set; }
    public int SuggestedRestDays { get; private set; }
    public string Status { get; private set; }
    
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public RecoveryPlan()
    {
        Description = string.Empty;
        Status = "Pending";
    }

    public RecoveryPlan(IssueRecoveryRecommendationCommand command)
    {
        MedicalStaffId = command.MedicalStaffId;
        Description = command.Description;
        SuggestedRestDays = command.SuggestedRestDays;
        Status = "Pending"; 
    }
    
    public void AcceptPlan()
    {
        Status = "Accepted";
    }

    public void RejectPlan()
    {
        Status = "Rejected";
    }
}