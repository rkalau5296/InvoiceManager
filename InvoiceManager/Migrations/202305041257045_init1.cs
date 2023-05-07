namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoicePositions", "Product_Id", "dbo.Products");
            DropIndex("dbo.InvoicePositions", new[] { "Product_Id" });
            RenameColumn(table: "dbo.InvoicePositions", name: "Product_Id", newName: "ProductId");
            AlterColumn("dbo.InvoicePositions", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.InvoicePositions", "ProductId");
            AddForeignKey("dbo.InvoicePositions", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoicePositions", "ProductId", "dbo.Products");
            DropIndex("dbo.InvoicePositions", new[] { "ProductId" });
            AlterColumn("dbo.InvoicePositions", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.InvoicePositions", name: "ProductId", newName: "Product_Id");
            CreateIndex("dbo.InvoicePositions", "Product_Id");
            AddForeignKey("dbo.InvoicePositions", "Product_Id", "dbo.Products", "Id");
        }
    }
}
