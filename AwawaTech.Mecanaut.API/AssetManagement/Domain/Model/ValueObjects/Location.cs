using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

public sealed class Location
{
    public string Address { get; private set; } = null!;
    public string City    { get; private set; } = null!;
    public string Country { get; private set; } = null!;

    public Location(string address, string city, string country)
    {
        if (string.IsNullOrWhiteSpace(address)) throw new ValidationException("Address required");
        if (string.IsNullOrWhiteSpace(city))    throw new ValidationException("City required");
        if (string.IsNullOrWhiteSpace(country)) throw new ValidationException("Country required");
        Address  = address;
        City     = city;
        Country  = country;
    }

    // Parameterless constructor required by EF Core
    protected Location() { }

    public override string ToString() => $"{Address}, {City}, {Country}";
} 