using BTech.Domain.Entities;
using BTech.Domain.Interfaces;
using BlitzTech.Data.Context;


namespace BTech.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.Include(o => o.Items).ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Include(o => o.Items).FirstOrDefault(o => o.OrderId == orderId);
        }

        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void DeleteOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
        }

        public void UpdateOrder(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
