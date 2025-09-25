using MinimalApi.Domain.Entities;
using MinimalApi.Data.Repositories;
using MinimalApi.Domain.DTOs.Responses;

namespace MinimalApi.Domain.Queries;

public class GetAllUsersQuery
{
    private readonly UserRepository _repository;

    public GetAllUsersQuery(UserRepository repository)
    {
        _repository = repository;
    }

    public GetAllUsersResponse Execute()
    {
        var users = _repository.GetAll();
        return new GetAllUsersResponse { Users = users };
    }
}