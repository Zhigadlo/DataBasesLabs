using lab7.Data;
using Microsoft.EntityFrameworkCore;

namespace lab7.Services
{
    public class OrderService
    {
        private CafeContext _context;

        public OrderService(CafeContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll() 
        {
            return _context.Orders.Include(o => o.Employee).AsEnumerable();
        }

        public Order? Get(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public int Create(Order? order)
        {
            if (order != null)
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                return order.Id;
            }
            else
                return -1;
        }

        public int Update(Order? order)
        {
            if (order != null)
            {
                _context.Orders.Update(order);
                _context.SaveChanges();
                return order.Id;
            }
            else
                return -1;
        }

        public Order? Delete(int id)
        {
            Order? order = _context.Orders.FirstOrDefault(o => o.Id == id);

            if(order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }

            return order;
        }
    }
}
