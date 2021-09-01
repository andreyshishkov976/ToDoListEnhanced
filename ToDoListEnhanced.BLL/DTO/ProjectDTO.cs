using System;

namespace ToDoListEnhanced.ClientBLL.DTO
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }

        public string ProjectName { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public Guid UserId { get; set; }
    }
}
