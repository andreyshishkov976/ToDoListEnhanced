using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoListEnhanced.DAL.Entities
{
    public class SubTask
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        private string _subTaskName;

        public string SubTaskName
        {
            get { return _subTaskName; }
            set
            {
                if (_subTaskName == value)
                    return;
                _subTaskName = value;
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

        private bool _ready;

        public bool Status
        {
            get { return _ready; }
            set
            {
                if (_ready == value)
                    return;
                _ready = value;
            }
        }

        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}
