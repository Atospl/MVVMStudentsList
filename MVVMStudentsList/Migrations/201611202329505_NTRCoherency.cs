namespace MVVMStudentsList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NTRCoherency : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "GroupID", "dbo.Groups");
            RenameColumn(table: "dbo.Students", name: "GroupID", newName: "IDGroup");
            RenameIndex(table: "dbo.Students", name: "IX_GroupID", newName: "IX_IDGroup");
            RenameColumn(table: "dbo.Groups", name: "GroupID", newName: "IDGroup");
            RenameColumn(table: "dbo.Students", name: "StudentID", newName: "IDStudent");
            //DropPrimaryKey("dbo.Groups");
            //DropPrimaryKey("dbo.Students");
            //AddColumn("dbo.Groups", "IDGroup", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Groups", "Stamp", c => c.DateTime(nullable: false));
            //AddColumn("dbo.Students", "IDStudent", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Students", "Stamp", c => c.DateTime(nullable: false));
            //AddPrimaryKey("dbo.Groups", "IDGroup");
            //AddPrimaryKey("dbo.Students", "IDStudent");
            //AddForeignKey("dbo.Students", "IDGroup", "dbo.Groups", "IDGroup");
            //DropColumn("dbo.Groups", "GroupId");
            //DropColumn("dbo.Students", "StudentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "StudentID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Groups", "GroupId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Students", "IDGroup", "dbo.Groups");
            DropPrimaryKey("dbo.Students");
            DropPrimaryKey("dbo.Groups");
            DropColumn("dbo.Students", "Stamp");
            DropColumn("dbo.Students", "IDStudent");
            DropColumn("dbo.Groups", "Stamp");
            DropColumn("dbo.Groups", "IDGroup");
            AddPrimaryKey("dbo.Students", "StudentID");
            AddPrimaryKey("dbo.Groups", "GroupId");
            RenameIndex(table: "dbo.Students", name: "IX_IDGroup", newName: "IX_GroupID");
            RenameColumn(table: "dbo.Students", name: "IDGroup", newName: "GroupID");
            AddForeignKey("dbo.Students", "GroupID", "dbo.Groups", "GroupId");
        }
    }
}
