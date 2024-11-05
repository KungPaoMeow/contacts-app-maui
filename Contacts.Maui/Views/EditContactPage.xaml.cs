using Contacts.Maui.Models;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.Views;

[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
    private Contact _contact;

    public string ContactId { 
        set
        {
            _contact = ContactRepository.GetContactById(int.Parse(value));
            if (_contact != null)
            {
                contactCtrl.Name = _contact.Name;
                contactCtrl.Address = _contact.Address;
                contactCtrl.Email = _contact.Email;
                contactCtrl.Phone = _contact.Phone;
            }
            //lblName.Text = _contact.Name;
        } 
    }

    public EditContactPage()
	{
		InitializeComponent();
	}

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
		//Shell.Current.GoToAsync("..");   // or absolut path by prepending root path with double slash
        Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
    }

    private void btnUpdate_Clicked(object sender, EventArgs e)
    {
        _contact.Name = contactCtrl.Name;
        _contact.Address = contactCtrl.Address;
        _contact.Email = contactCtrl.Email;
        _contact.Phone = contactCtrl.Phone;
            
        ContactRepository.UpdateContact(_contact.ContactId, _contact);
        Shell.Current.GoToAsync("..");
    }

    private void contactCtrl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}