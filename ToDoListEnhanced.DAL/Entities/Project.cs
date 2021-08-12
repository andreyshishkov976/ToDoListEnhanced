using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoListEnhanced.DAL.Entities
{
    public class Project
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        private string _projectName;

        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                if (_projectName == value)
                    return;
                _projectName = value;
            }
        }

        [MaxLength(300)]
        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                    return;
                _description = value;
            }
        }

        private bool _status;

        public bool Status
        {
            get { return _status; }
            set
            {
                if (_status == value)
                    return;
                _status = value;
            }
        }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<SubTask> SubTasks { get; set; }
    }
}
