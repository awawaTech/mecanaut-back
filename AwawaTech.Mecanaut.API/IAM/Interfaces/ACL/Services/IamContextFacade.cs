using System.Collections.Generic;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Commands;
using AwawaTech.Mecanaut.API.IAM.Domain.Model.Queries;
using AwawaTech.Mecanaut.API.IAM.Domain.Services;

namespace AwawaTech.Mecanaut.API.IAM.Interfaces.ACL.Services;

public class IamContextFacade(IUserCommandService userCommandService, IUserQueryService userQueryService) : IIamContextFacade
{
    public async Task<int> CreateUser(string username, string password)
    {
        // Crear usuario básico en el contexto actual (sin tenant bootstrap)
        var createCommand = new CreateUserCommand(username, password, $"{username}@example.com", "", "", new List<string>());
        await userCommandService.Handle(createCommand);
        var getUserByUsernameQuery = new GetUserByUsernameQuery(username);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? 0;
    }

    public async Task<int> FetchUserIdByUsername(string username)
    {
        var getUserByUsernameQuery = new GetUserByUsernameQuery(username);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? 0;
    }

    public async Task<string> FetchUsernameByUserId(int userId)
    {
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        var result = await userQueryService.Handle(getUserByIdQuery);
        return result?.Username ?? string.Empty;
    }
}