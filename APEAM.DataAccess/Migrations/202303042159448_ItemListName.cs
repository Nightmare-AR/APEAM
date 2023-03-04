namespace APEAM.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemListName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemLists", "Description", c => c.String(nullable: false));
            DropColumn("dbo.ItemLists", "Descripción");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemLists", "Descripción", c => c.String(nullable: false));
            DropColumn("dbo.ItemLists", "Description");
        }
    }
}
