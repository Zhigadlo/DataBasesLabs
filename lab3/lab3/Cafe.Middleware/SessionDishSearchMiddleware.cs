using lab3.Cafe.Domain;
using lab3.Cafe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace lab3.Cafe.Middleware
{
    public class SessionDishSearchMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionDishSearchMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, CafeContext dbcontext)
        {
            if (context.Request.Path == "/sessionSearchDishes")
            {
                string html = "<html><head>" +
                "<Title>Dish search</Title></head>" +
                "<meta http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                "<body><h1>Dish search</h1>";
                DishSearchModel dishSearchModel = new DishSearchModel("", "");
                if (context.Request.Query["searchname"].Count > 0)
                {
                    dishSearchModel = new DishSearchModel(context.Request.Query["searchname"], 
                                                          context.Request.Query["searchingridient"]);
                }
                else if(context.Session.Keys.Contains("dish"))
                {
                    dishSearchModel = context.Session.Get<DishSearchModel>("dish");
                }
                else
                {
                    context.Session.Set("dish", dishSearchModel);
                }

                context.Session.Set("dish", dishSearchModel);
                html += $"<form><h2>Dish name: </h2><input type='text' name='searchname' value='{dishSearchModel.dishName}'/><br/>";

                html += "<h2>Ingridient: </h2><select name='searchingridient' value=''/>";
                html += "<option value = ''> </option>";
                foreach (var i in dbcontext.Ingridients)
                {
                    if(i.Name == dishSearchModel.ingridient)
                       html += "<option value ='" + i.Name + "' selected>" + i.Name + "</option>";
                    else
                       html += "<option value ='" + i.Name + "'>" + i.Name + "</option>";
                }

                html += "</select><br/>";
                html += "<button name='searchbutton'>Search</button></form>";
                html += "</body></html>";


                var dishes = dbcontext.Dishes.Include(x => x.IngridientsDishes)
                        .Where(x => x.Name.Contains(dishSearchModel.dishName))
                        .Where(x => x.IngridientsDishes.Count(c => c.Ingridient.Name == dishSearchModel.ingridient) > 0)
                        .ToList();

                html += "<table border=1>";
                html += "<th><td>Name</td><td>Cost</td><td>Cooking Time</td></th>";
                foreach (var d in dishes)
                {
                    html += $"<tr><td>{d.Id}</td><td>{d.Name}</td><td>{d.Cost}</td><td>{d.CookingTime}</td></tr>";
                }
                html += "</table>";
                string newName = context.Request.Query["searchname"];
                string newIngridient = context.Request.Query["searchingridient"];
                if (newName != null && newIngridient != null)
                    context.Session.Set("name", new DishSearchModel(newName, newIngridient));

                context.Response.WriteAsync(html);
            }
            await _next(context);
            
        }
    }
}
