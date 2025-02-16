public class CustomerService : ICustomerService
{
    private readonly IRepository<Customer> _customerRepository;

    public CustomerService(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public void RegisterCustomer(string firstName, string lastName, string email, string? phone = null)
    {
        try
        {
            var existingCustomer = _customerRepository.Query().FirstOrDefault(c => c.Email == email);
            if (existingCustomer != null)
            {
                throw new Exception("Customer with the same email already exists.");
            }

            var customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                TotalSpent = 0,
                Orders = new List<Order>(),
                Reservations = new List<Reservation>()
            };

            _customerRepository.AddAsync(customer).Wait();
        }
        catch (Exception ex)
        {
            throw new Exception("Error registering customer", ex);
        }
    }

    public void RegisterCustomer(string name, string email)
    {
        throw new NotImplementedException();
    }
}
