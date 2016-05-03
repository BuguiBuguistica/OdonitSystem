namespace Odonit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableTreatment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Treatment",
                c => new
                    {
                        TreatmentId = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TreatmentId);
            
            AddColumn("dbo.ToothService", "TreatmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.ToothService", "TreatmentId");
            AddForeignKey("dbo.ToothService", "TreatmentId", "dbo.Treatment", "TreatmentId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToothService", "TreatmentId", "dbo.Treatment");
            DropIndex("dbo.ToothService", new[] { "TreatmentId" });
            DropColumn("dbo.ToothService", "TreatmentId");
            DropTable("dbo.Treatment");
        }
    }
}
