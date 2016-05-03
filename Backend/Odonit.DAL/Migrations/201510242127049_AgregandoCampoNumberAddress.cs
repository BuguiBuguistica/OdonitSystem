namespace Odonit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregandoCampoNumberAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Address", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Address", "Number");
        }
    }
}
