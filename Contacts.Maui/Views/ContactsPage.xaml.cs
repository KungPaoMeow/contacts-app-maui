using Contacts.Maui.Models;
using Contacts.UseCases.Interfaces;
using System.Collections.ObjectModel;
using Contact = Contacts.CoreBuisness.Contact;

namespace Contacts.Maui.Views;

public partial class ContactsPage : ContentPage
{
    private readonly IViewContactsUseCase _viewContactsUseCase;
    private readonly IDeleteContactUseCase deleteContactUseCase;

    public ContactsPage(IViewContactsUseCase viewContactsUseCase, IDeleteContactUseCase deleteContactUseCase)
    {
        InitializeComponent();
        this._viewContactsUseCase = viewContactsUseCase;
        this.deleteContactUseCase = deleteContactUseCase;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        SearchBar.Text = string.Empty;
        LoadContacts();
    }

    private async void listContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (listContacts.SelectedItem != null)
        {
            await Shell.Current.GoToAsync($"{nameof(EditContactPage)}?Id={((Contact)listContacts.SelectedItem).ContactId}");
        }
    }

    private void listContacts_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        listContacts.SelectedItem = null;
    }

    private void btnAdd_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AddContactPage));
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        MenuItem menuItem = sender as MenuItem;
        Contact contact = menuItem.CommandParameter as Contact;
        //ContactRepository.DeleteContact(contact.ContactId);
        await deleteContactUseCase.ExecuteAsync(contact.ContactId);

        LoadContacts();
    }

    private async void LoadContacts()     // Refresh list view
    {
        ObservableCollection<Contact> contacts = new ObservableCollection<Contact>(await this._viewContactsUseCase.ExecuteAsync(string.Empty));    // views subscribe to observable data they contain
        listContacts.ItemsSource = contacts;
    }

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        //ObservableCollection<Contact> contacts = new ObservableCollection<Contact>(ContactRepository.SearchContacts(((SearchBar) sender).Text));
        ObservableCollection<Contact> contacts = new ObservableCollection<Contact>(await this._viewContactsUseCase.ExecuteAsync(((SearchBar)sender).Text));
        listContacts.ItemsSource = contacts;
    }
}