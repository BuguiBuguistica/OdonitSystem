namespace Odonit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toothServiceAdding : DbMigration
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
                        ToothId = c.Int(nullable: false),
                        FaceId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ToothServiceId)
                .ForeignKey("dbo.Face", t => t.FaceId, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.Service", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.Tooth", t => t.ToothId, cascadeDelete: true)
                .Index(t => t.PersonId)
                .Index(t => t.ToothId)
                .Index(t => t.FaceId)
                .Index(t => t.ServiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToothService", "ToothId", "dbo.Tooth");
            DropForeignKey("dbo.ToothService", "ServiceId", "dbo.Service");
            DropForeignKey("dbo.ToothService", "PersonId", "dbo.Person");
            DropForeignKey("dbo.ToothService", "FaceId", "dbo.Face");
            DropIndex("dbo.ToothService", new[] { "ServiceId" });
            DropIndex("dbo.ToothService", new[] { "FaceId" });
            DropIndex("dbo.ToothService", new[] { "ToothId" });
            DropIndex("dbo.ToothService", new[] { "PersonId" });
            DropTable("dbo.ToothService");
        }
    }
}
