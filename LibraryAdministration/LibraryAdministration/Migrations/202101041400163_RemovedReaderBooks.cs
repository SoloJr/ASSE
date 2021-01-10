﻿namespace LibraryAdministration.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemovedReaderBooks : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReaderBooks", "Reader_Id", "dbo.Readers");
            DropForeignKey("dbo.ReaderBooks", "Book_Id", "dbo.Books");
            DropIndex("dbo.ReaderBooks", new[] { "Reader_Id" });
            DropIndex("dbo.ReaderBooks", new[] { "Book_Id" });
            DropTable("dbo.ReaderBooks");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.ReaderBooks",
                c => new
                {
                    Reader_Id = c.Int(nullable: false),
                    Book_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Reader_Id, t.Book_Id });

            CreateIndex("dbo.ReaderBooks", "Book_Id");
            CreateIndex("dbo.ReaderBooks", "Reader_Id");
            AddForeignKey("dbo.ReaderBooks", "Book_Id", "dbo.Books", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReaderBooks", "Reader_Id", "dbo.Readers", "Id", cascadeDelete: true);
        }
    }
}
