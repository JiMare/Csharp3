var builder = WebApplication.CreateBuilder(args);
{
    //Configure DI
    builder.Services.AddControllers();
}

var app = builder.Build();

{
    //ConfigureAwaitOptions Middleware
    app.MapControllers();
}

app.Run();
