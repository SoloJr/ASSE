﻿namespace LibraryAdministration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookPublishers", "ForLecture", c => c.Int(nullable: false));
            AddColumn("dbo.ReaderBooks", "LoanReturnDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReaderBooks", "LoanReturnDate");
            DropColumn("dbo.BookPublishers", "ForLecture");
        }
    }
}