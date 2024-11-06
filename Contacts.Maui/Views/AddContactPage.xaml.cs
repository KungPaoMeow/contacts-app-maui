using Contacts.Maui.Models;
using Contacts.UseCases.Interfaces;

using Contact = Contacts.CoreBuisness.Contact;

namespace Contacts.Maui.Views;

public partial class AddContactPage : ContentPage
{
    private readonly IAddContactUseCase addContactUseCase;

    public AddContactPage(IAddContactUseCase addContactUseCase)
	{
		InitializeComponent();
        this.addContactUseCase = addContactUseCase;
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private async void contactCtrl_OnSave(object sender, EventArgs e)
    {
         await this.addContactUseCase.ExecuteAsync(new Contact
        {
            Name = contactCtrl.Name,
            Email = contactCtrl.Email,
            Address = contactCtrl.Address,
            Phone = contactCtrl.Phone

        });

        await Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
    }

    private void contactCtrl_OnCancel(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
    }

    private void contactCtrl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}