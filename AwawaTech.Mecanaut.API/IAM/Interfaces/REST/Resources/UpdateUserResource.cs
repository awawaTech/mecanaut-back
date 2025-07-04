namespace AwawaTech.Mecanaut.API.IAM.Interfaces.REST.Resources;

public record UpdateUserResource(
    string Email,
    string FirstName,
    string LastName,
    IEnumerable<string> Roles); 