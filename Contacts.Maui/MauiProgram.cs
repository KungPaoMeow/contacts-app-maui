using CommunityToolkit.Maui;
using Contacts.Maui.ViewModels;
using Contacts.Maui.Views;
using Contacts.Maui.Views_MVVM;
using Contacts.Plugins.DataStore.InMemory;
using Contacts.UseCases;
using Contacts.UseCases.Interfaces;
using Contacts.UseCases.PluginInterfaces;
using Microsoft.Extensions.Logging;

namespace Contacts.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Dependency injection mapping
            builder.Services.AddSingleton<IContactRepository, ContactInMemoryRepository>();     // live for life of whole application
            builder.Services.AddSingleton<IViewContactsUseCase, ViewContactsUseCase>();
            builder.Services.AddSingleton<IViewContactUseCase, ViewContactUseCase>();
            builder.Services.AddTransient<IEditContactUseCase, EditContactUseCase>();       // does not need to always be active, can be used on as needed basis
            builder.Services.AddTransient<IAddContactUseCase, AddContactUseCase>();
            builder.Services.AddTransient<IDeleteContactUseCase, DeleteContactUseCase>();

            builder.Services.AddSingleton<ContactsViewModel>();
            builder.Services.AddSingleton<ContactViewModel>();

            // because .NET MAUI only knows how to create instance of a page with default ctor
            builder.Services.AddSingleton<ContactsPage>();
            builder.Services.AddSingleton<EditContactPage>();
            builder.Services.AddSingleton<AddContactPage>();

            builder.Services.AddSingleton<Contacts_MVVM_Page>();
            builder.Services.AddSingleton<EditContactPage_MVVM>();
            builder.Services.AddSingleton<AddContactPage_MVVM>();

            return builder.Build();
        }
    }
}
