namespace LibraryAdministration.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemovedRequiredInPersonalInfo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PersonalInfos", "PhoneNumber", c => c.String(maxLength: 10));
            AlterColumn("dbo.PersonalInfos", "Email", c => c.String(maxLength: 50));
        }

        public override void Down()
        {
            AlterColumn("dbo.PersonalInfos", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.PersonalInfos", "PhoneNumber", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
