using Microsoft.EntityFrameworkCore;
using ToDoListEnhanced.DAL.Entities;

namespace ToDoListEnhanced.DAL.EF
{
    public class ToDoListContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }

        public ToDoListContext(DbContextOptions<ToDoListContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
