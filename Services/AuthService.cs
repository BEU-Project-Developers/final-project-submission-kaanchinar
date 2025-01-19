using AddressBook.Data;
using AddressBook.Models;
using BCrypt.Net;
namespace AddressBook.Services;


public class AuthService
{
    private readonly AddressBookContext _context;

    public AuthService(AddressBookContext context)
    {
        _context = context;
    }

    public User RegisterUser(string username, string password, string email)
    {
        if (_context.Users.Any(u => u.Username == username))
            throw new Exception("Username already exists");

        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Contacts = new List<Contact>()
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User Login(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new Exception("Invalid username or password");

        return user;
    }
}