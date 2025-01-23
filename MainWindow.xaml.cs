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
    public ObservableCollection<Group> Groups { get; set; }
    public Group SelectedGroup { get; set; }
    private ObservableCollection<Contact> Contacts { get; set; }
    //private Contact SelectedContact { get; set; }
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
        LoadGroups();
        DataContext = this;
    }

    private void LoadContacts()
    {
        Contacts = new ObservableCollection<Contact>(
            _context.Contacts.Where(c => c.UserId == CurrentUser.Id).ToList()
        );
        contactsGrid.ItemsSource = Contacts;
    }
    
    private void LoadGroups()
    {
        Groups = new ObservableCollection<Group>(
            // _context.Groups.Where(g => g.Contacts.Any(c => c.UserId == CurrentUser.Id)).ToList()
            _context.Groups.ToList()
        );
        GroupsGrid.ItemsSource = Groups;
    }

    private async void AddContact_Click(object sender, RoutedEventArgs e)
    {
        var contact = new Contact { UserId = CurrentUser.Id };
        var contactWindow = new ContactWindow(contact, this);
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
        var contactWindow = new ContactWindow(contact,this);
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

    private async void AddGroup_Click(object sender, RoutedEventArgs e)
    {
        var group = new Group();
        var groupWindow = new GroupWindow(group, Contacts, this);
        if (groupWindow.ShowDialog() == true)
        {
            _context.Groups.Add(groupWindow.Group);
            await _context.SaveChangesAsync();
            LoadGroups();
        }
    }

    private async void EditGroup_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedGroup == null) return;

        var groupWindow = new GroupWindow(SelectedGroup, Contacts,this);
        if (groupWindow.ShowDialog() == true)
        {
            _context.Update(groupWindow.Group);
            await _context.SaveChangesAsync();
            LoadGroups();
        }
    }
    
    private async void DeleteGroup_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedGroup == null) return;

        var result = MessageBox.Show("Are you sure you want to delete this group?", 
            "Confirm Delete", MessageBoxButton.YesNo);
            
        if (result == MessageBoxResult.Yes)
        {
            _context.Groups.Remove(SelectedGroup);
            await _context.SaveChangesAsync();
            LoadGroups();
        }
    }
}
