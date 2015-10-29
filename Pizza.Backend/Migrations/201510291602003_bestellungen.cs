namespace Pizza.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bestellungen : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BestellungEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Besteller = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BestellungPositionEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bestellung_Id = c.Int(),
                        Pizza_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BestellungEntities", t => t.Bestellung_Id)
                .ForeignKey("dbo.PizzaEntities", t => t.Pizza_Id)
                .Index(t => t.Bestellung_Id)
                .Index(t => t.Pizza_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BestellungPositionEntities", "Pizza_Id", "dbo.PizzaEntities");
            DropForeignKey("dbo.BestellungPositionEntities", "Bestellung_Id", "dbo.BestellungEntities");
            DropIndex("dbo.BestellungPositionEntities", new[] { "Pizza_Id" });
            DropIndex("dbo.BestellungPositionEntities", new[] { "Bestellung_Id" });
            DropTable("dbo.BestellungPositionEntities");
            DropTable("dbo.BestellungEntities");
        }
    }
}
