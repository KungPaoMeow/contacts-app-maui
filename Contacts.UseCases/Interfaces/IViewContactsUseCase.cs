namespace Contacts.UseCases.Interfaces
{
    public interface IViewContactsUseCase
    {
        Task<List<CoreBuisness.Contact>> ExecuteAsync(string filterText);
    }
}