using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.ConditionMonitoring.Domain.Model.Aggregates;
    public class MachineMetricRelation : AuditableEntity
    {
        public long MachineId { get; private set; }
        public long MetricDefinitionId { get; private set; }
        public long TenantId { get; private set; }

        protected MachineMetricRelation() { }

        public MachineMetricRelation(long machineId, long metricDefinitionId, long tenantId)
        {
            MachineId = machineId;
            MetricDefinitionId = metricDefinitionId;
            TenantId = tenantId;
        }
    }

