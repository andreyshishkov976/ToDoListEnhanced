using System;

namespace ToDoListEnhanced.PL.Models
{
    public class SubTask : DomainObject
    {
        public SubTask()
        {
            Id = Guid.NewGuid();
            SubTaskName = "Sub Task...";
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

        private string _subTaskName;

        public string SubTaskName
        {
            get { return _subTaskName; }
            set
            {
                if (_subTaskName == value)
                    return;
                _subTaskName = value;
                OnPropertyChanged(nameof(SubTaskName));
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
