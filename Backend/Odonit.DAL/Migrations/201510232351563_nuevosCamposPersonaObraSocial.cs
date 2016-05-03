namespace Odonit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nuevosCamposPersonaObraSocial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "MedicalSecurity", c => c.String());
            AddColumn("dbo.Person", "SecurityPlan", c => c.String());
            AddColumn("dbo.Person", "SecurityNumber", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "SecurityNumber");
            DropColumn("dbo.Person", "SecurityPlan");
            DropColumn("dbo.Person", "MedicalSecurity");
        }
    }
}
