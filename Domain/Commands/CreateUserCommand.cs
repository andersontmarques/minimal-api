using MinimalApi.Domain.Entities;
using MinimalApi.Data.Repositories;
using MinimalApi.Domain.DTOs.Requests;
using MinimalApi.Domain.DTOs.Responses;
using BCrypt.Net;

namespace MinimalApi.Domain.Commands;

public class CreateUserCommand
{
    private readonly UserRepository _repository;

    public CreateUserCommand(UserRepository repository)
    {
        _repository = repository;
    }

    public CreateUserResponse Execute(CreateUserRequest request)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        _repository.Add(user);
        return new CreateUserResponse { User = user };
    }
}