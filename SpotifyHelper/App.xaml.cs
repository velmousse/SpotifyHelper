using System.Configuration;
using System.Data;
using System.Windows;

namespace SpotifyHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string ApiKey = string.Empty;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            CodePrompt codeWindow = new CodePrompt();
            codeWindow.Show();
        }
    }

}
