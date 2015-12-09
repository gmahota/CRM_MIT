using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIT.CRM.Models.Helper
{
    public class AppSettings
    {
        public string SiteTitle { get; set; }
        public ThemeOptions themeOptions { get; set; }

        public string defaultAdminUsername { get; set; }
        public string defaultAdminPassword { get; set; }

        public SignalR_Config signalRConfig { get; set; }

        public SendGrid_Config sendGridConfig { get; set; }

        public Primavera_Config primaveraConfig { get; set; }
        
    }

    public class Primavera_Config {
        public string userName { get; set; }
        public string password { get; set; }
        public short instancia { get; set; }
    }

    public class ThemeOptions
    {
       public string themeName {get; set; }
       public string font { get; set; }
    
    }

    public class SignalR_Config
    {
        public string server_Url { get; set; }
        public string javaScript_Url { get; set; }
    }

    public class SendGrid_Config
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string templateId { get; set; }
    }

    
}
