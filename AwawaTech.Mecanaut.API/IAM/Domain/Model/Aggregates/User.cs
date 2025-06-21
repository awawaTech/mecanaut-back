using System.Text.Json.Serialization;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.Entities;
using TenantIdVO = AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects.TenantId;

namespace AwawaTech.Mecanaut.API.IAM.Domain.Model.Aggregates;

/**
 * <summary>
 *     The user aggregate
 * </summary>
 * <remarks>
 *     This class is used to represent a user
 * </remarks>
 */
public class User(string username, string passwordHash) : AuditableAggregateRoot
{
    public User() : this(string.Empty, string.Empty)
    {
        TenantId = TenantIdVO.Default.Value;
    }

    public int Id { get; private set; }

    public string Username { get; private set; } = username;

    [JsonIgnore]
    public string PasswordHash { get; private set; } = passwordHash;

    public EmailAddress? Email { get; private set; }

    public string? FirstName { get; private set; }

    public string? LastName { get; private set; }

    public bool Active { get; private set; } = true;

    public long TenantId { get; private set; }

    public ICollection<Role> Roles { get; private set; } = new HashSet<Role>();

    /**
     * <summary>
     *     Update the username
     * </summary>
     * <param name="username">The new username</param>
     * <returns>The updated user</returns>
     */
    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }

    /**
     * <summary>
     *     Update the password hash
     * </summary>
     * <param name="passwordHash">The new password hash</param>
     * <returns>The updated user</returns>
     */
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }

    public User UpdatePersonalInfo(string? firstName, string? lastName, EmailAddress? email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        return this;
    }

    public User SetTenant(long tenantId)
    {
        TenantId = tenantId;
        return this;
    }

    public User Activate()
    {
        Active = true;
        return this;
    }

    public User Deactivate()
    {
        Active = false;
        return this;
    }

    public User AddRole(Role role)
    {
        Roles.Add(role);
        return this;
    }

    public User AddRoles(IEnumerable<Role> roles)
    {
        foreach (var role in roles)
            Roles.Add(role);
        return this;
    }
}