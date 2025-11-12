using Microsoft.AspNetCore.Builder;
using TodoApi;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

// Conectando ao banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new TodoRepository(connectionString));

builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "TodoAPI";
    config.Title = "TodoAPI v1";
    config.Version = "v1";
});
var app = builder.Build();

//Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "TodoAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

// Endpoints
app.MapGet("/obter-todo", (TodoRepository repo) => repo.ObterTodos());
app.MapPost("/adcionar-todo", (TodoRepository repo, Todo todo) =>
{
    repo.AdcionarTodo(todo);
    return Results.Ok("Deu bom!");
});

app.Run();
