using System.Windows;
using AddressBook.Data;
using AddressBook.Services;

namespace AddressBook;

public partial class RegisterWindow : Window
{
    private readonly AuthService _authService;

    public RegisterWindow()
    {
        InitializeComponent();
        _authService = new AuthService(new AddressBookContext());
    }

    private void Register_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (txtPassword.Password != txtConfirmPassword.Password)
            {
                txtError.Text = "Passwords do not match";
                return;
            }

            _authService.RegisterUser(txtUsername.Text, txtPassword.Password, txtEmail.Text);
            MessageBox.Show("Registration successful! You can now login.", "Success");
            Close();
        }
        catch (Exception ex)
        {
            txtError.Text = ex.Message;
        }
    }
}
