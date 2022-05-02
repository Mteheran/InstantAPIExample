using Fritz.InstantAPIs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlite<MyContext>("Data Source=ToDo.db");
builder.Services.AddInstantAPIs();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapInstantAPIs<MyContext>(config =>
{
	config.IncludeTable(db => db.ToDoItems, ApiMethodsToGenerate.All, "ToDoItems");
});

app.Run();

public class MyContext : DbContext 
{
    public MyContext(DbContextOptions<MyContext> options) : base(options) {}

    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
}

public class ToDoItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}