namespace MVVMStudentsList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "GroupID", "dbo.Groups");
            DropIndex("dbo.Students", new[] { "GroupID" });
            AlterColumn("dbo.Students", "GroupID", c => c.Int(nullable: true));
            CreateIndex("dbo.Students", "GroupID");
            AddForeignKey("dbo.Students", "GroupID", "dbo.Groups", "GroupId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "GroupID", "dbo.Groups");
            DropIndex("dbo.Students", new[] { "GroupID" });
            AlterColumn("dbo.Students", "GroupID", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "GroupID");
            AddForeignKey("dbo.Students", "GroupID", "dbo.Groups", "GroupId", cascadeDelete: true);
        }
    }
}
