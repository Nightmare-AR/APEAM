namespace APEAM.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerPaymentMethod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "PaymentMethod", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "PaymentMethod");
        }
    }
}
