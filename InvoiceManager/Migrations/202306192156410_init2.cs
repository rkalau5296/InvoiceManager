namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MethodOfPayments", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.MethodOfPayments", "UserId");
            AddForeignKey("dbo.MethodOfPayments", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MethodOfPayments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.MethodOfPayments", new[] { "UserId" });
            DropColumn("dbo.MethodOfPayments", "UserId");
        }
    }
}
