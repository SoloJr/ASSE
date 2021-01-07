namespace LibraryAdministration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDueDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReaderBooks", "DueDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReaderBooks", "DueDate");
        }
    }
}
