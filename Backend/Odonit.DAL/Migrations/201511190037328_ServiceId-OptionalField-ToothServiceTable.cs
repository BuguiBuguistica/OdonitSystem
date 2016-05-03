namespace Odonit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceIdOptionalFieldToothServiceTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToothService", "ServiceId", "dbo.Service");
            DropIndex("dbo.ToothService", new[] { "ServiceId" });
            AlterColumn("dbo.ToothService", "ServiceId", c => c.Int());
            CreateIndex("dbo.ToothService", "ServiceId");
            AddForeignKey("dbo.ToothService", "ServiceId", "dbo.Service", "ServiceId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToothService", "ServiceId", "dbo.Service");
            DropIndex("dbo.ToothService", new[] { "ServiceId" });
            AlterColumn("dbo.ToothService", "ServiceId", c => c.Int(nullable: false));
            CreateIndex("dbo.ToothService", "ServiceId");
            AddForeignKey("dbo.ToothService", "ServiceId", "dbo.Service", "ServiceId", cascadeDelete: true);
        }
    }
}
