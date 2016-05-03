namespace Odonit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingColumnQuadrantFaceTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Face", "Quadrant", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Face", "Quadrant");
        }
    }
}
