namespace lab6_DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class four : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseDepartments",
                c => new
                    {
                        DeptId = c.Int(nullable: false),
                        CrsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeptId, t.CrsId })
                .ForeignKey("dbo.Courses", t => t.CrsId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DeptId, cascadeDelete: true)
                .Index(t => t.DeptId)
                .Index(t => t.CrsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseDepartments", "DeptId", "dbo.Departments");
            DropForeignKey("dbo.CourseDepartments", "CrsId", "dbo.Courses");
            DropIndex("dbo.CourseDepartments", new[] { "CrsId" });
            DropIndex("dbo.CourseDepartments", new[] { "DeptId" });
            DropTable("dbo.CourseDepartments");
        }
    }
}
