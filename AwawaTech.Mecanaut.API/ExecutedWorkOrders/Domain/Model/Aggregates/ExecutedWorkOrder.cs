using System;
using System.Collections.Generic;
using System.Linq;
using AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Events;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.ExecutedWorkOrders.Domain.Model.Aggregates;

public class ExecutedWorkOrder : AuditableAggregateRoot
{
    public string Code { get; private set; }
    
    public string Annotations { get; private set; }
    public TenantId TenantId { get; private set; }
    public DateTime ExecutionDate { get; private set; }
    public long ProductionLineId { get; private set; }
    public List<long> IntervenedMachineIds { get; private set; }
    public List<long?> AssignedTechnicianIds { get; private set; }
    public List<string> ExecutedTasks { get; private set; }

    protected ExecutedWorkOrder()
    {
        IntervenedMachineIds = new List<long>();
        AssignedTechnicianIds = new List<long?>();
        ExecutedTasks = new List<string>();
    }

    private ExecutedWorkOrder(
        string code,
        string annotations,
        TenantId tenantId,
        DateTime executionDate,
        long productionLineId) : this()
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("ExecutedWorkOrder code cannot be empty");

        Code = code;
        Annotations = annotations;
        TenantId = tenantId;
        ExecutionDate = executionDate;
        ProductionLineId = productionLineId;

        AddDomainEvent(new ExecutedWorkOrderCreatedEvent(Id, tenantId.Value, code));
    }

    public static ExecutedWorkOrder Create(
        string code,
        string annotations,
        TenantId tenantId,
        DateTime executionDate,
        long productionLineId)
    {
        return new ExecutedWorkOrder(code,annotations ,tenantId, executionDate, productionLineId);
    }

    public void SetIntervenedMachines(IEnumerable<long> machineIds)
    {
        if (!machineIds.Any())
            throw new ArgumentException("At least one machine must be assigned");

        IntervenedMachineIds.Clear();
        IntervenedMachineIds.AddRange(machineIds);
    }

    public void SetAssignedTechnicians(IEnumerable<long?> technicianIds)
    {
        AssignedTechnicianIds.Clear();
        AssignedTechnicianIds.AddRange(technicianIds);
    }

    public void SetExecutedTasks(IEnumerable<string> tasks)
    {
        if (!tasks.Any())
            throw new ArgumentException("At least one task must be added");

        ExecutedTasks.Clear();
        ExecutedTasks.AddRange(tasks);
    }
} 