using MusicStore.Data;

namespace MusicStore.Controllers
{
    public class ReservationController
    {
        private readonly MusicStoreDbContext _context;

        public ReservationController(MusicStoreDbContext context)
        {
            _context = context;
        }

        public void AddReservation(int vinylRecordId, int customerId)
        {
            try
            {
                var vinylRecord = _context.VinylRecords.Find(vinylRecordId);
                var customer = _context.Customers.Find(customerId);

                if (vinylRecord == null)
                {
                    Console.WriteLine($"Error: Vinyl record with ID {vinylRecordId} not found.");
                    return;
                }

                if (customer == null)
                {
                    Console.WriteLine($"Error: Customer with ID {customerId} not found.");
                    return;
                }

                var reservation = new Reservation
                {
                    ReservedAt = DateTime.Now,
                    VinylRecordId = vinylRecordId,
                    CustomerId = customerId,
                    VinylRecord = vinylRecord,
                    Customer = customer
                };

                _context.Reservations.Add(reservation);
                _context.SaveChanges();

                Console.WriteLine($"Reservation for vinyl record '{vinylRecord.Name}' by '{customer.FirstName} {customer.LastName}' added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding reservation: {ex.Message}");
            }
        }

        public void ListReservations()
        {
            var reservations = _context.Reservations.ToList();
            if (reservations.Count == 0)
            {
                Console.WriteLine("No reservations found.");
                return;
            }

            Console.WriteLine("Reservations List:");
            foreach (var reservation in reservations)
            {
                Console.WriteLine($"Id: {reservation.Id}, Customer: {reservation.Customer.FirstName} {reservation.Customer.LastName}, Vinyl Record: {reservation.VinylRecord.Name}, Date: {reservation.ReservedAt}");
            }
        }

        public void GetReservationById(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation == null)
            {
                Console.WriteLine($"Reservation with ID {id} not found.");
                return;
            }

            Console.WriteLine($"Id: {reservation.Id}, Customer: {reservation.Customer.FirstName} {reservation.Customer.LastName}, Vinyl Record: {reservation.VinylRecord.Name}, Date: {reservation.ReservedAt}");
        }

        public void DeleteReservation(int id)
        {
            try
            {
                var reservation = _context.Reservations.Find(id);
                if (reservation == null)
                {
                    Console.WriteLine($"Error: Reservation with ID {id} not found.");
                    return;
                }

                _context.Reservations.Remove(reservation);
                _context.SaveChanges();
                Console.WriteLine($"Reservation for '{reservation.VinylRecord.Name}' by '{reservation.Customer.FirstName} {reservation.Customer.LastName}' deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting reservation: {ex.Message}");
            }
        }
    }
}
