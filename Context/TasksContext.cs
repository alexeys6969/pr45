using Microsoft.EntityFrameworkCore;
using API_Shashin1.Model;
using System;

namespace API_Shashin1.Context
{
    public class TasksContext : DbContext
    {
        public DbSet<Task> Tasks {  get; set; }
        public TasksContext()
        {
            Database.EnsureCreated();
            Tasks.Load();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=127.0.0.1;uid=root;pwd=;database=TaskManager", new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
