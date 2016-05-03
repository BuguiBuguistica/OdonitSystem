namespace Odonit.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoEdadPersonaNOComputado : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Person", "Age", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Person", "Age", c => c.Int());
        }
    }
}
