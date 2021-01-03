namespace LibraryAdministration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedDomainFromBook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "BookDomainId", "dbo.Domains");
            DropIndex("dbo.Books", new[] { "BookDomainId" });
            DropColumn("dbo.Books", "BookDomainId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "BookDomainId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "BookDomainId");
            AddForeignKey("dbo.Books", "BookDomainId", "dbo.Domains", "Id", cascadeDelete: true);
        }
    }
}
