using System;

namespace ToDoListEnhanced.ClientBLL.Infrastructure
{
    public class AuthentificationException : Exception
    {
        public string Login { get; protected set; }
        public string Password { get; protected set; }
        public AuthentificationException(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public AuthentificationException(string message, string login, string password) : base(message)
        {
            Login = login;
            Password = password;
        }

        public AuthentificationException(string message, Exception innerException, string login, string password) : base(message, innerException)
        {
            Login = login;
            Password = password;
        }
    }
}
