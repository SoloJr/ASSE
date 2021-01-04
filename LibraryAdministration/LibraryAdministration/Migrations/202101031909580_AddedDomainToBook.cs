﻿namespace LibraryAdministration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDomainToBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "BookDomainId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "BookDomainId");
            AddForeignKey("dbo.Books", "BookDomainId", "dbo.Domains", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "BookDomainId", "dbo.Domains");
            DropIndex("dbo.Books", new[] { "BookDomainId" });
            DropColumn("dbo.Books", "BookDomainId");
        }
    }
}