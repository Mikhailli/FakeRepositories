namespace FakeRepositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsEnd = c.Boolean(nullable: false),
                        Description = c.String(),
                        AgeLimit = c.String(),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Studios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnimeId = c.Int(nullable: false),
                        Name = c.String(),
                        IsMainCharacter = c.Boolean(nullable: false),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnimeId = c.Int(nullable: false),
                        SeriesNumber = c.Int(nullable: false),
                        SeasonNumber = c.Int(nullable: false),
                        SeriesDuration = c.String(),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreAnimes",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Anime_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Anime_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Animes", t => t.Anime_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Anime_Id);
            
            CreateTable(
                "dbo.StudioAnimes",
                c => new
                    {
                        Studio_Id = c.Int(nullable: false),
                        Anime_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Studio_Id, t.Anime_Id })
                .ForeignKey("dbo.Studios", t => t.Studio_Id, cascadeDelete: true)
                .ForeignKey("dbo.Animes", t => t.Anime_Id, cascadeDelete: true)
                .Index(t => t.Studio_Id)
                .Index(t => t.Anime_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudioAnimes", "Anime_Id", "dbo.Animes");
            DropForeignKey("dbo.StudioAnimes", "Studio_Id", "dbo.Studios");
            DropForeignKey("dbo.GenreAnimes", "Anime_Id", "dbo.Animes");
            DropForeignKey("dbo.GenreAnimes", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.StudioAnimes", new[] { "Anime_Id" });
            DropIndex("dbo.StudioAnimes", new[] { "Studio_Id" });
            DropIndex("dbo.GenreAnimes", new[] { "Anime_Id" });
            DropIndex("dbo.GenreAnimes", new[] { "Genre_Id" });
            DropTable("dbo.StudioAnimes");
            DropTable("dbo.GenreAnimes");
            DropTable("dbo.Series");
            DropTable("dbo.Characters");
            DropTable("dbo.Studios");
            DropTable("dbo.Genres");
            DropTable("dbo.Animes");
        }
    }
}
