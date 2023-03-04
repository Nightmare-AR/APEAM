namespace APEAM.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Email");
        }
    }
}
