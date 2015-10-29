namespace Pizza.Backend.Migrations
{
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<Pizza.Backend.PizzaContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(Pizza.Backend.PizzaContext context)
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

			context.Pizzen.AddOrUpdate(
				p => p.Name,
				new PizzaEntity { Id = 1, Name = "Hawaii", Preis = 7.5m, Zutaten = "Ananas\nSchinken\nKäse" },
				new PizzaEntity { Id = 2, Name = "Speziale", Preis = 6.5m, Zutaten = "Ruccola\nTomaten\nKäse" }
			);
		}
	}
}
