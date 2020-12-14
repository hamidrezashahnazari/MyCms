namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdmins : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminLogins",
                c => new
                {
                    LoginID = c.Int(nullable: false, identity: true),
                    UserName = c.String(nullable: false, maxLength: 100),
                    Email = c.String(nullable: false, maxLength: 250),
                    Password = c.String(nullable: false, maxLength: 250),
                })
                .PrimaryKey(t => t.LoginID);

            CreateTable(
                "dbo.Admins",
                c => new
                {
                    AdminID = c.Int(nullable: false, identity: true),
                    FullName = c.String(nullable: false),
                    BirthDate = c.DateTime(nullable: false),
                    Email = c.String(nullable: false),
                    Password = c.String(nullable: false),
                    ConfirmPassword = c.String(nullable: false),
                    Bio = c.String(nullable: false, maxLength: 200),
                })
                .PrimaryKey(t => t.AdminID);

            CreateTable(
                "dbo.Categories",
                c => new
                {
                    CategoryID = c.Int(nullable: false, identity: true),
                    CatTittle = c.String(nullable: false, maxLength: 150),
                })
                .PrimaryKey(t => t.CategoryID);

            CreateTable(
                "dbo.News",
                c => new
                {
                    NewsID = c.Int(nullable: false, identity: true),
                    CategoryID = c.Int(nullable: false),
                    Tittle = c.String(nullable: false, maxLength: 250),
                    ShortDescription = c.String(nullable: false, maxLength: 350),
                    Text = c.String(nullable: false),
                    Tags = c.String(),
                    AuthorName = c.String(nullable: false, maxLength: 150),
                    Visit = c.Int(nullable: false),
                    ImageName = c.String(),
                    ShowInSlider = c.Boolean(nullable: false),
                    UploadDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.NewsID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);

            CreateTable(
                "dbo.NewsComments",
                c => new
                {
                    CommentID = c.Int(nullable: false, identity: true),
                    NewsID = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 150),
                    Email = c.String(nullable: false, maxLength: 350),
                    Website = c.String(maxLength: 200),
                    Comment = c.String(nullable: false),
                    SubmitDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.News", t => t.NewsID, cascadeDelete: true)
                .Index(t => t.NewsID);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsComments", "NewsID", "dbo.News");
            DropForeignKey("dbo.News", "CategoryID", "dbo.Categories");
            DropIndex("dbo.NewsComments", new[] { "NewsID" });
            DropIndex("dbo.News", new[] { "CategoryID" });
            DropTable("dbo.NewsComments");
            DropTable("dbo.News");
            DropTable("dbo.Categories");
            DropTable("dbo.Admins");
            DropTable("dbo.AdminLogins");
        }
    }
}
