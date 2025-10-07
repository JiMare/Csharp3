var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/test", () => "Hello Test");

app.MapGet("/pozdrav/{jmeno}", (string jmeno) => $"Hello Test {jmeno}");

app.MapGet("/nazdar-svete", () => "Nazdar!!!");

app.Run();
