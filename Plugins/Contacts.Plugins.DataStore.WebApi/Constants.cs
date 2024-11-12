using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Plugins.DataStore.WebApi
{
    public class Constants
    {
        // 10.0.2.2 is to access localhost from emulator
        public const string WebApiBaseUrl = "http://10.0.2.2:5097/api";     // http because it makes it easier to test on localhost on Android? port number is from launchSettings.json
        //public const string WebApiBaseUrl = "https://10.0.2.2:7203/api";
    }
}
