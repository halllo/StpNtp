using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Pizza.Backend
{
	[ServiceContract]
	public class PizzaService
	{
		[OperationContract]
		public List<PizzaEntity> Alle()
		{
			using (var ctx = new PizzaContext())
			{
				return ctx.Pizzen.ToList();
			}
		}

		[OperationContract]
		public void Speichern(PizzaEntity pizza)
		{
			using (var ctx = new PizzaContext())
			{
				var entity = ctx.Entry(pizza);
				entity.State = pizza.Id == 0 ? System.Data.Entity.EntityState.Added : System.Data.Entity.EntityState.Modified;
				
				ctx.SaveChanges();
			}
		}
	}

	public class PizzaEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Preis { get; set; }
		public string Zutaten { get; set; }
	}

	public class PizzaContext : DbContext
	{
		public PizzaContext() : base()
		{
		}

		public DbSet<PizzaEntity> Pizzen { get; set; }
	}
}
