using System;
using System.Collections.Generic;

namespace lab5.Data.Models
{
    public partial class Ingridient
    {
        public Ingridient()
        {
            IngridientsDishes = new HashSet<IngridientsDish>();
            IngridientsWarehouses = new HashSet<IngridientsWarehouse>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<IngridientsDish> IngridientsDishes { get; set; }
        public virtual ICollection<IngridientsWarehouse> IngridientsWarehouses { get; set; }
    }
}
