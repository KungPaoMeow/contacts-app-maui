namespace Contacts.UseCases.Interfaces
{
    public interface IAddContactUseCase
    {
        Task ExecuteAsync(CoreBuisness.Contact contact);
    }
}