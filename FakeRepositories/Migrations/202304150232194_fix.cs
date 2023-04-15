namespace FakeRepositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Characters", "AnimeId");
            CreateIndex("dbo.Series", "AnimeId");
            AddForeignKey("dbo.Characters", "AnimeId", "dbo.Animes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Series", "AnimeId", "dbo.Animes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Series", "AnimeId", "dbo.Animes");
            DropForeignKey("dbo.Characters", "AnimeId", "dbo.Animes");
            DropIndex("dbo.Series", new[] { "AnimeId" });
            DropIndex("dbo.Characters", new[] { "AnimeId" });
        }
    }
}
