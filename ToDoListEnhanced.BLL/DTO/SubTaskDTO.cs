using System;

namespace ToDoListEnhanced.BLL.DTO
{
    public class SubTaskDTO
    {
        public Guid Id { get; set; }

        public string SubTaskName { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public Guid ProjectId { get; set; }
    }
}
