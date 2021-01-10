namespace LibraryAdministration.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangedTypeOfFlag : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Domains", "ParentId", c => c.Int());
        }

        public override void Down()
        {
            AlterColumn("dbo.Domains", "ParentId", c => c.Int(nullable: false));
        }
    }
}
