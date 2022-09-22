using Microsoft.EntityFrameworkCore;
using lab2.Entities;

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

                    break;
                case 5:

                    break;
                case 6:

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
        Console.WriteLine("4. ")
        Console.WriteLine("11. Exit");
    }

    void ListOutput<T>(IEnumerable<T> list) where T: class
    {
        int i = 1;
        foreach(var item in list)
        {
            Console.WriteLine(i.ToString() + ". " + item.ToString());
            i++;
        }
    }
}