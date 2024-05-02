using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Auth;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Application.Services;

public class UsersService
{
    private readonly IPasswordHash _passwordHash;
    private readonly IUsersRepository _usersRepository;

    public UsersService(IPasswordHash passwordHash, IUsersRepository usersRepository)
    {
        _passwordHash = passwordHash;
        _usersRepository = usersRepository;
    }

    public async Task Register(UserDto userDto)
    {
        var existingUser = await _usersRepository.GetByEmail(userDto.Email);
        if (existingUser != null)
        {
            return;
        }
        var hashPassword = _passwordHash.Generate(userDto.Password);

        var user = new User 
        { 
            Id = Guid.NewGuid(),
            Email = userDto.Email,
            Password = hashPassword,
            Name = userDto.Name,
            Surname = userDto.Surname,
            Phone = userDto.Phone,
            Points = userDto.Points,
            GenderId = userDto.GenderId,
        };

        await _usersRepository.Create(user);
    }

    public async Task<UserDto?> GetUserByEmail(string email)
    {
        var user = await _usersRepository.GetByEmail(email);

        if (user is null)
            return null;

        return new UserDto(user);
    }
}
