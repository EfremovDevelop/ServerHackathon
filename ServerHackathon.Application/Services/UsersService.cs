﻿using ServerHackathon.Core.DtoModels;
using ServerHackathon.Core.Interfaces.Auth;
using ServerHackathon.Core.Interfaces.Repositories;
using ServerHackathon.DomainModel;

namespace ServerHackathon.Application.Services;

public class UsersService
{
    private readonly IPasswordHash _passwordHash;
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtProvider _jwtProvider;
    
    public UsersService(IPasswordHash passwordHash, IUsersRepository usersRepository, IJwtProvider jwtProvider)
    {
        _passwordHash = passwordHash;
        _usersRepository = usersRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Guid?> Register(UserDto userDto)
    {
        var existingUser = await _usersRepository.GetByLogin(userDto.Login);
        if (existingUser != null)
        {
            return null;
        }
        var hashPassword = _passwordHash.Generate(userDto.Password);

        var user = new User
        { 
            Id = Guid.NewGuid(),
            Email = userDto.Email,
            Login = userDto.Login,
            Password = hashPassword,
            Name = userDto.Name,
            Surname = userDto.Surname,
            Phone = userDto.Phone,
            GenderId = userDto.Gender.Id,
            UniversityId = userDto.University.Id,
            ProfileImageUrl = userDto.ProfileImageUrl,
        };

        return await _usersRepository.Create(user);
    }

    public async Task<string> Login(string login, string password)
    {
        var user = await _usersRepository.GetByLogin(login);

        var result = _passwordHash.Verify(password, user.Password);

        if (result == false)
            throw new Exception("Failed to login");

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }

    public async Task<UserDto?> GetUserByLogin(string login)
    {
        var user = await _usersRepository.GetByLogin(login);

        if (user is null)
            return null;

        var userDto = new UserDto(user);
        userDto.Events = user.Events.Select(e => new EventDto
        {
            Id = e.Id,
            Name = e.Name,
            Thumbnail = e.Thumbnail,
            Date = e.Date,
            Description = e.Description,
            Place = new PlaceDto(e.Place),
            Status = new EventStatusDto(e.Status),

        }).ToList();
        return userDto;
    }

    public async Task<bool> CheckUserById(Guid id)
    {
        return await _usersRepository.CheckById(id);
    }
}
