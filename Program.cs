using MinimalApi;
using MinimalApi.Domain.Queries;
using MinimalApi.Domain.Commands;
using MinimalApi.Domain.Entities;
using MinimalApi.Data.Repositories;
using MinimalApi.Domain.DTOs.Requests;

var builder = WebApplication.CreateBuilder(args);

var dbSettings = new DatabaseSettings();
builder.Configuration.GetSection("ConnectionStrings").Bind(dbSettings);

builder.Services.AddSingleton(dbSettings);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MinimalApi", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/users", (DatabaseSettings settings) =>
{
    var repository = new UserRepository(settings);
    var query = new GetAllUsersQuery(repository);
    return query.Execute();
});

app.MapPost("/users", (CreateUserRequest request, DatabaseSettings settings) =>
{
    var repository = new UserRepository(settings);
    var command = new CreateUserCommand(repository);
    return command.Execute(request);
});

app.MapPost("/auth/login", (LoginRequest request, DatabaseSettings settings) =>
{
    var repository = new UserRepository(settings);
    var command = new LoginCommand(repository, settings);
    var result = command.Execute(request);
    
    if (!result.Success)
    {
        return Results.Unauthorized();
    }
    
    return Results.Ok(result);
});

app.Run();