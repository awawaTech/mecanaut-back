using System;
using System.Collections.Generic;

namespace AwawaTech.Mecanaut.API.ExecutedWorkOrders.Interfaces.REST.Resources;

public class ExecutedWorkOrderResource
{
    public long Id { get; set; }
    public string Code { get; set; }
    public DateTime ExecutionDate { get; set; }
    public long ProductionLineId { get; set; }
    public List<long> IntervenedMachineIds { get; set; }
    public List<long?> AssignedTechnicianIds { get; set; }
    public List<string> ExecutedTasks { get; set; }
    public List<ProductQuantityResource> UsedProducts { get; set; }
} 