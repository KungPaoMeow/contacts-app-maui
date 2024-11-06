namespace Contacts.CoreBuisness
{
    // All the code in this file is included in all platforms.
    public class Contact
    {
        public int ContactId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}
