using System.ComponentModel.DataAnnotations;

namespace AwawaTech.Mecanaut.API.AssetManagement.Domain.Model.ValueObjects;

public readonly struct Location
{
    public string Address { get; }
    public string City { get; }
    public string Country { get; }

    public Location(string address, string city, string country)
    {
        if (string.IsNullOrWhiteSpace(address)) throw new ValidationException("Address required");
        if (string.IsNullOrWhiteSpace(city))    throw new ValidationException("City required");
        if (string.IsNullOrWhiteSpace(country)) throw new ValidationException("Country required");
        Address  = address;
        City     = city;
        Country  = country;
    }

    public override string ToString() => $"{Address}, {City}, {Country}";
} 