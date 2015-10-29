namespace Pizza.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bestelldatum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BestellungEntities", "Datum", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BestellungEntities", "Datum");
        }
    }
}
