using System;

namespace ToDoListEnhanced.PL.Models
{
    public class Project : DomainObject
    {
        public Project()
        {
            Id = Guid.NewGuid();
            ProjectName = "Task...";
        }

        private Guid _id;

        public Guid Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                    return;
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _projectName;

        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                if (_projectName == value)
                    return;
                _projectName = value;
                OnPropertyChanged(nameof(ProjectName));
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                    return;
                _description = value;
                OnPropertyChanged(nameof(Description));
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
                OnPropertyChanged(nameof(Status));
            }
        }
    }
}
