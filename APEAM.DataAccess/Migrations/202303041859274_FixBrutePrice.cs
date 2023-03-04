namespace APEAM.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixBrutePrice : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sales", "BrutePrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "BrutePrice", c => c.Double(nullable: false));
        }
    }
}
