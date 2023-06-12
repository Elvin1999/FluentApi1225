namespace FluentApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactName = c.String(maxLength: 40),
                        CompanyName = c.String(nullable: false, maxLength: 30),
                        City = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ContactName, unique: true);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Customers", new[] { "ContactName" });
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
