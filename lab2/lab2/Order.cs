namespace lab2;

public partial class Order
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerPhoneNumber { get; set; }

    public int PaymentMethod { get; set; }

    public int IsCompleted { get; set; }

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public override string ToString()
    {
        return "Date: " + OrderDate.ToShortDateString() + ", Customer: " + CustomerName + ", Customer phone: " +
            CustomerPhoneNumber + ", PaymentMethod: " + PaymentMethod + ", Is complited: " + IsCompleted + ", Employee: " + Employee.LastName;
    }

    public virtual ICollection<OrderDish> OrderDishes { get; } = new List<OrderDish>();
}
