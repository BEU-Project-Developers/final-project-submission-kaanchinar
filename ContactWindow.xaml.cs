using System.Windows;
using AddressBook.Models;

namespace AddressBook;

public partial class ContactWindow : Window
{
    public Contact Contact { get; private set; }

    public ContactWindow(Contact contact)
    {
        InitializeComponent();
        Contact = contact;
        LoadContact();
    }

    private void LoadContact()
    {
        txtFirstName.Text = Contact.FirstName;
        txtLastName.Text = Contact.LastName;
        txtEmail.Text = Contact.Email;
        txtPhoneNumber.Text = Contact.PhoneNumber;
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        Contact.FirstName = txtFirstName.Text;
        Contact.LastName = txtLastName.Text;
        Contact.Email = txtEmail.Text;
        Contact.PhoneNumber = txtPhoneNumber.Text;

        DialogResult = true;
        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}