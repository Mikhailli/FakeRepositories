namespace FakeRepositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Series", "SeriesDuration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Series", "SeriesDuration", c => c.String());
        }
    }
}
