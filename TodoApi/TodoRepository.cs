using Dapper;
using MySql.Data.MySqlClient;
using TodoApi.Models;

namespace TodoApi;

public class TodoRepository
{
    private readonly string _connectionString;

    public TodoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<Todo> ObterTodos()
    {
        using var connection = new MySqlConnection(_connectionString);
        return connection.Query<Todo>("SELECT * FROM todo");
    }

    public int AdcionarTodo(Todo todo)
    {
        using var connection = new MySqlConnection(_connectionString);
        var sql = "INSERT INTO Todo (Name, IsComplete) VALUES (@Name, @IsComplete);";
        return connection.Execute(sql, todo);
    }
}
