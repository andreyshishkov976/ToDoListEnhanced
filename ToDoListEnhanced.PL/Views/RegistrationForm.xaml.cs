using System.Windows;
using System.Windows.Controls;

namespace ToDoListEnhanced.PL.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : UserControl
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            LastNameBox.Clear();
            FirstNameBox.Clear();
            SurNameBox.Clear();
            LoginBox.Clear();
            PasswordBox.Clear();
        }
    }
}
