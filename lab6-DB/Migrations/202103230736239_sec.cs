namespace lab6_DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sec : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CrsId = c.Int(nullable: false),
                        CrsName = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.CrsId);
            
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CrsId = c.Int(nullable: false),
                        StuDegree = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.CrsId })
                .ForeignKey("dbo.Courses", t => t.CrsId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.CrsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentCourses", "Id", "dbo.Students");
            DropForeignKey("dbo.StudentCourses", "CrsId", "dbo.Courses");
            DropIndex("dbo.StudentCourses", new[] { "CrsId" });
            DropIndex("dbo.StudentCourses", new[] { "Id" });
            DropTable("dbo.StudentCourses");
            DropTable("dbo.Courses");
        }
    }
}
