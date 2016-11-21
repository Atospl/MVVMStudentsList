namespace MVVMStudentsList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndeksToInt : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Students", "Surname", "LastName");
            AlterColumn("dbo.Students", "IndexNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Surname", c => c.String());
            AlterColumn("dbo.Students", "IndexNo", c => c.String());
            DropColumn("dbo.Students", "LastName");
        }
    }
}
