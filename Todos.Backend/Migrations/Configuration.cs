namespace TodosBackend.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TodosBackend.TodoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TodosBackend.TodoContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Todos.AddOrUpdate(p => p.Id,
                new Todo { Name = "Erste Aufgabe", Erledigt = true },
                new Todo { Name = "Zweite Aufgabe", Erledigt = false },
                new Todo { Name = "Dritte Aufgabe", Erledigt = false }
            );
        }
    }
}
