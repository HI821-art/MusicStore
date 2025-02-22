using AutoMapper;
using MusicStore.Data;
using Microsoft.EntityFrameworkCore;
using static MappingProfile;

namespace MusicStore.Controllers
{
    public class CustomerController
    {
        private readonly MusicStoreDbContext _context;
        private readonly IMapper _mapper;

        public CustomerController(MusicStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddCustomer(AddCustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password) ||
                string.IsNullOrWhiteSpace(dto.FirstName) || string.IsNullOrWhiteSpace(dto.LastName) ||
                string.IsNullOrWhiteSpace(dto.Email))
            {
                Console.WriteLine("Error: All fields except phone are required.");
                return;
            }

            if (!IsValidEmail(dto.Email))
            {
                Console.WriteLine("Error: Invalid email format.");
                return;
            }

            try
            {
                if (_context.Customers.Any(c => c.Email == dto.Email))
                {
                    Console.WriteLine("Error: This email is already registered.");
                    return;
                }

                var customer = _mapper.Map<Customer>(dto);
                customer.TotalSpent = 0; // Ініціалізація

                _context.Customers.Add(customer);
                _context.SaveChanges();

                Console.WriteLine($"Customer {dto.FirstName} {dto.LastName} added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding customer: {ex.Message}");
            }
        }

        public void ListCustomers()
        {
            var customers = _context.Customers.ToList();
            if (!customers.Any())
            {
                Console.WriteLine("No customers found.");
                return;
            }

            Console.WriteLine("Customers List:");
            foreach (var customer in customers)
            {
                Console.WriteLine($"Id: {customer.Id}, Name: {customer.FirstName} {customer.LastName}");
            }
        }

        public void GetCustomerById(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                Console.WriteLine($"Customer with ID {id} not found.");
                return;
            }

            Console.WriteLine($"Id: {customer.Id}, Name: {customer.FirstName} {customer.LastName}, Email: {customer.Email}, Phone: {customer.Phone ?? "N/A"}");
        }

        public void DeleteCustomer(int id)
        {
            try
            {
                var customer = _context.Customers.Find(id);
                if (customer == null)
                {
                    Console.WriteLine($"Error: Customer with ID {id} not found.");
                    return;
                }

                _context.Customers.Remove(customer);
                _context.SaveChanges();
                Console.WriteLine($"Customer '{customer.FirstName} {customer.LastName}' deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting customer: {ex.Message}");
            }
        }

        public void UpdateCustomer(int id, UpdateCustomerDto dto)
        {
            try
            {
                var customer = _context.Customers.Find(id);
                if (customer == null)
                {
                    Console.WriteLine($"Error: Customer with ID {id} not found.");
                    return;
                }

                _mapper.Map(dto, customer);

                _context.Customers.Update(customer);
                _context.SaveChanges();

                Console.WriteLine($"Customer '{customer.FirstName} {customer.LastName}' updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating customer: {ex.Message}");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
