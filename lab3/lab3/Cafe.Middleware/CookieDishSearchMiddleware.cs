using lab3.Cafe.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace lab3.Cafe.Middleware
{
    public class CookieDishSearchMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieDishSearchMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, CafeContext dbcontext)
        {
            if (context.Request.Path == "/cookiesSearchDishes")
            {
                string html = "<html><head>" +
                "<Title>Dish search</Title></head>" +
                "<meta http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                "<body><h1>Dish search</h1>";

                string name = "", ingridient = "";
                if (context.Request.Query["searchname"].Count > 0)
                    name = context.Request.Query["searchname"];
                else if (context.Request.Cookies.ContainsKey("name"))
                    name = context.Request.Cookies["name"];
                else
                    context.Response.Cookies.Append("name", name);

                if (context.Request.Query["searchingridient"].Count > 0)
                    ingridient = context.Request.Query["searchingridient"];
                else if(context.Request.Cookies.ContainsKey("ingridient"))
                    ingridient = context.Request.Cookies["ingridient"];
                else
                    context.Response.Cookies.Append("ingridient", ingridient);
                
                html += $"<form><h2>Dish name: </h2><input type='text' name='searchname' value='{name}'/><br/>";


                html += "<h2>Ingridient: </h2><select name='searchingridient' value=''/>";
                html += "<option value = ''> </option>";
                foreach (var i in dbcontext.Ingridients)
                {
                    if(i.Name == ingridient)
                        html += "<option value ='" + i.Name + "' selected>" + i.Name + "</option>";
                    else
                        html += "<option value ='" + i.Name + "'>" + i.Name + "</option>";
                    
                }

                html += "</select><br/>";
                html += "<button name='searchbutton'>Search</button></form>";
                html += "</body></html>";


                var dishes = dbcontext.Dishes.Include(x => x.IngridientsDishes)
                        .Where(x => x.Name.Contains(name))
                        .Where(x => x.IngridientsDishes.Count(c => c.Ingridient.Name == ingridient) > 0)
                        .ToList();

                html += "<table border=1>";
                html += "<th><td>Name</td><td>Cost</td><td>Cooking Time</td></th>";
                foreach (var d in dishes)
                {
                    html += $"<tr><td>{d.Id}</td><td>{d.Name}</td><td>{d.Cost}</td><td>{d.CookingTime}</td></tr>";
                }
                html += "</table>";
                string newCookieName = context.Request.Query["searchname"];
                if(newCookieName != null)
                    context.Response.Cookies.Append("name", newCookieName);

                string newCookieIngridient = context.Request.Query["searchingridient"];
                if(newCookieIngridient != null)
                    context.Response.Cookies.Append("ingridient", newCookieIngridient);

                context.Response.WriteAsync(html);
            }
            await _next(context);
            
        }
    }
}
