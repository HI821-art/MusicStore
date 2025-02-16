using MusicStore.Data;
namespace MusicStore.Controllers
{
    public class CustomerController
    {
        private readonly MusicStoreDbContext _context;

        public CustomerController(MusicStoreDbContext context)
        {
            _context = context;
        }

        // Метод для додавання клієнта
        public void AddCustomer(string username, string password, string firstName, string lastName, string email, string phone)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Error: All fields except phone are required.");
                return;
            }

            // Додаткові перевірки формату email та телефону
            if (!IsValidEmail(email))
            {
                Console.WriteLine("Error: Invalid email format.");
                return;
            }

            try
            {
                if (_context.Customers.Any(c => c.Email == email))
                {
                    Console.WriteLine("Error: This email is already registered.");
                    return;
                }

                var customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone,
                    TotalSpent = 0 // Ініціалізація
                };

                _context.Customers.Add(customer);
                _context.SaveChanges();

                Console.WriteLine($"Customer {firstName} {lastName} added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding customer: {ex.Message}");
            }
        }

        // Метод для переліку всіх клієнтів
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

        // Метод для отримання клієнта за ID
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

        // Метод для видалення клієнта
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
