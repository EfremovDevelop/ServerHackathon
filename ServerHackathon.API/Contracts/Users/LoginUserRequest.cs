using System.ComponentModel.DataAnnotations;

namespace ServerHackathon.API.Contracts.Users
{
    public record LoginUserRequest(
    [Required] string Login,
    [Required] string Password);
}
