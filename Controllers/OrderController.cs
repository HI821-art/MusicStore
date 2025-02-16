using Microsoft.EntityFrameworkCore;
using MusicStore.Data;


namespace MusicStore.Controllers
{
    public class OrderController
    {
        private readonly MusicStoreDbContext _context;

        public OrderController(MusicStoreDbContext context)
        {
            _context = context;
        }

        public void AddOrder(int customerId, decimal totalPrice, DateTime orderDate)
        {
            if (totalPrice <= 0)
            {
                Console.WriteLine("Error: Total price must be greater than zero.");
                return;
            }

            try
            {
                var customer = _context.Customers.Find(customerId);
                if (customer == null)
                {
                    Console.WriteLine($"Error: Customer with ID {customerId} not found.");
                    return;
                }

                var order = new Order
                {
                    CustomerId = customerId,
                    Customer = customer,
                    TotalPrice = totalPrice,
                    OrderDate = orderDate
                };

                _context.Orders.Add(order);
                _context.SaveChanges();

                Console.WriteLine($"Order added successfully for customer '{customer.FirstName} {customer.LastName}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding order: {ex.Message}");
            }
        }

        public void ListOrders()
        {
            var orders = _context.Orders.Include(o => o.Customer).ToList();
            if (!orders.Any())
            {
                Console.WriteLine("No orders found.");
                return;
            }

            Console.WriteLine("Orders List:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.Id}, Customer: {order.Customer.FirstName} {order.Customer.LastName}, Order Date: {order.OrderDate}, Total Price: {order.TotalPrice:C}");
            }
        }

        public void GetOrderById(int id)
        {
            var order = _context.Orders.Include(o => o.Customer).FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                Console.WriteLine($"Order with ID {id} not found.");
                return;
            }

            Console.WriteLine($"Order ID: {order.Id}, Customer: {order.Customer.FirstName} {order.Customer.LastName}, Order Date: {order.OrderDate}, Total Price: {order.TotalPrice:C}");
        }

        public void DeleteOrder(int id)
        {
            try
            {
                var order = _context.Orders.Find(id);
                if (order == null)
                {
                    Console.WriteLine($"Error: Order with ID {id} not found.");
                    return;
                }

                _context.Orders.Remove(order);
                _context.SaveChanges();
                Console.WriteLine($"Order ID {order.Id} deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting order: {ex.Message}");
            }
        }

        public void UpdateOrder(int id, decimal totalPrice, DateTime orderDate)
        {
            try
            {
                var order = _context.Orders.Find(id);
                if (order == null)
                {
                    Console.WriteLine($"Error: Order with ID {id} not found.");
                    return;
                }

                order.TotalPrice = totalPrice;
                order.OrderDate = orderDate;

                _context.Orders.Update(order);
                _context.SaveChanges();

                Console.WriteLine($"Order ID {order.Id} updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating order: {ex.Message}");
            }
        }
    }
}