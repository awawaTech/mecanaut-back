using AwawaTech.Mecanaut.API.IAM.Domain.Model.ValueObjects;

namespace AwawaTech.Mecanaut.API.IAM.Domain.Model.Entities;

/// <summary>
/// Entidad de dominio que representa un rol de usuario.
/// </summary>
public class Role
{
    public int Id { get; private set; }

    public Roles Name { get; private set; }

    private Role() { }

    public Role(Roles name)
    {
        Name = name;
    }

    public string GetStringName() => Name.ToString();

    public static Role GetDefaultRole() => new(Roles.RoleTechnical);

    public static Role ToRoleFromName(string name) => new(Enum.Parse<Roles>(name, ignoreCase: true));
} 