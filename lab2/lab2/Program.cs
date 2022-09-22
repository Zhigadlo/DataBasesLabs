using Microsoft.EntityFrameworkCore;
using lab2.Entities;
using ConsoleTables;

using (CafeContext context = new CafeContext())
{
    bool isEnd = false;
    while(!isEnd)
    {
        Menu();
        Console.Write("Enter menu number(int): ");
        string? str = Console.ReadLine();
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

                    break;
                case 8:

                    break;
                case 9:

                    break;
                case 10:

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
        Console.WriteLine("1. Output ingridient list");
        Console.WriteLine("2. Output professions that more than 13 employees have");
        Console.WriteLine("3. Output count of people that use cash");
        Console.WriteLine("4. Output all employees");
        Console.WriteLine("5. Output all ingridients info in wahrehouses that costs more than 400");
        Console.WriteLine("6. Add ingridient");
        Console.WriteLine("11. Exit");
    }

    void ListOutput<T>(IEnumerable<T> dbset) where T: class
    {
        ConsoleTable table = new ConsoleTable(typeof(T).GetProperties().Where(x => !x.PropertyType.IsGenericType).Select(x => x.Name).ToArray());

        foreach(var item in dbset)
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
        string? str = Console.ReadLine();
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
}