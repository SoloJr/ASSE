namespace LibraryAdministration.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ReaderBooks", "LoanReturnDate", c => c.DateTime());
        }

        public override void Down()
        {
            AlterColumn("dbo.ReaderBooks", "LoanReturnDate", c => c.DateTime(nullable: false));
        }
    }
}
