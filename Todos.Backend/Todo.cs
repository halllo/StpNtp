using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TodosBackend
{
    public class Todo
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public bool Erledigt { get; set; }
    }

    public class TodoContext : DbContext
    {
        public TodoContext() : base()
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}