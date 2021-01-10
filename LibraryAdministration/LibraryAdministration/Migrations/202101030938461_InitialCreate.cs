namespace LibraryAdministration.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    BirthDate = c.DateTime(nullable: false),
                    DeathDate = c.DateTime(nullable: false),
                    Country = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Books",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Year = c.Int(nullable: false),
                    Language = c.String(nullable: false, maxLength: 20),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.BookPublishers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Pages = c.Int(nullable: false),
                    Type = c.Int(nullable: false),
                    ReleaseDate = c.DateTime(nullable: false),
                    BookId = c.Int(nullable: false),
                    PublisherId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Publishers", t => t.PublisherId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.PublisherId);

            CreateTable(
                "dbo.Publishers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 30),
                    FoundingDate = c.DateTime(nullable: false),
                    Headquarter = c.String(maxLength: 30),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Domains",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 30),
                    ParentId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.BookAuthors",
                c => new
                {
                    Book_Id = c.Int(nullable: false),
                    Author_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Book_Id, t.Author_Id })
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
                .Index(t => t.Book_Id)
                .Index(t => t.Author_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.BookPublishers", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.BookPublishers", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookAuthors", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.BookAuthors", "Book_Id", "dbo.Books");
            DropIndex("dbo.BookAuthors", new[] { "Author_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Book_Id" });
            DropIndex("dbo.BookPublishers", new[] { "PublisherId" });
            DropIndex("dbo.BookPublishers", new[] { "BookId" });
            DropTable("dbo.BookAuthors");
            DropTable("dbo.Domains");
            DropTable("dbo.Publishers");
            DropTable("dbo.BookPublishers");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
