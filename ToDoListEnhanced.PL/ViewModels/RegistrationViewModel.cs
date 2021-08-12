using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ToDoListEnhanced.BLL.DTO;
using ToDoListEnhanced.BLL.Infrastructure;
using ToDoListEnhanced.BLL.Interfaces;
using ToDoListEnhanced.PL.Util;
using ToDoListEnhanced.PL.ViewModels.Commands;

namespace ToDoListEnhanced.PL.ViewModels
{
    public class RegistrationViewModel : BaseViewModel, IDataErrorInfo
    {
        private bool _isSubmitEnabled;

        public bool IsSubmitEnabled
        {
            get { return _isSubmitEnabled; }
            set
            {
                if (_isSubmitEnabled != value)
                {
                    _isSubmitEnabled = value;
                    OnPropertyChanged(nameof(IsSubmitEnabled));
                }
            }
        }

        private IUserService _userService;

        public RegistrationViewModel(IUserService userService)
        {

            IsSubmitEnabled = false;
            this.PropertyChanged += RegistrationViewModel_PropertyChanged;
            _userService = userService;
            RegisterCommand = new DelegateCommand(RegisterNewUser);
            LogInCommand = new DelegateCommand(LogIn);
        }

        private void LogIn(object obj)
        {
            var injector = new DInjector();
            Application.Current.MainWindow.Content = injector.GetLoginVM();
        }

        private void RegistrationViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IsSubmitEnabled = IsAllPropsValid();
        }

        private async void RegisterNewUser(object obj)
        {
            try
            {
                await _userService.RegisterUser(new UserDTO
                {
                    LastName = LastName,
                    FirstName = FirstName,
                    SurName = SurName,
                    Login = Login,
                    PasswordHash = Password
                });
                LogIn(obj);
            }
            catch (AuthentificationException exception)
            {
                MessageBox.Show($"{exception.Message}\nЛогин: {exception.Login}\nПароль: {exception.Password}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public ICommand RegisterCommand { get; private set; }

        public ICommand LogInCommand { get; private set; }

        #region Validation
        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    IsPersonDataValid(_lastName, nameof(LastName));
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    IsPersonDataValid(_firstName, nameof(FirstName));
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        private string _surName;

        public string SurName
        {
            get { return _surName; }
            set
            {
                if (_surName != value)
                {
                    _surName = value;
                    IsPersonDataValid(_surName, nameof(SurName));
                    OnPropertyChanged(nameof(SurName));
                }
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
                    IsAuthenticalDataValid(_login, nameof(Login));
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
                    IsAuthenticalDataValid(_password, nameof(Password));
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private bool IsPersonDataValid(string value, string propertyName)
        {
            bool isValid = true;
            if (value.Contains(" ") || value.Replace(" ", "").Length <= 0)
            {
                AddError(propertyName, DATA_ERROR, false);
                isValid = false;
            }
            else RemoveError(propertyName, DATA_ERROR);
            if (value.Length > 50 || value.Length < 3)
            {
                AddError(propertyName, PERSON_DATA_WARNING, true);
                isValid = false;
            }
            else RemoveError(propertyName, PERSON_DATA_WARNING);
            validProps[propertyName] = isValid;
            return isValid;
        }

        private bool IsAuthenticalDataValid(string value, string propertyName)
        {
            bool isValid = true;
            if (value.Contains(" ") || value.Replace(" ", "").Length <= 0)
            {
                AddError(propertyName, DATA_ERROR, false);
                isValid = false;
            }
            else RemoveError(propertyName, DATA_ERROR);
            if (value.Length > 16 || value.Length < 8)
            {
                AddError(propertyName, AUTH_DATA_WARNING, true);
                isValid = false;
            }
            else RemoveError(propertyName, AUTH_DATA_WARNING);
            validProps[propertyName] = isValid;
            return isValid;
        }

        private Dictionary<string, bool> validProps = new Dictionary<string, bool>()
        {
            {nameof(LastName),false},
            {nameof(FirstName),false},
            {nameof(SurName),false},
            {nameof(Login),false},
            {nameof(Password),false}
        };
        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
        private const string AUTH_DATA_WARNING = "Длина строки должна быть в диапазоне от 8 до 16 символов.";
        private const string PASSWORD_DO_NOT_MATCH_ERROR = "Пароли не совпадают.";
        private const string DATA_ERROR = "Поле не может быть пустым или содержать пробелы.";
        private const string PERSON_DATA_WARNING = "Длина строки должна быть в диапазоне от 3 до 50 символов.";

        private void AddError(string propertyName, string error, bool isWarning)
        {
            if (!errors.ContainsKey(propertyName))
                errors[propertyName] = new List<string>();

            if (!errors[propertyName].Contains(error))
            {
                if (isWarning) errors[propertyName].Add(error);
                else errors[propertyName].Insert(0, error);
            }
        }

        private void RemoveError(string propertyName, string error)
        {
            if (errors.ContainsKey(propertyName) &&
                errors[propertyName].Contains(error))
            {
                errors[propertyName].Remove(error);
                if (errors[propertyName].Count == 0) errors.Remove(propertyName);
            }
        }

        private bool IsAllPropsValid()
        {
            foreach (var prop in validProps)
                if (!prop.Value)
                    return false;
            return true;
        }

        #region IDataErrorInfo Members

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string propertyName]
        {
            get
            {
                return (!errors.ContainsKey(propertyName) ? null :
                    String.Join(Environment.NewLine, errors[propertyName]));
            }
        }

        #endregion
        #endregion
    }
}
