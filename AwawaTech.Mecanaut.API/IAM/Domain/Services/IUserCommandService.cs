using AwawaTech.Mecanaut.API.IAM.Domain.Model.Aggregates;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Commands;

namespace AwawaTech.Mecanaut.API.IAM.Domain.Services;

/**
 * <summary>
 *     The user command service
 * </summary>
 * <remarks>
 *     This interface is used to handle user commands
 * </remarks>
 */
public interface IUserCommandService
{
    /**
        * <summary>
        *     Handle sign in command
        * </summary>
        * <param name="command">The sign in command</param>
        * <returns>The authenticated user and the JWT token</returns>
        */
    Task<(User user, string token)> Handle(SignInCommand command);

    /**
        * <summary>
        *     Handle sign up command
        * </summary>
        * <param name="command">The sign up command</param>
        * <returns>A confirmation message on successful creation.</returns>
        */
    Task<User> Handle(SignUpCommand command);

    /// <summary>
    ///     Handle create user command (admin creates another user)
    /// </summary>
    Task<User> Handle(CreateUserCommand command);

    /// <summary>
    ///     Handle update user command
    /// </summary>
    Task<User> Handle(UpdateUserCommand command);

    /// <summary>
    ///     Handle delete user command (soft-delete/deactivate)
    /// </summary>
    Task<User> Handle(DeleteUserCommand command);
}