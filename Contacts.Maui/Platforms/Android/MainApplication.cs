using Android.App;
using Android.Runtime;

namespace Contacts.Maui
{
#if DEBUG
    [Application(UsesCleartextTraffic = true)]  // this allows us to call the local web api from an Android application
#else
    [Application]
#endif
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
