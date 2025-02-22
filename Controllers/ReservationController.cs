using MusicStore.Data;

public class ReservationController
{
    private readonly MusicStoreDbContext _context;

    public ReservationController(MusicStoreDbContext context)
    {
        _context = context;
    }

    private T FindEntityById<T>(int id) where T : class
    {
        var entity = _context.Set<T>().Find(id);
        if (entity == null)
        {
            OutputMessage($"Error: {typeof(T).Name} with ID {id} not found.");
        }
        return entity;
    }

    private void OutputMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void AddReservation(int vinylRecordId, int customerId)
    {
        try
        {
            var vinylRecord = FindEntityById<VinylRecord>(vinylRecordId);
            var customer = FindEntityById<Customer>(customerId);

            if (vinylRecord == null || customer == null) return;

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

            OutputMessage($"Reservation for vinyl record '{vinylRecord.Name}' by '{customer.FirstName} {customer.LastName}' added successfully.");
        }
        catch (Exception ex)
        {
            OutputMessage($"Error adding reservation: {ex.Message}");
        }
    }

    public void ListReservations()
    {
        var reservations = _context.Reservations.ToList();
        if (reservations.Count == 0)
        {
            OutputMessage("No reservations found.");
            return;
        }

        OutputMessage("Reservations List:");
        foreach (var reservation in reservations)
        {
            OutputMessage($"Id: {reservation.Id}, Customer: {reservation.Customer.FirstName} {reservation.Customer.LastName}, Vinyl Record: {reservation.VinylRecord.Name}, Date: {reservation.ReservedAt}");
        }
    }

    public void GetReservationById(int id)
    {
        var reservation = _context.Reservations.Find(id);
        if (reservation == null)
        {
            OutputMessage($"Reservation with ID {id} not found.");
            return;
        }

        OutputMessage($"Id: {reservation.Id}, Customer: {reservation.Customer.FirstName} {reservation.Customer.LastName}, Vinyl Record: {reservation.VinylRecord.Name}, Date: {reservation.ReservedAt}");
    }

    public void DeleteReservation(int id)
    {
        try
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation == null)
            {
                OutputMessage($"Error: Reservation with ID {id} not found.");
                return;
            }

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
            OutputMessage($"Reservation for '{reservation.VinylRecord.Name}' by '{reservation.Customer.FirstName} {reservation.Customer.LastName}' deleted successfully.");
        }
        catch (Exception ex)
        {
            OutputMessage($"Error deleting reservation: {ex.Message}");
        }
    }
}
