using MinimalApi.Domain.Entities;

namespace MinimalApi.Domain.DTOs.Responses;

public class GetAllUsersResponse
{
    public IEnumerable<User> Users { get; set; } = Enumerable.Empty<User>();
}