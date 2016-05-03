namespace Odonit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(),
                        BirthDate = c.DateTime(),
                        Gender = c.Boolean(),
                        Code = c.Int(nullable: false),
                        AddressId = c.Int(),
                        ContactId = c.Int(),
                        MedicalHistoryId = c.Int(),
                        PatientId = c.Int(),
                        CreatedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                        MembershipId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Address", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.Contact", t => t.ContactId, cascadeDelete: true)
                .ForeignKey("dbo.MedicalHistory", t => t.MedicalHistoryId)
                .Index(t => t.AddressId)
                .Index(t => t.ContactId)
                .Index(t => t.MedicalHistoryId);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        LandPhone = c.String(),
                        CellPhone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.MedicalHistory",
                c => new
                    {
                        MedicalHistoryId = c.Int(nullable: false, identity: true),
                        CurrentMedication = c.String(),
                        BloodGroup = c.String(),
                        Alergic = c.Boolean(),
                        Hemorrhage = c.Boolean(),
                        Diabetes = c.Boolean(),
                        HeartDisease = c.Boolean(),
                        RespiratoryDisease = c.Boolean(),
                        KidneyDisease = c.Boolean(),
                        Hepatitis = c.Boolean(),
                        Hypertension = c.Boolean(),
                        STD = c.Boolean(),
                        PregnancyDate = c.DateTime(),
                        TransfusionDate = c.DateTime(),
                        Observations = c.String(),
                        Habits = c.String(),
                        Others = c.String(),
                        ExamId = c.Int(),
                    })
                .PrimaryKey(t => t.MedicalHistoryId)
                .ForeignKey("dbo.Exam", t => t.ExamId)
                .Index(t => t.ExamId);
            
            CreateTable(
                "dbo.Exam",
                c => new
                    {
                        ExamId = c.Int(nullable: false, identity: true),
                        Extraoral = c.String(),
                        Intraoral = c.String(),
                        Others = c.String(),
                        Observations = c.String(),
                    })
                .PrimaryKey(t => t.ExamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Person", "MedicalHistoryId", "dbo.MedicalHistory");
            DropForeignKey("dbo.MedicalHistory", "ExamId", "dbo.Exam");
            DropForeignKey("dbo.Person", "ContactId", "dbo.Contact");
            DropForeignKey("dbo.Person", "AddressId", "dbo.Address");
            DropIndex("dbo.MedicalHistory", new[] { "ExamId" });
            DropIndex("dbo.Person", new[] { "MedicalHistoryId" });
            DropIndex("dbo.Person", new[] { "ContactId" });
            DropIndex("dbo.Person", new[] { "AddressId" });
            DropTable("dbo.Exam");
            DropTable("dbo.MedicalHistory");
            DropTable("dbo.Contact");
            DropTable("dbo.Person");
            DropTable("dbo.Address");
        }
    }
}
