using System.Linq;
using AwawaTech.Mecanaut.API.IAM.Application.Internal.OutboundServices;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.IAM.Domain.Repositories;
using AwawaTech.Mecanaut.API.IAM.Domain.Services;
using AwawaTech.Mecanaut.API.Shared.Domain.Repositories;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Entities;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Domain.Model.ValueObjects;
using AwawaTech.Mecanaut.API.Shared.Infrastructure.Multitenancy;

namespace AwawaTech.Mecanaut.API.IAM.Application.Internal.CommandServices;

/**
 * <summary>
 *     The user command service
 * </summary>
 * <remarks>
 *     This class is used to handle user commands
 * </remarks>
 */
public class UserCommandService(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    ITenantRepository tenantRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork)
    : IUserCommandService
{
    /**
     * <summary>
     *     Handle sign in command
     * </summary>
     * <param name="command">The sign in command</param>
     * <returns>The authenticated user and the JWT token</returns>
     */
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid username or password");

        var token = tokenService.GenerateToken(user);
        

        return (user, token);
    }

    /**
     * <summary>
     *     Handle sign up command
     * </summary>
     * <param name="command">The sign up command</param>
     * <returns>A confirmation message on successful creation.</returns>
     */
    public async Task<User> Handle(SignUpCommand command)
    {
        // 1) Crear Tenant
        var tenant = new Tenant(
            command.Ruc,
            command.LegalName,
            command.CommercialName,
            command.Address,
            command.City,
            command.Country,
            command.TenantPhone,
            command.TenantEmail,
            command.Website);

        await tenantRepository.AddAsync(tenant);
        // Persistimos el tenant para obtener su Id generado antes de crear el usuario admin
        await unitOfWork.CompleteAsync();

        // 2) Establecer contexto multitenant para la creación del usuario administrador
        TenantContext.SetTenantId(tenant.Id);
        try
        {
            // Verificar username único dentro del nuevo tenant
            if (userRepository.ExistsByUsername(command.Username))
                throw new Exception($"Username {command.Username} is already taken");

            // Crear usuario admin
            var hashedPassword = hashingService.HashPassword(command.Password);
            var adminUser = new User(command.Username, hashedPassword)
                .UpdatePersonalInfo(command.FirstName, command.LastName, command.Email)
                .SetTenant(tenant.Id);

            // asignar rol ADMIN
            var adminRole = await roleRepository.FindByNameAsync(nameof(Roles.RoleAdmin)) ?? new Role(Roles.RoleAdmin);
            if (adminRole.Id == 0)
                await roleRepository.AddAsync(adminRole);

            adminUser.AddRole(adminRole);

            await userRepository.AddAsync(adminUser);
            await unitOfWork.CompleteAsync();

            return adminUser;
        }
        finally
        {
            // Limpiar el contexto para no afectar otras operaciones
            TenantContext.Clear();
        }
    }

    public async Task<User> Handle(CreateUserCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} is already taken");

        var hashed = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashed)
            .UpdatePersonalInfo(command.FirstName, command.LastName, new EmailAddress(command.Email))
            .SetTenant(TenantContext.CurrentTenantId);

        // map roles
        var rolesToAssign = new List<Role>();
        if (command.Roles != null)
        {
            foreach (var name in command.Roles)
            {
                var role = await roleRepository.FindByNameAsync(name);
                if (role == null)
                {
                    // create role on the fly if does not exist
                    role = new Role(Enum.Parse<Roles>(name, true));
                    await roleRepository.AddAsync(role);
                }
                rolesToAssign.Add(role);
            }
        }
        user.AddRoles(rolesToAssign);
        await userRepository.AddAsync(user);
        await unitOfWork.CompleteAsync();
        return user;
    }

    public async Task<User> Handle(UpdateUserCommand command)
    {
        var user = await userRepository.FindByIdAsync(command.UserId) ?? throw new Exception("User not found");
        user = user.UpdatePersonalInfo(command.FirstName, command.LastName, new EmailAddress(command.Email));
        if (command.Roles.Any())
        {
            var roles = new List<Role>();
            foreach (var name in command.Roles)
            {
                var role = await roleRepository.FindByNameAsync(name);
                if (role == null)
                {
                    role = new Role(Enum.Parse<Roles>(name, true));
                    await roleRepository.AddAsync(role);
                }
                roles.Add(role);
            }
            user.AddRoles(roles);
        }
        userRepository.Update(user);
        await unitOfWork.CompleteAsync();
        return user;
    }

    public async Task<User> Handle(DeleteUserCommand command)
    {
        var user = await userRepository.FindByIdAsync(command.UserId) ?? throw new Exception("User not found");
        user.Deactivate();
        userRepository.Update(user);
        await unitOfWork.CompleteAsync();
        return user;
    }
}