using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

public readonly struct ContactInfo
{
    private static readonly Regex EmailRegex = new("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$", RegexOptions.Compiled);

    public string Phone { get; }
    public string Email { get; }

    public ContactInfo(string phone, string email)
    {
        if (string.IsNullOrWhiteSpace(phone)) throw new ValidationException("Phone required");
        if (!EmailRegex.IsMatch(email))        throw new ValidationException("Invalid email");
        Phone = phone;
        Email = email;
    }

    public override string ToString() => $"{Phone} / {Email}";
} 