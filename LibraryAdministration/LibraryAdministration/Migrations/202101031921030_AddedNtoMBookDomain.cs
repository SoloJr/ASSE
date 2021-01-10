namespace LibraryAdministration.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedNtoMBookDomain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DomainBooks",
                c => new
                {
                    Domain_Id = c.Int(nullable: false),
                    Book_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Domain_Id, t.Book_Id })
                .ForeignKey("dbo.Domains", t => t.Domain_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Domain_Id)
                .Index(t => t.Book_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.DomainBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.DomainBooks", "Domain_Id", "dbo.Domains");
            DropIndex("dbo.DomainBooks", new[] { "Book_Id" });
            DropIndex("dbo.DomainBooks", new[] { "Domain_Id" });
            DropTable("dbo.DomainBooks");
        }
    }
}
