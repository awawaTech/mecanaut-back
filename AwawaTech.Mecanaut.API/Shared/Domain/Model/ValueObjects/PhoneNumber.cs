using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;

public readonly record struct PhoneNumber
{
    private static readonly Regex Regex =
        new(@"^\+?[0-9\s\-]{7,20}$", RegexOptions.Compiled);

    public string Value { get; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value))
            throw new ValidationException("Invalid phone number.");
        Value = value;
    }

    public override string ToString() => Value;
} 