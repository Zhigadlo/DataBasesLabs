using Microsoft.EntityFrameworkCore;
using lab3;
using lab3.Cafe.Domain;
using lab3.Cafe.Services;
using lab3.Cafe.Middleware;

var builder = WebApplication.CreateBuilder(args);
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CafeContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddTransient<CachedDishesService>();
builder.Services.AddMemoryCache();

var app = builder.Build();
app.UseRouting();
app.Map("/", () => "Index\n");
app.Map("/info", (context) => context.Response.WriteAsync($"<h1>User: {context.User}</h1>"));

app.Map("/dishes", (context) =>
{
    CachedDishesService cachedDishesService = context.RequestServices.GetService<CachedDishesService>();
    string html = "<html><head>" +
                "<Title>Dishes</Title></head>" +
                "<meta http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                "<body><h1>Dish list</h1>";

    html += "<table border=1>";

    html += "<th><td>Name</td><td>Cost</td><td>Cooking Time</td></th>";

    IEnumerable<Dish> dishes = cachedDishesService.GetDishes("Dishes20");

    foreach (Dish d in dishes)
    {
        html += $"<tr><td>{d.Id}</td><td>{d.Name}</td><td>{d.Cost}</td><td>{d.CookingTime}</td></tr>";
    }

    html += "</table></body></html>";
    return context.Response.WriteAsync(html);
});

app.UseMiddleware<CookieDishSearchMiddleware>();
app.Run();