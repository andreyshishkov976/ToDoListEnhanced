using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using ToDoListEnhanced.BLL.Infrastructure;
using ToDoListEnhanced.BLL.Interfaces;
using ToDoListEnhanced.PL.Util;
using ToDoListEnhanced.PL.ViewModels.Commands;

namespace ToDoListEnhanced.PL.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private IUserService _userService;

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
            LogInCommand = new DelegateCommand(LogIn);
            RegisterCommand = new DelegateCommand(Register);
        }

        private void Register(object obj)
        {
            var injector = new DInjector();
            Application.Current.MainWindow.Content = injector.GetRegVM();
        }

        private async void LogIn(object obj)
        {
            try
            {
                Dictionary<string, string> authUserInfo = await _userService.AuthorizeUser(Login, Password);
                var injector = new DInjector();
                var mainVM = injector.GetMainVM();
                mainVM.SetLoggedInUser(authUserInfo);
                Application.Current.MainWindow.Content = mainVM;
            }
            catch (AuthentificationException exception)
            {
                MessageBox.Show($"{exception.Message}\nЛогин: {exception.Login}\nПароль: {exception.Password}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string _login;

        public string Login
        {
            get { return _login; }
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public ICommand LogInCommand { get; private set; }

        public ICommand RegisterCommand { get; private set; }
    }
}
