using ToDoList.Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    //Configure DI
    builder.Services.AddControllers();
    builder.Services.AddDbContext<ToDoItemsContext>();
}

var app = builder.Build();

{
    //ConfigureAwaitOptions Middleware
    app.MapControllers();
}

app.Run();
