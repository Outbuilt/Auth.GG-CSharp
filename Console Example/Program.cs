using System;
using System.Diagnostics;
using System.Windows;
using Auth.GG;
namespace Console_Example
{
    class Program
    {
        internal static AuthGG AuthGG;
        static void Main(string[] args)
        {
            //Update this with your program information found in dashboard
            //APPNAME = Name of your application
            //AIDHERE = AID found in your settings page > Upper right corner > Settings
            //APPSECRET = Secret in applications table
            //1.0 = indicates your application version located in your application settings
            //YOUTUBE TUTORIAL | https://www.youtube.com/watch?v=VjPz21Va9wU

            //This connects your file to the Auth.GG API, and sends back your application settings and such
            AuthGG = new AuthGG("APPNAME", "AIDHERE", "APPSECRET", "1.0");
            if (ApplicationSettings.FreeMode)
            {
                //Usually when your application doesn't need a login and has freemode enabled you put the code here you want to do
                MessageBox.Show("Freemode is active, bypassing login!", AuthGG.Name, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (!ApplicationSettings.Status)
            {
                //If application is disabled in your web-panel settings this action will occur
                MessageBox.Show("Application is disabled!", AuthGG.Name, MessageBoxButton.OK, MessageBoxImage.Error);
                Process.GetCurrentProcess().Kill(); // closes the application
            }
            PrintLogo();
            Console.WriteLine("[1] Register");
            Console.WriteLine("[2] Login");
            Console.WriteLine("[3] All in one");
            Console.WriteLine("[4] Extend Subscription");
            string option = Console.ReadLine();
            if (option == "1")
            {
                if (!ApplicationSettings.Register)
                {
                    //Register is disabled in application settings, will show a messagebox that it is not enabled
                    MessageBox.Show("Register is not enabled, please try again later!", AuthGG.Name, MessageBoxButton.OK, MessageBoxImage.Error);
                    Process.GetCurrentProcess().Kill(); //closes the application
                }
                else
                {
                    Console.Clear();
                    PrintLogo();
                    Console.WriteLine();
                    Console.WriteLine("Username:");
                    string username = Console.ReadLine();
                    Console.WriteLine("Password:");
                    string password = Console.ReadLine();
                    Console.WriteLine("Email:");
                    string email = Console.ReadLine();
                    Console.WriteLine("License:");
                    string license = Console.ReadLine();
                    if (UserSystem.Register(username, password, email, license))
                    {
                        MessageBox.Show("You have successfully registered!", AuthGG.Name, MessageBoxButton.OK, MessageBoxImage.Information);
                        // Do code of what you want after successful register here!
                    }
                }
            }
            else if (option == "2")
            {
                if (!ApplicationSettings.Login)
                {
                    //Register is disabled in application settings, will show a messagebox that it is not enabled
                    MessageBox.Show("Login is not enabled, please try again later!", AuthGG.Name, MessageBoxButton.OK, MessageBoxImage.Error);
                    Process.GetCurrentProcess().Kill(); //closes the application
                }
                else
                {
                    Console.Clear();
                    PrintLogo();
                    Console.WriteLine();
                    Console.WriteLine("Username:");
                    string username = Console.ReadLine();
                    Console.WriteLine("Password:");
                    string password = Console.ReadLine();
                    if (UserSystem.Login(username, password))
                    {
                        MessageBox.Show("You have successfully logged in!", AuthGG.Name, MessageBoxButton.OK, MessageBoxImage.Information);
                        Console.Clear();
                        PrintLogo();
                        // Success login stuff goes here
                        Console.ForegroundColor = ConsoleColor.White;
                        App.Log(username, "Logged in!"); //Logs this action to your web-panel, you can do this anywhere and for anything!
                        Console.WriteLine("***************************************************");
                        Console.WriteLine("All user information:");
                        Console.WriteLine("***************************************************");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"User ID -> {User.ID}");
                        Console.WriteLine($"Username -> {User.Username}");
                        Console.WriteLine($"Password -> {User.Password}");
                        Console.WriteLine($"Email -> {User.Email}");
                        Console.WriteLine($"HWID -> {User.HWID}");
                        Console.WriteLine($"User Variable -> {User.UserVariable}");
                        Console.WriteLine($"User Rank -> {User.Rank}");
                        Console.WriteLine($"User IP -> {User.IP}");
                        Console.WriteLine($"Expiry -> {User.Expiry}");
                        Console.WriteLine($"Last Login -> {User.LastLogin}");
                        Console.WriteLine($"Register Date -> {User.RegisterDate}");
                        Console.WriteLine($"Variable -> {App.GrabVariable("PutVariableSecretHere")}"); // Replace put variable secret here with the secret of the variable in your panel - https://i.imgur.com/v3q2a6e.png
                    }
                }
            }
            else if (option == "4")
            {
                Console.Clear();
                PrintLogo();
                Console.WriteLine();
                Console.WriteLine("Username:");
                string username = Console.ReadLine();
                Console.WriteLine("Password:");
                string password = Console.ReadLine();
                Console.WriteLine("Token:");
                string token = Console.ReadLine();
                if (UserSystem.ExtendSubscription(username, password, token))
                {
                    MessageBox.Show("You have successfully extended your subscription!", AuthGG.Name, MessageBoxButton.OK, MessageBoxImage.Information);
                    // Do code of what you want after successful extend here!
                }
            }
            else if (option == "3")
            {
                Console.Clear();
                PrintLogo();
                Console.WriteLine();
                Console.WriteLine("AIO Key:");
                string KEY = Console.ReadLine();
                if (UserSystem.AIO(KEY))
                {
                    //Code you want to do here on successful login
                    MessageBox.Show("Welcome back to my application!", AuthGG.Name, MessageBoxButton.OK, MessageBoxImage.Information);
                    Process.GetCurrentProcess().Kill(); // closes the application
                }
                else
                {
                    //Code you want to do here on failed login
                    MessageBox.Show("Your key does not exist!", AuthGG.Name, MessageBoxButton.OK, MessageBoxImage.Error);
                    Process.GetCurrentProcess().Kill(); // closes the application
                }
            }
            Console.Read();
        }
        public static void PrintLogo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("                                █████╗ ██╗   ██╗████████╗██╗  ██╗    ██████╗  ██████╗ ");
            Console.WriteLine("                               ██╔══██╗██║   ██║╚══██╔══╝██║  ██║   ██╔════╝ ██╔════╝ ");
            Console.WriteLine("                               ███████║██║   ██║   ██║   ███████║   ██║  ███╗██║  ███╗");
            Console.WriteLine("                               ██╔══██║██║   ██║   ██║   ██╔══██║   ██║   ██║██║   ██║");
            Console.WriteLine("                               ██║  ██║╚██████╔╝   ██║   ██║  ██║██╗╚██████╔╝╚██████╔╝");
            Console.WriteLine("                               ╚═╝  ╚═╝ ╚═════╝    ╚═╝   ╚═╝  ╚═╝╚═╝ ╚═════╝  ╚═════╝ ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
