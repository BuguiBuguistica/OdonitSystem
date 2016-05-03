namespace Odonit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class configureColumnsToBeOptionalToothService : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToothService", "FaceId", "dbo.Face");
            DropForeignKey("dbo.ToothService", "TreatmentId", "dbo.Treatment");
            DropIndex("dbo.ToothService", new[] { "FaceId" });
            DropIndex("dbo.ToothService", new[] { "TreatmentId" });
            AlterColumn("dbo.ToothService", "Amount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ToothService", "Payment", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ToothService", "Remainder", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ToothService", "FaceId", c => c.Int());
            AlterColumn("dbo.ToothService", "TreatmentId", c => c.Int());
            CreateIndex("dbo.ToothService", "FaceId");
            CreateIndex("dbo.ToothService", "TreatmentId");
            AddForeignKey("dbo.ToothService", "FaceId", "dbo.Face", "FaceId");
            AddForeignKey("dbo.ToothService", "TreatmentId", "dbo.Treatment", "TreatmentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToothService", "TreatmentId", "dbo.Treatment");
            DropForeignKey("dbo.ToothService", "FaceId", "dbo.Face");
            DropIndex("dbo.ToothService", new[] { "TreatmentId" });
            DropIndex("dbo.ToothService", new[] { "FaceId" });
            AlterColumn("dbo.ToothService", "TreatmentId", c => c.Int(nullable: false));
            AlterColumn("dbo.ToothService", "FaceId", c => c.Int(nullable: false));
            AlterColumn("dbo.ToothService", "Remainder", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ToothService", "Payment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ToothService", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.ToothService", "TreatmentId");
            CreateIndex("dbo.ToothService", "FaceId");
            AddForeignKey("dbo.ToothService", "TreatmentId", "dbo.Treatment", "TreatmentId", cascadeDelete: true);
            AddForeignKey("dbo.ToothService", "FaceId", "dbo.Face", "FaceId", cascadeDelete: true);
        }
    }
}
