namespace LibraryAdministration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFurtherTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Readers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 100),
                        ReaderPersonalInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonalInfos", t => t.ReaderPersonalInfoId, cascadeDelete: true)
                .Index(t => t.ReaderPersonalInfoId);
            
            CreateTable(
                "dbo.PersonalInfos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookRentals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ForRent = c.Int(nullable: false),
                        RentBookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.RentBookId, cascadeDelete: true)
                .Index(t => t.RentBookId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 100),
                        EmployeePersonalInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonalInfos", t => t.EmployeePersonalInfoId, cascadeDelete: true)
                .Index(t => t.EmployeePersonalInfoId);
            
            CreateTable(
                "dbo.ReaderBooks",
                c => new
                    {
                        Reader_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Reader_Id, t.Book_Id })
                .ForeignKey("dbo.Readers", t => t.Reader_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Reader_Id)
                .Index(t => t.Book_Id);
            
            AddColumn("dbo.BookPublishers", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "EmployeePersonalInfoId", "dbo.PersonalInfos");
            DropForeignKey("dbo.BookRentals", "RentBookId", "dbo.Books");
            DropForeignKey("dbo.Readers", "ReaderPersonalInfoId", "dbo.PersonalInfos");
            DropForeignKey("dbo.ReaderBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.ReaderBooks", "Reader_Id", "dbo.Readers");
            DropIndex("dbo.ReaderBooks", new[] { "Book_Id" });
            DropIndex("dbo.ReaderBooks", new[] { "Reader_Id" });
            DropIndex("dbo.Employees", new[] { "EmployeePersonalInfoId" });
            DropIndex("dbo.BookRentals", new[] { "RentBookId" });
            DropIndex("dbo.Readers", new[] { "ReaderPersonalInfoId" });
            DropColumn("dbo.BookPublishers", "Count");
            DropTable("dbo.ReaderBooks");
            DropTable("dbo.Employees");
            DropTable("dbo.BookRentals");
            DropTable("dbo.PersonalInfos");
            DropTable("dbo.Readers");
        }
    }
}
