using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace MusicApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            StartupUri = new Uri("/MusicApp;component/MVVM/View/MusicWindow.xaml",UriKind.Relative);
        }
    }
}
