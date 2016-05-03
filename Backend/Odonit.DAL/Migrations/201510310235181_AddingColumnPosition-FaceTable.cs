namespace Odonit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingColumnPositionFaceTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Face", "Position", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Face", "Position");
        }
    }
}
