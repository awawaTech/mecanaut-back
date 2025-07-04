namespace AwawaTech.Mecanaut.API.IAM.Interfaces.REST.Resources;

public record CreateUserResource(
    string Username,
    string Password,
    string Email,
    string FirstName,
    string LastName,
    IEnumerable<string> Roles); 