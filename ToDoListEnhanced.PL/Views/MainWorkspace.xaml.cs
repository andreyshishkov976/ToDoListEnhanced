using System.Windows.Controls;

namespace ToDoListEnhanced.PL.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWorkspace.xaml
    /// </summary>
    public partial class MainWorkspace : UserControl
    {
        public MainWorkspace()
        {
            InitializeComponent();
        }

        public MainWorkspace(object viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
