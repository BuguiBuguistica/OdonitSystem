namespace Odonit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class configureColumnsToBeOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToothService", "InitialDate", c => c.DateTime());
            AlterColumn("dbo.ToothService", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToothService", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ToothService", "InitialDate", c => c.DateTime(nullable: false));
        }
    }
}
