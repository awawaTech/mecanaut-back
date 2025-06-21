using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

public sealed class ContactInfo
{
    private static readonly Regex EmailRegex = new("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$", RegexOptions.Compiled);

    public string Phone { get; private set; } = null!;
    public string Email { get; private set; } = null!;

    public ContactInfo(string phone, string email)
    {
        if (string.IsNullOrWhiteSpace(phone)) throw new ValidationException("Phone required");
        if (!EmailRegex.IsMatch(email))        throw new ValidationException("Invalid email");
        Phone = phone;
        Email = email;
    }

    protected ContactInfo() { }

    public override string ToString() => $"{Phone} / {Email}";
} 