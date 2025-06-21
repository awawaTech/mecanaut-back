using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.ValueObjects;

public readonly struct PlanPeriod
{
    public DateOnly StartDate { get; }
    public DateOnly EndDate { get; }

    public PlanPeriod(DateOnly startDate, DateOnly endDate)
    {
        if (endDate < startDate)
            throw new ValidationException("End date must be on or after start date");

        StartDate = startDate;
        EndDate   = endDate;
    }

    public bool Contains(DateOnly date) => date >= StartDate && date <= EndDate;

    public override string ToString() => $"{StartDate:yyyy-MM-dd}..{EndDate:yyyy-MM-dd}";
} 