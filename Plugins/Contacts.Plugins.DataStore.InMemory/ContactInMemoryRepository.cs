using Contacts.UseCases.PluginInterfaces;
using Contact = Contacts.CoreBuisness.Contact;

namespace Contacts.Plugins.DataStore.InMemory
{
    // All the code in this file is included in all platforms.
    public class ContactInMemoryRepository : IContactRepository
    {
        private static List<Contact> _contacts;

        public ContactInMemoryRepository()
        {
             _contacts = new List<Contact>
            {
                new Contact { ContactId=1, Name="John Pork", Email="JohnPork@gmail.com" },
                new Contact { ContactId=2, Name="Jane Doe", Email="JaneDoe@gmail.com" },
                new Contact { ContactId=3, Name="Tom Hanks", Email="TomHanks@gmail.com" },
                new Contact { ContactId=4, Name="Frank Liu", Email="FrankLiu@gmail.com" }
            };
        }

        public Task AddContactAsync(Contact contact)
        {
            if (_contacts.Count > 0)
            {
                int maxId = _contacts.Max(x => x.ContactId);
                contact.ContactId = maxId + 1;
            }
            else contact.ContactId = 1;

            _contacts.Add(contact);
            return Task.CompletedTask;
        }

        public Task DeleteContactAsync(int contactId)
        {
            Contact? contact = _contacts.FirstOrDefault(x => x.ContactId == contactId);
            if (contact != null)
            {
                _contacts.Remove(contact);
            }
            return Task.CompletedTask;
        }

        public Task<Contact> GetContactByIdAsync(int contactId)
        {
            Contact? contact = _contacts.FirstOrDefault(x => x.ContactId == contactId);
            if (contact != null)
            {
                return Task.FromResult(new Contact
                {
                    ContactId = contactId,
                    Name = contact.Name,
                    Address = contact.Address,
                    Email = contact.Email,
                    Phone = contact.Phone
                });
            }
            return null;
        }

        public Task<List<Contact>> GetContactsAsync(string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
                return Task.FromResult(_contacts);

            List<Contact>? contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Email) && x.Email.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            else
                return Task.FromResult(contacts);

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Phone) && x.Phone.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            else
                return Task.FromResult(contacts);

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Address) && x.Address.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            else
                return Task.FromResult(contacts);

            if (contacts != null)
                return Task.FromResult(contacts);
          
            return Task.FromResult(new List<Contact>());
        }

        public Task UpdateContactAsync(int contactId, Contact contact)      // doesn't need to be async for in memory here
        {
            if (contactId != contact.ContactId) return Task.CompletedTask;

            Contact? contactToUpdate = _contacts.FirstOrDefault(x => x.ContactId == contactId);
            if (contactToUpdate != null)
            {
                contactToUpdate.Name = contact.Name;
                contactToUpdate.Address = contact.Address;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Phone = contact.Phone;
            }

            return Task.CompletedTask;
        }
    }
}
