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
		public List<PizzaEntity> Pizzen()
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

		[OperationContract]
		public void Bestellen(string besteller, List<PizzaEntity> pizzen)
		{
			using (var ctx = new PizzaContext())
			{
				var pizzenById = pizzen.ToLookup(p => p.Id);
				foreach (var pizza in pizzenById)
				{
					var entity = ctx.Entry(pizza.First());
					entity.State = System.Data.Entity.EntityState.Unchanged;
				}
				var bestellung = new BestellungEntity
				{
					Datum = DateTime.Now,
					Besteller = besteller,
					Pizzen = new List<BestellungPositionEntity>(pizzen.Select(p => new BestellungPositionEntity { Pizza = pizzenById[p.Id].First() }))
				};

				ctx.Bestellungen.Add(bestellung);
				ctx.SaveChanges();
			}
		}

		[OperationContract]
		public List<BestellungEntity> Bestellungen(string von)
		{
			using (var ctx = new PizzaContext())
			{
				return ctx.Bestellungen.Include("Pizzen.Pizza").Where(b => b.Besteller == von).ToList();
			}
		}

		[OperationContract]
		public List<BestellungEntity> AlleBestellungen()
		{
			using (var ctx = new PizzaContext())
			{
				return ctx.Bestellungen.Include("Pizzen.Pizza").ToList();
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

	[DataContract]
	public class BestellungEntity
	{
		public int Id { get; set; }
		[DataMember]
		public DateTime Datum { get; set; }
		[DataMember]
		public string Besteller { get; set; }
		[DataMember]
		public List<BestellungPositionEntity> Pizzen { get; set; }
	}

	[DataContract]
	public class BestellungPositionEntity
	{
		public int Id { get; set; }
		public BestellungEntity Bestellung { get; set; }
		[DataMember]
		public PizzaEntity Pizza { get; set; }
	}

	public class PizzaContext : DbContext
	{
		public PizzaContext() : base()
		{
		}

		public DbSet<PizzaEntity> Pizzen { get; set; }
		public DbSet<BestellungEntity> Bestellungen { get; set; }

		//protected override void OnModelCreating(DbModelBuilder modelBuilder)
		//{
		//}
	}
}
