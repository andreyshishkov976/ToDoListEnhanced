using System.Windows;
using System.Windows.Controls;

namespace ToDoListEnhanced.PL.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginForm.xaml
    /// </summary>
    public partial class LoginForm : UserControl
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            LoginBox.Clear();
            //PasswordBox.Password = string.Empty;
            PasswordBox.Text = string.Empty;
        }
    }
}
