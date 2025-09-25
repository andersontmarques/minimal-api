using MinimalApi.Domain.Entities;

namespace MinimalApi.Domain.DTOs.Responses;

public class CreateUserResponse
{
    public User User { get; set; } = new();
}