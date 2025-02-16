public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } // In a real app, hash the password
}