namespace Contacts.UseCases.Interfaces
{
    public interface IViewContactUseCase
    {
        Task<CoreBuisness.Contact> ExecuteAsync(int contactId);
    }
}