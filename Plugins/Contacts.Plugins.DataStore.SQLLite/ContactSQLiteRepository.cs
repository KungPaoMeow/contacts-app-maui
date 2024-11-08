using Contacts.Plugins.DataStore.SQLite;
using Contacts.UseCases.PluginInterfaces;
using SQLite;
using Contact = Contacts.CoreBuisness.Contact;

namespace Contacts.Plugins.DataStore.SQLLite
{
    // All the code in this file is included in all platforms.
    public class ContactSQLiteRepository : IContactRepository
    {
        private SQLiteAsyncConnection _database;

        public ContactSQLiteRepository()
        {
            this._database = new SQLiteAsyncConnection(Constants.DatabasePath);
            this._database.CreateTableAsync<Contact>();
        }

        public async Task AddContactAsync(Contact contact)
        {
            await this._database.InsertAsync(contact);
        }

        public async Task DeleteContactAsync(int contactId)
        {
            Contact contact = await GetContactByIdAsync(contactId);
            if (contact != null && contact.ContactId == contactId) 
                await this._database.DeleteAsync(contact);
        }

        public async Task<Contact> GetContactByIdAsync(int contactId)
        {
            return await this._database.Table<Contact>().Where(x => x.ContactId == contactId).FirstOrDefaultAsync();
        }

        public async Task<List<Contact>> GetContactsAsync(string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
                return await this._database.Table<Contact>().ToListAsync();

            // Field starts with filter
            return await this._database.QueryAsync<Contact>(@"
                SELECT *
                FROM Contact
                WHERE
                    Name LIKE ? OR
                    Email LIKE ? OR
                    Phone LIKE ? OR
                    Address LIKE ?",
                    $"{filterText}%",
                    $"{filterText}%",
                    $"{filterText}%",
                    $"{filterText}%");
        }

        public async Task UpdateContactAsync(int contactId, Contact contact)
        {
            if (contact.ContactId == contactId)
                await this._database.UpdateAsync(contact);
        }
    }
}
