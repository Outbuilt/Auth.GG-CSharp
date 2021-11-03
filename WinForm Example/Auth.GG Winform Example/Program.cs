using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auth.GG_Winform_Example
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Update this with your program information found in dashboard
            //APPNAME = Name of your application
            //AIDHERE = AID found in your settings page > Upper right corner > Settings
            //APPSECRET = secret in applications table
            //1.0 indicates your application version located in your application settings
            //YOUTUBE TUTORIAL | https://www.youtube.com/watch?v=VjPz21Va9wU

            OnProgramStart.Initialize("APPNAME", "AIDHERE", "APPSECRET", "1.0");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
