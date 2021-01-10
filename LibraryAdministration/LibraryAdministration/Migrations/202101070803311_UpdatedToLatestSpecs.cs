namespace LibraryAdministration.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatedToLatestSpecs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookRentals", "RentBookId", "dbo.Books");
            DropIndex("dbo.BookRentals", new[] { "RentBookId" });
            AddColumn("dbo.BookPublishers", "ForRent", c => c.Boolean(nullable: false));
            AddColumn("dbo.BookRentals", "RentBookPublisherId", c => c.Int(nullable: false));
            CreateIndex("dbo.BookRentals", "RentBookPublisherId");
            AddForeignKey("dbo.BookRentals", "RentBookPublisherId", "dbo.BookPublishers", "Id", cascadeDelete: true);
            DropColumn("dbo.BookRentals", "RentBookId");
        }

        public override void Down()
        {
            AddColumn("dbo.BookRentals", "RentBookId", c => c.Int(nullable: false));
            DropForeignKey("dbo.BookRentals", "RentBookPublisherId", "dbo.BookPublishers");
            DropIndex("dbo.BookRentals", new[] { "RentBookPublisherId" });
            DropColumn("dbo.BookRentals", "RentBookPublisherId");
            DropColumn("dbo.BookPublishers", "ForRent");
            CreateIndex("dbo.BookRentals", "RentBookId");
            AddForeignKey("dbo.BookRentals", "RentBookId", "dbo.Books", "Id", cascadeDelete: true);
        }
    }
}
