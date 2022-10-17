var builder = WebApplication.CreateBuilder(args);
builder.Configuration.GetConnectionString("DefaultConnection");
var app = builder.Build();

app.Map("/info", Info);
app.Run(async (context) => await context.Response.WriteAsync("It's lab nubmer 3"));
app.Run();


void Info(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync($"<h1>User: {context.User}</h1>");
    });

}