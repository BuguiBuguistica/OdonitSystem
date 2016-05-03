namespace Odonit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class laCONCHA : DbMigration
    {
        public override void Down()
        {
            DropForeignKey("dbo.ToothService", "Face_FaceId", "dbo.Face");
            DropForeignKey("dbo.ToothService", "Person_PersonId", "dbo.Person");
            DropForeignKey("dbo.ToothService", "Service_ServiceId", "dbo.Service");
            DropForeignKey("dbo.ToothService", "Tooth_ToothId", "dbo.Tooth");
            DropIndex("dbo.ToothService", new[] { "Face_FaceId" });
            DropIndex("dbo.ToothService", new[] { "Person_PersonId" });
            DropIndex("dbo.ToothService", new[] { "Service_ServiceId" });
            DropIndex("dbo.ToothService", new[] { "Tooth_ToothId" });
            DropTable("dbo.Face");
            DropTable("dbo.Service");
            DropTable("dbo.Tooth");
            DropTable("dbo.ToothService");
        }
        
        public override void Up()
        {
            CreateTable(
                "dbo.ToothService",
                c => new
                    {
                        ToothServiceId = c.Int(nullable: false, identity: true),
                        Diagnosis = c.String(),
                        Status = c.String(),
                        InitialDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Payment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remainder = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PersonId = c.Int(nullable: false),
                        ToothId = c.Int(),
                        FaceId = c.Int(),
                        ServiceId = c.Int(),
                        Face_FaceId = c.Int(),
                        Person_PersonId = c.Int(),
                        Service_ServiceId = c.Int(),
                        Tooth_ToothId = c.Int(),
                    })
                .PrimaryKey(t => t.ToothServiceId);
            
            CreateTable(
                "dbo.Tooth",
                c => new
                    {
                        ToothId = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ToothId);
            
            CreateTable(
                "dbo.Service",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ServiceId);
            
            CreateTable(
                "dbo.Face",
                c => new
                    {
                        FaceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.FaceId);
            
            CreateIndex("dbo.ToothService", "Tooth_ToothId");
            CreateIndex("dbo.ToothService", "Service_ServiceId");
            CreateIndex("dbo.ToothService", "Person_PersonId");
            CreateIndex("dbo.ToothService", "Face_FaceId");
            AddForeignKey("dbo.ToothService", "Tooth_ToothId", "dbo.Tooth", "ToothId");
            AddForeignKey("dbo.ToothService", "Service_ServiceId", "dbo.Service", "ServiceId");
            AddForeignKey("dbo.ToothService", "Person_PersonId", "dbo.Person", "PersonId");
            AddForeignKey("dbo.ToothService", "Face_FaceId", "dbo.Face", "FaceId");
        }
    }
}
