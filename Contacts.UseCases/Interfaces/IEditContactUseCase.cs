namespace Contacts.UseCases.Interfaces
{
    public interface IEditContactUseCase
    {
        Task ExecuteAsync(int contactId, CoreBuisness.Contact contact);
    }
}