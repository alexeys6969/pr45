using Microsoft.EntityFrameworkCore;
using System;
using API_Shashin11.Model;
using Microsoft.EntityFrameworkCore.Storage;

namespace API_Shashin11.Context
{
    public class TasksContext : DbContext
    {
        public DbSet<Tasks> Tasks { get; set; }
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
