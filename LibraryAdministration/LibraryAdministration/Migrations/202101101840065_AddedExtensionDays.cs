﻿namespace LibraryAdministration.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedExtensionDays : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReaderBooks", "ExtensionDays", c => c.Int());
        }

        public override void Down()
        {
            DropColumn("dbo.ReaderBooks", "ExtensionDays");
        }
    }
}
