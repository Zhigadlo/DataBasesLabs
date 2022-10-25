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
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
var app = builder.Build();
app.UseSession();
app.Map("/", (context) => 
{ 
    string html = ""; 
    html += "<h1><a href='/info'>User info</a></h1>";
    html += "<h1><a href='/dishes'>Cached dishes list</a></h1>";
    html += "<h1><a href='/cookiesSearchDishes'>Search form with cookies using</a></h1>";
    html += "<h1><a href='/sessionSearchDishes'>Search form with session using</a></h1>";
    return context.Response.WriteAsync(html);
});
app.Map("/info", (context) => 
{
    string html = "";
    html += $"<h1>Browser: {context.Request.Headers["sec-ch-ua"]}</h1>";
    html += $"<h1>Platform: {context.Request.Headers["sec-ch-ua-platform"]}</h1>";
    return context.Response.WriteAsync(html); 
});

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
app.UseMiddleware<SessionDishSearchMiddleware>();
app.Run();