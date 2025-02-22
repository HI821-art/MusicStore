using AutoMapper;
using MusicStore.Data;
using Microsoft.EntityFrameworkCore;
using static MappingProfile;

namespace MusicStore.Controllers
{
    public class OrderController
    {
        private readonly MusicStoreDbContext _context;
        private readonly IMapper _mapper;

        public OrderController(MusicStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddOrder(AddOrderDto dto)
        {
            if (dto.TotalPrice <= 0)
            {
                Console.WriteLine("Error: Total price must be greater than zero.");
                return;
            }

            try
            {
                var customer = _context.Customers.Find(dto.CustomerId);
                if (customer == null)
                {
                    Console.WriteLine($"Error: Customer with ID {dto.CustomerId} not found.");
                    return;
                }

                var order = _mapper.Map<Order>(dto);
                order.Customer = customer;

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

        public void UpdateOrder(int id, UpdateOrderDto dto)
        {
            try
            {
                var order = _context.Orders.Find(id);
                if (order == null)
                {
                    Console.WriteLine($"Error: Order with ID {id} not found.");
                    return;
                }

                _mapper.Map(dto, order);

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
