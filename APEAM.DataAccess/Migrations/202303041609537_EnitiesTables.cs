namespace APEAM.DataAccess
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnitiesTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        RFC = c.String(),
                        Address = c.String(nullable: false),
                        ZipCode = c.String(),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        Phone = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        Uptime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ItemLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Descripción = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        SaleId = c.Int(nullable: false),
                        IsDisabled = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        Uptime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Sales", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                        IsDisabled = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        Uptime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Folio = c.String(),
                        BrutePrice = c.Double(nullable: false),
                        IVA = c.Single(nullable: false),
                        SaleDate = c.DateTime(nullable: false),
                        IsPayed = c.Boolean(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        IsDisabled = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        Uptime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
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
                        SaleId = c.Int(nullable: false),
                        IsDisabled = c.Boolean(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        Uptime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sales", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.SaleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.ItemLists", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ItemLists", "ProductId", "dbo.Products");
            DropIndex("dbo.Payments", new[] { "SaleId" });
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.ItemLists", new[] { "SaleId" });
            DropIndex("dbo.ItemLists", new[] { "ProductId" });
            DropTable("dbo.Payments");
            DropTable("dbo.Sales");
            DropTable("dbo.Products");
            DropTable("dbo.ItemLists");
            DropTable("dbo.Customers");
        }
    }
}
