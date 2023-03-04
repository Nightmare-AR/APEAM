namespace APEAM.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePayment : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Payments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CardNumber = c.String(nullable: false),
                        MonthExp = c.String(nullable: false),
                        YearExp = c.String(nullable: false),
                        CVV = c.String(nullable: false),
                        NameCard = c.String(nullable: false),
                        IsDisabled = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        Uptime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
