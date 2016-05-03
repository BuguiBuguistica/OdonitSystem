namespace Odonit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTableStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StatusId);
            
            AddColumn("dbo.ToothService", "StatusId", c => c.Int(nullable: false));
            CreateIndex("dbo.ToothService", "StatusId");
            AddForeignKey("dbo.ToothService", "StatusId", "dbo.Status", "StatusId", cascadeDelete: true);
            DropColumn("dbo.ToothService", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ToothService", "Status", c => c.String());
            DropForeignKey("dbo.ToothService", "StatusId", "dbo.Status");
            DropIndex("dbo.ToothService", new[] { "StatusId" });
            DropColumn("dbo.ToothService", "StatusId");
            DropTable("dbo.Status");
        }
    }
}
