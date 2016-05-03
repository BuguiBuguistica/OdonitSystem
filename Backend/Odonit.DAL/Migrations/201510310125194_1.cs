namespace Odonit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
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
                        ServiceId = c.Int()
                    })
                .PrimaryKey(t => t.ToothServiceId)
                .ForeignKey("dbo.Face", t => t.FaceId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .ForeignKey("dbo.Service", t => t.ServiceId)
                .ForeignKey("dbo.Tooth", t => t.ToothId)
                .Index(t => t.FaceId)
                .Index(t => t.PersonId)
                .Index(t => t.ServiceId)
                .Index(t => t.ToothId);
            
            CreateTable(
                "dbo.Face",
                c => new
                    {
                        FaceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.FaceId);
            
            CreateTable(
                "dbo.Service",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ServiceId);
            
            CreateTable(
                "dbo.Tooth",
                c => new
                    {
                        ToothId = c.Int(nullable: false, identity: true),
                        Code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ToothId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToothService", "ToothId", "dbo.Tooth");
            DropForeignKey("dbo.ToothService", "ServiceId", "dbo.Service");
            DropForeignKey("dbo.ToothService", "PersonId", "dbo.Person");
            DropForeignKey("dbo.ToothService", "FaceId", "dbo.Face");
            DropIndex("dbo.ToothService", new[] { "ToothId" });
            DropIndex("dbo.ToothService", new[] { "ServiceId" });
            DropIndex("dbo.ToothService", new[] { "PersonId" });
            DropIndex("dbo.ToothService", new[] { "FaceId" });
            DropTable("dbo.Tooth");
            DropTable("dbo.Service");
            DropTable("dbo.Face");
            DropTable("dbo.ToothService");
        }
    }
}
