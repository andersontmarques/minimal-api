using MinimalApi.Data.Repositories;
using MinimalApi.Domain.DTOs.Requests;
using MinimalApi.Domain.DTOs.Responses;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using StackExchange.Redis;
using System.Text.Json;

namespace MinimalApi.Domain.Commands;

public class LoginCommand
{
    private readonly UserRepository _repository;
    private readonly IDatabase _redis;

    public LoginCommand(UserRepository repository, DatabaseSettings settings)
    {
        _repository = repository;
        var connection = ConnectionMultiplexer.Connect(settings.Redis);
        _redis = connection.GetDatabase();
    }

    public LoginResponse Execute(LoginRequest request)
    {
        var user = _repository.GetByEmail(request.Email);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            return new LoginResponse { Success = false, Message = "Invalid credentials" };
        }

        var token = GenerateJwtToken(user.Name, user.Email);
        
        var userData = JsonSerializer.Serialize(new { user.Id, user.Name, user.Email });
        _redis.StringSet(token, userData, TimeSpan.FromHours(1));
        
        return new LoginResponse { Success = true, Message = "Login successful", Token = token };
    }

    private string GenerateJwtToken(string name, string email)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my-secret-key-with-at-least-32-characters"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim("name", name),
            new Claim("email", email)
        };
        
        var token = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddHours(1),
            claims: claims,
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}