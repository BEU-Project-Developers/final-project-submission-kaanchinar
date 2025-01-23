namespace AddressBook.Models;

public class ChangeLog
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string Action { get; set; } // Create, Update, Delete
    public string Description { get; set; }
}