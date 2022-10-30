namespace lab4.Cafe.Models
{
    public static class SampleData
    {
        public static void Initialize(CafeContext context)
        {
            Ingridient[] ingridients;
            Dish[] dishes;

            // Ingridient filling
            if (!context.Ingridients.Any())
            {
                string[] ingridientNames = new string[] { "coffee", "milk", "tea", "sugar", "lemon", "water" };
                ingridients = new Ingridient[ingridientNames.Length];
                for (int i = 0; i < ingridientNames.Length; i++)
                {
                    Ingridient newIngridient = new Ingridient();
                    newIngridient.Name = ingridientNames[i];
                    newIngridient.Id = i;
                    ingridients[i] = newIngridient;
                }
                context.Ingridients.AddRange(ingridients);
            }
            else
                ingridients = context.Ingridients.ToArray();
            
            //Dish filling
            if(!context.Dishes.Any())
            {
                string[] dishNames = new string[] { "milk coffee", "tea with sugar", "tea with lemon" };
                float[] dishCosts = new float[] { 3.1f, 2.3f, 2.7f };
                int[] cookingTimes = new int[] { 5, 3, 3 };
                dishes = new Dish[dishNames.Length];
                for(int i = 0; i < dishNames.Length; i++)
                {
                    Dish newDish = new Dish();
                    newDish.Id = i;
                    newDish.Name = dishNames[i];
                    newDish.Cost = dishCosts[i];
                    newDish.CookingTime = cookingTimes[i];
                    dishes[i] = newDish;
                }

                context.Dishes.AddRange(dishes);
            }
            else
                dishes = context.Dishes.ToArray();

            //Ingridientdishes filling
            if(!context.IngridientsDishes.Any())
            {
                IngridientsDish ingridientDish1 = new IngridientsDish();
                ingridientDish1.DishId = dishes[0].Id;
                ingridientDish1.IngridientId = ingridients[0].Id;
                ingridientDish1.IngridientWeight = 10;
                IngridientsDish ingridientDish2 = new IngridientsDish();
                ingridientDish2.DishId = dishes[0].Id;
                ingridientDish2.IngridientId = ingridients[1].Id;
                ingridientDish2.IngridientWeight = 50;
                IngridientsDish ingridientDish3 = new IngridientsDish();
                ingridientDish3.DishId = dishes[1].Id;
                ingridientDish3.IngridientId = ingridients[2].Id;
                ingridientDish3.IngridientWeight = 5;
                IngridientsDish ingridientDish4 = new IngridientsDish();
                ingridientDish4.DishId = dishes[1].Id;
                ingridientDish4.IngridientId = ingridients[3].Id;
                ingridientDish4.IngridientWeight = 5;
                IngridientsDish ingridientDish5 = new IngridientsDish();
                ingridientDish5.DishId = dishes[2].Id;
                ingridientDish5.IngridientId = ingridients[2].Id;
                ingridientDish5.IngridientWeight = 5;
                IngridientsDish ingridientDish6 = new IngridientsDish();
                ingridientDish6.DishId = dishes[2].Id;
                ingridientDish6.IngridientId = ingridients[4].Id;
                ingridientDish6.IngridientWeight = 8;
                IngridientsDish ingridientDish7 = new IngridientsDish();
                ingridientDish1.DishId = dishes[0].Id;
                ingridientDish1.IngridientId = ingridients[5].Id;
                ingridientDish1.IngridientWeight = 150;
                IngridientsDish ingridientDish8 = new IngridientsDish();
                ingridientDish1.DishId = dishes[1].Id;
                ingridientDish1.IngridientId = ingridients[5].Id;
                ingridientDish1.IngridientWeight = 200;
                IngridientsDish ingridientDish9 = new IngridientsDish();
                ingridientDish1.DishId = dishes[2].Id;
                ingridientDish1.IngridientId = ingridients[5].Id;
                ingridientDish1.IngridientWeight = 170;
                context.IngridientsDishes.AddRange(ingridientDish1, ingridientDish2, ingridientDish3, ingridientDish4, ingridientDish5, ingridientDish6, ingridientDish7, ingridientDish8, ingridientDish9);
            }
            
            context.SaveChanges();
        }

    }
}
