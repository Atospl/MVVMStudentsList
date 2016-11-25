namespace MVVMStudentsList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timestamp : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Groups", "Stamp");
            DropColumn("dbo.Students", "Stamp");
            AddColumn("dbo.Students", "Stamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Groups", "Stamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Stamp", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Groups", "Stamp", c => c.DateTime(nullable: false));
        }
    }
}
