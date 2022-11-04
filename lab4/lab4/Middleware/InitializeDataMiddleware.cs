using lab4.Cafe.Models;

namespace lab4.Middleware
{
    public class InitializeDataMiddleware
    {
        private readonly RequestDelegate _next;

        public InitializeDataMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, CafeContext dbcontext)
        {
            IngridientsInitialize(dbcontext);
            DishesInitialize(dbcontext);
            ProfessionInitialize(dbcontext);
            EmployeesInitialize(dbcontext);
            return _next(httpContext);
        }

        private void IngridientsInitialize(CafeContext dbcontext)
        {
            if (!dbcontext.Ingridients.Any())
            {
                string[] ingridientNames = new string[] { "coffee", "milk", "tea", "sugar", "lemon", "water" };
                Ingridient[] ingridients = new Ingridient[ingridientNames.Length];
                for (int i = 0; i < ingridientNames.Length; i++)
                {
                    Ingridient newIngridient = new Ingridient();
                    newIngridient.Name = ingridientNames[i];
                    //newIngridient.Id = i + 1;
                    ingridients[i] = newIngridient;
                }
                dbcontext.Ingridients.AddRange(ingridients);
                dbcontext.SaveChanges();
            }
        }

        private void DishesInitialize(CafeContext dbcontext)
        {
            if (!dbcontext.Dishes.Any())
            {
                string[] dishNames = new string[] { "milk coffee", "tea with sugar", "tea with lemon" };
                float[] dishCosts = new float[] { 3.1f, 2.3f, 2.7f };
                int[] cookingTimes = new int[] { 5, 3, 3 };
                Dish[] dishes = new Dish[dishNames.Length];
                for (int i = 0; i < dishNames.Length; i++)
                {
                    Dish newDish = new Dish();
                    //newDish.Id = i + 1;
                    newDish.Name = dishNames[i];
                    newDish.Cost = dishCosts[i];
                    newDish.CookingTime = cookingTimes[i];
                    dishes[i] = newDish;
                }

                dbcontext.Dishes.AddRange(dishes);
                dbcontext.SaveChanges();
            }

        }

        private void ProfessionInitialize(CafeContext dbcontext)
        {
            if (!dbcontext.Professions.Any())
            {
                string[] names = new string[] { "cleaner", "manager", "cook", "cassier", "waiter" };
                Profession[] professions = new Profession[names.Length];
                for (int i = 0; i < professions.Length; i++)
                {
                    var newProfession = new Profession();
                    newProfession.Name = names[i];
                    professions[i] = newProfession;
                }
                dbcontext.AddRange(professions);
                dbcontext.SaveChanges();
            }
        }

        private void EmployeesInitialize(CafeContext dbcontext)
        {
            if (!dbcontext.Employees.Any())
            {
                Profession[] professions = dbcontext.Professions.ToArray();
                Random rnd = new Random();
                Employee[] employees = new Employee[20];
                for (int i = 0; i < employees.Length; i++)
                {
                    Employee newEmployee = new Employee();
                    newEmployee.Profession = professions[rnd.Next(0, professions.Length)];
                    newEmployee.FirstName = "firstName" + rnd.Next(1, 20);
                    newEmployee.LastName = "lastName" + rnd.Next(1, 20);
                    newEmployee.MiddleName = "middleName" + rnd.Next(1, 20);
                    newEmployee.Age = rnd.Next(18, 64);
                    newEmployee.Education = "education" + rnd.Next(0, 5);
                    employees[i] = newEmployee;
                }
                dbcontext.AddRange(employees);
                dbcontext.SaveChanges();
            }
        }
    }
}
