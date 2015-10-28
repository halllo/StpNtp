namespace TodosBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredadded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Todoes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Todoes", "Name", c => c.String());
        }
    }
}
