namespace Odonit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableMedicalSecurity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedicalSecurity",
                c => new
                    {
                        MedicalSecurityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.MedicalSecurityId);
            
            AddColumn("dbo.Person", "MedicalSecurityId", c => c.Int());
            CreateIndex("dbo.Person", "MedicalSecurityId");
            AddForeignKey("dbo.Person", "MedicalSecurityId", "dbo.MedicalSecurity", "MedicalSecurityId");
            DropColumn("dbo.Person", "MedicalSecurity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "MedicalSecurity", c => c.String());
            DropForeignKey("dbo.Person", "MedicalSecurityId", "dbo.MedicalSecurity");
            DropIndex("dbo.Person", new[] { "MedicalSecurityId" });
            DropColumn("dbo.Person", "MedicalSecurityId");
            DropTable("dbo.MedicalSecurity");
        }
    }
}
