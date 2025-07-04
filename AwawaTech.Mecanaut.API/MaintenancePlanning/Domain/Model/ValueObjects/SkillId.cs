using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.MaintenancePlanning.Domain.Model.ValueObjects;

public readonly struct SkillId
{
    public long Value { get; }
    public SkillId(long value)
    {
        if (value <= 0) throw new ValidationException("SkillId must be positive");
        Value = value;
    }
    public override string ToString() => Value.ToString();
} 