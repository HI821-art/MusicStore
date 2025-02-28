﻿public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }

    public decimal TotalSpent { get; set; }

    public List<Order> Orders { get; set; } = new List<Order>();
    public List<Reservation> Reservations { get; set; } = new List<Reservation>();
}