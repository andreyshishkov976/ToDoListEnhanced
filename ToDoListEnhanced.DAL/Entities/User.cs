using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoListEnhanced.DAL.Entities
{
    public class User
    {
        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                    _lastName = value;
            }
        }

        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                    _firstName = value;
            }
        }

        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        private string _surName;

        public string SurName
        {
            get { return _surName; }
            set
            {
                if (_surName != value)
                    _surName = value;
            }
        }

        [Required]
        [MinLength(8)]
        [MaxLength(16)]
        private string _login;

        public string Login
        {
            get { return _login; }
            set
            {
                if (_login != value)
                    _login = value;
            }
        }

        [Required]
        [MinLength(8)]
        [MaxLength(16)]
        private string _password;

        public string PasswordHash
        {
            get { return _password; }
            set
            {
                if (_password != value)
                    _password = value;
            }
        }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
