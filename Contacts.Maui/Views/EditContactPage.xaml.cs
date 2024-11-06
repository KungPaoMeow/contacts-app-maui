using Contacts.Maui.Models;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.Views;

[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
    private CoreBuisness.Contact _contact;
    private readonly IViewContactUseCase viewContactUseCase;
    private readonly IEditContactUseCase editContactUseCase;

    public string ContactId { 
        set
        {
            _contact = this.viewContactUseCase.ExecuteAsync(int.Parse(value)).GetAwaiter().GetResult();

            if (_contact != null)
            {
                contactCtrl.Name = _contact.Name;
                contactCtrl.Address = _contact.Address;
                contactCtrl.Email = _contact.Email;
                contactCtrl.Phone = _contact.Phone;
            }
        } 
    }

    public EditContactPage(IViewContactUseCase viewContactUseCase, IEditContactUseCase editContactUseCase)
	{
		InitializeComponent();
        this.viewContactUseCase = viewContactUseCase;
        this.editContactUseCase = editContactUseCase;
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
		//Shell.Current.GoToAsync("..");   // or absolut path by prepending root path with double slash
        Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
    }

    private async void btnUpdate_Clicked(object sender, EventArgs e)
    {
        _contact.Name = contactCtrl.Name;
        _contact.Address = contactCtrl.Address;
        _contact.Email = contactCtrl.Email;
        _contact.Phone = contactCtrl.Phone;

        await this.editContactUseCase.ExecuteAsync(_contact.ContactId, _contact);
        await Shell.Current.GoToAsync("..");
    }

    private void contactCtrl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}