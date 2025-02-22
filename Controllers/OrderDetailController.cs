using AutoMapper;
using MusicStore.Data;
using Microsoft.EntityFrameworkCore;
using static MappingProfile;

namespace MusicStore.Controllers
{
    public class OrderDetailController
    {
        private readonly MusicStoreDbContext _context;
        private readonly IMapper _mapper;

        public OrderDetailController(MusicStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddOrderDetail(AddOrderDetailDto dto)
        {
            if (dto.Quantity <= 0 || dto.Price <= 0)
            {
                Console.WriteLine("Error: Quantity and price must be greater than zero.");
                return;
            }

            try
            {
                var order = _context.Orders.Find(dto.OrderId);
                if (order == null)
                {
                    Console.WriteLine($"Error: Order with ID {dto.OrderId} not found.");
                    return;
                }

                var vinylRecord = _context.VinylRecords.Find(dto.VinylRecordId);
                if (vinylRecord == null)
                {
                    Console.WriteLine($"Error: Vinyl record with ID {dto.VinylRecordId} not found.");
                    return;
                }

                var orderDetail = _mapper.Map<OrderDetail>(dto);

                _context.OrderDetails.Add(orderDetail);
                _context.SaveChanges();

                Console.WriteLine($"Order detail added for Order ID {dto.OrderId}, Vinyl Record: {vinylRecord.Name}, Quantity: {dto.Quantity}, Price: {dto.Price:C}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding order detail: {ex.Message}");
            }
        }

        public void ListOrderDetails(int orderId)
        {
            var orderDetails = _context.OrderDetails
                .Where(od => od.OrderId == orderId)
                .Include(od => od.VinylRecord)
                .ToList();

            if (!orderDetails.Any())
            {
                Console.WriteLine($"No details found for Order ID {orderId}.");
                return;
            }

            Console.WriteLine($"Order Details for Order ID {orderId}:");
            foreach (var orderDetail in orderDetails)
            {
                Console.WriteLine($"Vinyl Record: {orderDetail.VinylRecord.Name}, Quantity: {orderDetail.Quantity}, Price: {orderDetail.Price:C}");
            }
        }

        public void GetOrderDetailById(int id)
        {
            var orderDetail = _context.OrderDetails
                .Include(od => od.VinylRecord)
                .FirstOrDefault(od => od.Id == id);

            if (orderDetail == null)
            {
                Console.WriteLine($"Order Detail with ID {id} not found.");
                return;
            }

            Console.WriteLine($"Order Detail ID: {orderDetail.Id}, Vinyl Record: {orderDetail.VinylRecord.Name}, Quantity: {orderDetail.Quantity}, Price: {orderDetail.Price:C}");
        }

        public void DeleteOrderDetail(int id)
        {
            try
            {
                var orderDetail = _context.OrderDetails.Find(id);
                if (orderDetail == null)
                {
                    Console.WriteLine($"Error: Order Detail with ID {id} not found.");
                    return;
                }

                _context.OrderDetails.Remove(orderDetail);
                _context.SaveChanges();
                Console.WriteLine($"Order Detail ID {orderDetail.Id} deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting order detail: {ex.Message}");
            }
        }

        public void UpdateOrderDetail(int id, UpdateOrderDetailDto dto)
        {
            try
            {
                var orderDetail = _context.OrderDetails.Find(id);
                if (orderDetail == null)
                {
                    Console.WriteLine($"Error: Order Detail with ID {id} not found.");
                    return;
                }

                orderDetail.Quantity = dto.Quantity;
                orderDetail.Price = dto.Price;

                _context.OrderDetails.Update(orderDetail);
                _context.SaveChanges();

                Console.WriteLine($"Order Detail ID {orderDetail.Id} updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating order detail: {ex.Message}");
            }
        }
    }
}
