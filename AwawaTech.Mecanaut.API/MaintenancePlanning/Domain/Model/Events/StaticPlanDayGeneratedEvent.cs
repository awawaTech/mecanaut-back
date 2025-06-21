using AwawaTech.Mecanaut.API.Shared.Domain.Events;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.Events;

public record StaticPlanDayGeneratedEvent(long PlanId, DateOnly CycleStart, int DayIndex) : IDomainEvent; 