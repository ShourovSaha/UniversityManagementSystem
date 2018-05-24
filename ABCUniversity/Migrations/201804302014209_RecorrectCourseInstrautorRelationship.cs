namespace ABCUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecorrectCourseInstrautorRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InstructorCourse", "Instructor_InstructorID", "dbo.Instructor");
            DropForeignKey("dbo.InstructorCourse", "Course_CourseID", "dbo.Course");
            DropIndex("dbo.InstructorCourse", new[] { "Instructor_InstructorID" });
            DropIndex("dbo.InstructorCourse", new[] { "Course_CourseID" });
            DropTable("dbo.InstructorCourse");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.InstructorCourse",
                c => new
                    {
                        Instructor_InstructorID = c.Int(nullable: false),
                        Course_CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Instructor_InstructorID, t.Course_CourseID });
            
            CreateIndex("dbo.InstructorCourse", "Course_CourseID");
            CreateIndex("dbo.InstructorCourse", "Instructor_InstructorID");
            AddForeignKey("dbo.InstructorCourse", "Course_CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
            AddForeignKey("dbo.InstructorCourse", "Instructor_InstructorID", "dbo.Instructor", "InstructorID", cascadeDelete: true);
        }
    }
}
