using System.Collections.ObjectModel;
using System.Windows;
using AddressBook.Data;
using AddressBook.Models;

namespace AddressBook;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly AddressBookContext _context;
    private ObservableCollection<Contact> Contacts { get; set; }
    private Contact SelectedContact { get; set; }
    private User CurrentUser { get; set; }

    public MainWindow()
    {
        var loginWindow = new LoginWindow();
        if (loginWindow.ShowDialog() != true)
        {
            Application.Current.Shutdown();
            return;
        }

        InitializeComponent();
        _context = new AddressBookContext();
        CurrentUser = loginWindow.LoggedInUser;
        this.Title = $"Address Book - {CurrentUser.Username}";
        LoadContacts();
        DataContext = this;
    }

    private void LoadContacts()
    {
        Contacts = new ObservableCollection<Contact>(
            _context.Contacts.Where(c => c.UserId == CurrentUser.Id).ToList()
        );
        contactsGrid.ItemsSource = Contacts;
    }

    private async void AddContact_Click(object sender, RoutedEventArgs e)
    {
        var contact = new Contact { UserId = CurrentUser.Id };
        var contactWindow = new ContactWindow(contact);
        if (contactWindow.ShowDialog() == true)
        {
            _context.Contacts.Add(contactWindow.Contact);
            await _context.SaveChangesAsync();
            LoadContacts();
        }
    }

    private async void EditContact_Click(object sender, RoutedEventArgs e)
    {
        if (contactsGrid.SelectedItem == null) return;

        var contact = (Contact)contactsGrid.SelectedItem;
        var contactWindow = new ContactWindow(contact);
        if (contactWindow.ShowDialog() == true)
        {
            _context.Update(contactWindow.Contact);
            await _context.SaveChangesAsync();
            LoadContacts();
        }
    }

    private async void DeleteContact_Click(object sender, RoutedEventArgs e)
    {
        if (contactsGrid.SelectedItem == null) return;

        var contact = (Contact)contactsGrid.SelectedItem;
        var result = MessageBox.Show("Are you sure you want to delete this contact?", 
            "Confirm Delete", MessageBoxButton.YesNo);
            
        if (result == MessageBoxResult.Yes)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            LoadContacts();
        }
    }
}
