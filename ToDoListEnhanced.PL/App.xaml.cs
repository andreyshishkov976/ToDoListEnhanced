using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ToDoListEnhanced.PL.Util;

namespace ToDoListEnhanced.PL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            var injector = new DInjector();
            MainWindow = new MainWindow();
            MainWindow.DataContext = injector.GetLoginVM();
            MainWindow.Show();
        }
    }
}
