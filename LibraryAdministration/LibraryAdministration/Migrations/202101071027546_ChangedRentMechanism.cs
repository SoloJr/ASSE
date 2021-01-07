﻿namespace LibraryAdministration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedRentMechanism : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookPublishers", "RentCount", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.BookPublishers", "ForRent", c => c.Int(nullable: false));
            DropColumn("dbo.BookPublishers", "Count");
            DropColumn("dbo.BookPublishers", "AllForRent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookPublishers", "AllForRent", c => c.Boolean(nullable: false));
            AddColumn("dbo.BookPublishers", "Count", c => c.Int(nullable: false));
            DropColumn("dbo.BookPublishers", "ForRent");
            DropColumn("dbo.BookPublishers", "RentCount");
        }
    }
}
