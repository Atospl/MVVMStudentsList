namespace MVVMStudentsList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ctors1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "IDGroup", "dbo.Groups");
            DropIndex("dbo.Students", new[] { "IDGroup" });
            AlterColumn("dbo.Groups", "Name", c => c.String(maxLength: 16));
            AlterColumn("dbo.Students", "FirstName", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.Students", "LastName", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.Students", "IndexNo", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Students", "BirthPlace", c => c.String(maxLength: 64));
            AlterColumn("dbo.Students", "IDGroup", c => c.Int(nullable: false));
            CreateIndex("dbo.Students", "IndexNo", unique: true);
            CreateIndex("dbo.Students", "IDGroup");
            AddForeignKey("dbo.Students", "IDGroup", "dbo.Groups", "IDGroup", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "IDGroup", "dbo.Groups");
            DropIndex("dbo.Students", new[] { "IDGroup" });
            DropIndex("dbo.Students", new[] { "IndexNo" });
            AlterColumn("dbo.Students", "IDGroup", c => c.Int());
            AlterColumn("dbo.Students", "BirthPlace", c => c.String());
            AlterColumn("dbo.Students", "IndexNo", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "LastName", c => c.String());
            AlterColumn("dbo.Students", "FirstName", c => c.String());
            AlterColumn("dbo.Groups", "Name", c => c.String());
            CreateIndex("dbo.Students", "IDGroup");
            AddForeignKey("dbo.Students", "IDGroup", "dbo.Groups", "IDGroup");
        }
    }
}
