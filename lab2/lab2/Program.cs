using Microsoft.EntityFrameworkCore;
using lab2.Entities;
using ConsoleTables;
using System.Globalization;

using (CafeContext context = new CafeContext())
{
    bool isEnd = false;
    while(!isEnd)
    {
        Menu();
        Console.Write("Enter menu number(int): ");
        string str = Console.ReadLine() ?? string.Empty;
        try
        {
            int numOfMenu = int.Parse(str);
            switch (numOfMenu)
            {
                //1.Выборка всех данных из таблицы, стоящей в схеме базы данных нас стороне отношения «один» – 1 шт.
                case 1:
                    ListOutput(context.Ingridients);
                    break;
                //2.Выборку данных из таблицы, стоящей в схеме базы данных нас стороне отношения «один», отфильтрованные по определенному условию, налагающему ограничения на одно или несколько полей – 1 шт.
                case 2:
                    IEnumerable<Profession> professions = context.Professions.Where(x => x.Employees.Count() > 13);
                    ListOutput(professions);
                    break;
                //3. Выборка данных, сгруппированных по любому из полей данных с выводом какого-либо итогового результата (min, max, avg, сount или др.) по выбранному полю из таблицы, стоящей в схеме базы данных нас стороне отношения «многие» – 1 шт.
                case 3:
                    int count = context.Orders.Count(x => x.PaymentMethod == 0);
                    Console.WriteLine("Count of people that use cash: " + count);
                    break;
                //4. Выборку данных из двух полей двух таблиц, связанных между собой отношением «один-ко-многим» – 1 шт.
                case 4:
                    var employees = context.Employees.Join(context.Professions,
                                                            x => x.ProfessionId,
                                                            y => y.Id,
                                                            (x, y) => new
                                                            {
                                                                Name = x.FirstName,
                                                                LastName = x.LastName,
                                                                MiddleName = x.MiddleName,
                                                                Profession = y.Name
                                                            });
                    ListOutput(employees);
                    break;
                //5. Выборку данных из двух таблиц, связанных между собой отношением «один-ко-многим» и отфильтрованным по некоторому условию, налагающему ограничения на значения одного или нескольких полей – 1 шт.
                case 5:
                    var ingridientsWarehouse = context.IngridientsWarehouses.Join(context.Providers,
                                                                                  x => x.ProviderId,
                                                                                  y => y.Id,
                                                                                  (x, y) => new
                                                                                  {
                                                                                      IngridientId = x.IngridientId,
                                                                                      Cost = x.Cost,
                                                                                      Weight = x.Weight,
                                                                                      ReleaseDate = x.ReleaseDate,
                                                                                      StorageLife = x.StorageLife,
                                                                                      Provider = y.Name
                                                                                  }).Join(context.Ingridients,
                                                                                          x => x.IngridientId,
                                                                                          y => y.Id,
                                                                                          (x, y) => new
                                                                                          {
                                                                                              IngridientName = y.Name,
                                                                                              Cost = x.Cost,
                                                                                              Weight = x.Weight,
                                                                                              ReleaseDate = x.ReleaseDate,
                                                                                              StorageLife = x.StorageLife,
                                                                                              Provider = x.Provider
                                                                                          }).Where(x => x.Cost > 400);
                    ListOutput(ingridientsWarehouse);
                    break;
                case 6:
                    AddIngridient();
                    break;
                case 7:
                    AddDish();
                    break;
                case 8:
                    RemoveIngridient();
                    break;
                case 9:
                    RemoveDish();
                    break;
                case 10:
                    UpdateEmployeeProfession();
                    break;
                case 11:
                    isEnd = true;
                    break;
                default:
                    Console.WriteLine("There is no such number in the menu");
                    break;
            }
        }
        catch
        {
            Console.WriteLine("You had written not a number");
        }
        Console.Write("Press any key to continue");
        Console.ReadKey();
        Console.Clear();
    }

    void Menu()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("1. Output ingridient list");
        Console.WriteLine("2. Output professions that more than 13 employees have");
        Console.WriteLine("3. Output count of people that use cash");
        Console.WriteLine("4. Output all employees");
        Console.WriteLine("5. Output all ingridients info in wahrehouses that costs more than 400");
        Console.WriteLine("6. Add ingridient");
        Console.WriteLine("7. Add dish");
        Console.WriteLine("8. Remove ingridient");
        Console.WriteLine("9. Remove dish");
        Console.WriteLine("10. Update employee profession");
        Console.WriteLine("11. Exit");
    }

    void UpdateEmployeeProfession()
    {
        int id = InputIntValue("Enter employee id: ");
        Employee? employee = context.Employees.FirstOrDefault(x => x.Id == id);
        if (employee == null)
            Console.WriteLine("There is no such employee");
        else
        {
            Console.WriteLine(employee.ProfessionId + " " + employee.Profession);
            id = InputIntValue("Enter new profession id: ");
            Profession? profession = context.Professions.FirstOrDefault(x => x.Id == id);
            if (profession == null)
                Console.WriteLine("There is no such profession");
            else
            {
                employee.Profession = profession;
                employee.ProfessionId = id;
                context.SaveChanges();
                Console.WriteLine("Employee information is updated");
            }
        }
    }

    void ListOutput<T>(IEnumerable<T> list) where T: class
    {
        ConsoleTable table = new ConsoleTable(typeof(T).GetProperties().Where(x => !x.PropertyType.IsGenericType).Select(x => x.Name).ToArray());

        foreach(var item in list)
        {
            table.AddRow(item.GetType().GetProperties().Where(x => !x.PropertyType.IsGenericType).Select(x => x.GetValue(item)).ToArray());
        }
        table.Write();
    }

    void AddIngridient()
    {
        Console.Clear();
        Console.WriteLine("Ingridient adding");
        Console.Write("Enter name of new ingridient: ");
        string str = Console.ReadLine() ?? string.Empty;
        if (context.Ingridients.ToList().Exists(x => x.Name == str))
        {
            Console.WriteLine("Ingridient with such name is already exist");
        }
        else
        {
            Ingridient newIngridient = new Ingridient();
            newIngridient.Name = str;
            context.Ingridients.Add(newIngridient);
            context.SaveChanges();
            Console.WriteLine("Ingridient " + str + " is added:)");
        } 
    }

    void RemoveIngridient()
    {
        Console.WriteLine("Ingridient removing");
        int id = InputIntValue("Enter ingridient id to remove: ");
        Ingridient ingridient;
        if(GetIngridientById(id, out ingridient))
        {
            context.Ingridients.Remove(ingridient);
            Console.WriteLine(ingridient + " is deleted");
            context.SaveChanges();  
        }
        else
        {
            Console.WriteLine("There is no ingridients with such id");
        }
    }

    void RemoveDish()
    {
        Console.WriteLine("Dish deleting");
        int id = InputIntValue("Enter dish id to remove: ");
        Dish? dish = context.Dishes.FirstOrDefault(x => x.Id == id);
        if (dish == null)
            Console.WriteLine("There is no such dishes");
        else
        {
            context.Dishes.Remove(dish);
            context.SaveChanges();
            Console.WriteLine("Dish " + dish.Name + " is deleted");
        }
    }

    void AddDish()
    {
        Console.Clear();
        Console.WriteLine("Dish adding");
        Console.Write("Enter name of new dish: ");
        string str = Console.ReadLine() ?? string.Empty; 
        if (context.Dishes.ToList().Exists(x => x.Name == str))
        {
            Console.WriteLine("Dish with this name already exist");
        }
        else
        {
            Dish newDish = new Dish();
            newDish.Name = str;
            double cost = InputDoubleValue("Enter cost: ");
            newDish.Cost = cost;
            int time = InputIntValue("Enter time of cooking in minutes(int): ");
            newDish.CookingTime = time;
            int ingridientsCount = InputIntValue("Enter ingridients count(int): ");
            context.Dishes.Add(newDish);
            int i = 0;
            while (i < ingridientsCount)
            {
                int id = InputIntValue("Enter " + i + "st ingridient id: ");
                Ingridient ingridient;
                if (GetIngridientById(id, out ingridient))
                {
                    IngridientsDish ingridientsDish = new IngridientsDish();
                    ingridientsDish.IngridientId = id;
                    ingridientsDish.Ingridient = ingridient;
                    ingridientsDish.DishId = newDish.Id;
                    ingridientsDish.IngridientWeight = InputIntValue("Input ingridient weight in gramns: ");
                    context.IngridientsDishes.Add(ingridientsDish);
                    i++;
                }
                else
                {
                    Console.WriteLine("There is no such ingridients. Try again.");
                }    
            }
            context.SaveChanges();
            Console.WriteLine("Dish " + newDish.Name + " is added:)");
        }
    }

    bool GetIngridientById(int id, out Ingridient? ingridient)
    {
        ingridient = context.Ingridients.FirstOrDefault(x => x.Id == id);
        if (ingridient == null)
            return false;
        else
            return true;
    }

    double InputDoubleValue(string message)
    {
        Console.Write(message);
        bool isEnd = false;
        double value = 0;
        while(!isEnd)
        {
            string str = Console.ReadLine() ?? string.Empty;
            isEnd = double.TryParse(str, out value);
            if(isEnd == false)
                Console.WriteLine("You need to enter double number. Try again: ");
        }

        return value;
    }

    int InputIntValue(string message)
    {
        Console.Write(message);
        bool isEnd = false;
        int value = 0;
        while (!isEnd)
        {
            string str = Console.ReadLine() ?? string.Empty;
            isEnd = int.TryParse(str, out value);
            if (isEnd == false)
                Console.WriteLine("You need to enter int number. Try again: ");
        }

        return value;
    }
}