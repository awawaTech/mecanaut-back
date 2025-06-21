using AwawaTech.Mecanaut.API.Shared.Domain.Events;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.Events;
using AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.ValueObjects;
using System.Linq;
using System.Collections.Generic;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.Aggregates;

public class StaticMaintenancePlan : MaintenancePlan
{
    public ProductionLineId LineId { get; private set; }
    public int CyclePeriodInDays { get; private set; }
    public int DurationInDays { get; private set; }

    private readonly List<StaticPlanItem> _items = new();
    public IReadOnlyCollection<StaticPlanItem> Items => _items.AsReadOnly();

    private StaticMaintenancePlan(string name,
                                  PlanPeriod period,
                                  ProductionLineId lineId,
                                  int cyclePeriodInDays,
                                  int durationInDays,
                                  TenantId tenantId)
        : base(name, period, PlanType.Static, tenantId)
    {
        if (cyclePeriodInDays <= 0 || durationInDays <= 0 || durationInDays > cyclePeriodInDays)
            throw new ArgumentException("Invalid cycle/duration parameters");

        LineId           = lineId;
        CyclePeriodInDays= cyclePeriodInDays;
        DurationInDays   = durationInDays;

        AddDomainEvent(new MaintenancePlanCreatedEvent(Id, tenantId.Value, PlanType.Static));
    }

    public static StaticMaintenancePlan Create(string name,
                                               PlanPeriod period,
                                               ProductionLineId lineId,
                                               int cyclePeriodInDays,
                                               int durationInDays,
                                               TenantId tenantId) =>
        new(name, period, lineId, cyclePeriodInDays, durationInDays, tenantId);

    public StaticPlanItem AddItem(int dayIndex)
    {
        if (dayIndex < 1 || dayIndex > DurationInDays)
            throw new ArgumentOutOfRangeException(nameof(dayIndex));

        var item = new StaticPlanItem(this, dayIndex);
        _items.Add(item);
        return item;
    }

    public void GenerateWorkOrdersFor(DateOnly currentDate)
    {
        if (!IsActiveOn(currentDate)) return;

        var daysSinceStart = currentDate.ToDateTime(TimeOnly.MinValue).Subtract(Period.StartDate.ToDateTime(TimeOnly.MinValue)).Days;
        if (daysSinceStart < 0) return;

        int dayOfCycle = (daysSinceStart % CyclePeriodInDays) + 1; // 1-based
        if (dayOfCycle > DurationInDays) return;

        _items.Where(i => i.DayIndex == dayOfCycle)
              .ToList()
              .ForEach(i => AddDomainEvent(new StaticPlanDayGeneratedEvent(Id,
                    currentDate.AddDays(-(dayOfCycle - 1)),
                    dayOfCycle)));
    }
} 