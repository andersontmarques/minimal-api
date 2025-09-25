using MinimalApi.Domain.Entities;
using MongoDB.Driver;

namespace MinimalApi.Data.Repositories;

public class UserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(DatabaseSettings settings)
    {
        var client = new MongoClient(settings.MongoDB);
        var database = client.GetDatabase("Auth");
        _users = database.GetCollection<User>("users");
    }

    public IEnumerable<User> GetAll()
    {
        return _users.Find(_ => true).ToList();
    }

    public void Add(User user)
    {
        _users.InsertOne(user);
    }

    public User? GetByEmail(string email)
    {
        return _users.Find(u => u.Email == email).FirstOrDefault();
    }
}