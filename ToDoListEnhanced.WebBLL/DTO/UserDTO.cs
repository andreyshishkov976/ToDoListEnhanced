using System;

namespace ToDoListEnhanced.WebBLL.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string Login { get; set; }

        public string PasswordHash { get; set; }
    }
}
