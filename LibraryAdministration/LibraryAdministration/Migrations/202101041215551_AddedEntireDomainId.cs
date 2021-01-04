﻿namespace LibraryAdministration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEntireDomainId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Domains", "EntireDomainId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Domains", "EntireDomainId");
        }
    }
}