using System.Windows;
using AddressBook.Data;
using AddressBook.Models;
using AddressBook.Services;

namespace AddressBook;

public partial class LoginWindow : Window
{
    private readonly AuthService _authService;

    public User LoggedInUser { get; private set; }

    public LoginWindow()
    {
        InitializeComponent();
        _authService = new AuthService(new AddressBookContext());
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            LoggedInUser = _authService.Login(txtUsername.Text, txtPassword.Password);
            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            txtError.Text = ex.Message;
        }
    }

    private void Register_Click(object sender, RoutedEventArgs e)
    {
        var registerWindow = new RegisterWindow(this);
        registerWindow.ShowDialog();
    }
}