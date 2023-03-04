namespace APEAM.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "SaleId", "dbo.Sales");
            DropIndex("dbo.Payments", new[] { "SaleId" });
            DropColumn("dbo.Sales", "IsPayed");
            DropColumn("dbo.Payments", "SaleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "SaleId", c => c.Int(nullable: false));
            AddColumn("dbo.Sales", "IsPayed", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Payments", "SaleId");
            AddForeignKey("dbo.Payments", "SaleId", "dbo.Sales", "ID", cascadeDelete: true);
        }
    }
}
