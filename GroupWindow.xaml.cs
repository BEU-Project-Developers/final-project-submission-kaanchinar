using System.Collections.ObjectModel;
using System.Windows;
using AddressBook.Models;

namespace AddressBook
{
    public partial class GroupWindow : Window
    {
        public Group Group { get; private set; }
        public ObservableCollection<Contact> Contacts { get; private set; }

        public GroupWindow(Group group, ObservableCollection<Contact> contacts, Window owner)
        {
            InitializeComponent();
            Group = group;
            Contacts = contacts;
            txtGroupName.Text = group.Name;
            txtDescription.Text = group.Description;
            lstContacts.ItemsSource = Contacts;
            this.Owner = owner;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Group.Name = txtGroupName.Text;
            Group.Description = txtDescription.Text;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}