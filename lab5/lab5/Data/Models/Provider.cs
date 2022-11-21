namespace lab5.Data.Models
{
    public partial class Provider
    {
        public Provider()
        {
            IngridientsWarehouses = new HashSet<IngridientsWarehouse>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<IngridientsWarehouse> IngridientsWarehouses { get; set; }
    }
}
