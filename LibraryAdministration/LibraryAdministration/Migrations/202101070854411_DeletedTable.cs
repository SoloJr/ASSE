﻿namespace LibraryAdministration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookRentals", "RentBookPublisherId", "dbo.BookPublishers");
            DropForeignKey("dbo.ReaderBooks", "BookId", "dbo.Books");
            DropIndex("dbo.BookRentals", new[] { "RentBookPublisherId" });
            DropIndex("dbo.ReaderBooks", new[] { "BookId" });
            AddColumn("dbo.ReaderBooks", "BookPublisherId", c => c.Int(nullable: false));
            CreateIndex("dbo.ReaderBooks", "BookPublisherId");
            AddForeignKey("dbo.ReaderBooks", "BookPublisherId", "dbo.BookPublishers", "Id", cascadeDelete: true);
            DropColumn("dbo.ReaderBooks", "BookId");
            DropTable("dbo.BookRentals");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookRentals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ForRent = c.Int(nullable: false),
                        RentBookPublisherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ReaderBooks", "BookId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ReaderBooks", "BookPublisherId", "dbo.BookPublishers");
            DropIndex("dbo.ReaderBooks", new[] { "BookPublisherId" });
            DropColumn("dbo.ReaderBooks", "BookPublisherId");
            CreateIndex("dbo.ReaderBooks", "BookId");
            CreateIndex("dbo.BookRentals", "RentBookPublisherId");
            AddForeignKey("dbo.ReaderBooks", "BookId", "dbo.Books", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BookRentals", "RentBookPublisherId", "dbo.BookPublishers", "Id", cascadeDelete: true);
        }
    }
}