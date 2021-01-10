namespace LibraryAdministration.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedRelationBetweenBookAndReader : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReaderBooks",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    LoanDate = c.DateTime(nullable: false),
                    BookId = c.Int(nullable: false),
                    ReaderId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Readers", t => t.ReaderId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.ReaderId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.ReaderBooks", "ReaderId", "dbo.Readers");
            DropForeignKey("dbo.ReaderBooks", "BookId", "dbo.Books");
            DropIndex("dbo.ReaderBooks", new[] { "ReaderId" });
            DropIndex("dbo.ReaderBooks", new[] { "BookId" });
            DropTable("dbo.ReaderBooks");
        }
    }
}
