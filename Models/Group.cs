namespace AddressBook.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}