namespace ABCUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameInstructorID : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.CourseInstructor", "IntructorID", "InstructorID");

            //DropForeignKey("dbo.CourseInstructor", "Instructors_InstructorID", "dbo.Instructor");
            //DropIndex("dbo.CourseInstructor", new[] { "Instructors_InstructorID" });
            //RenameColumn(table: "dbo.CourseInstructor", name: "Instructors_InstructorID", newName: "InstructorID");
            //AlterColumn("dbo.CourseInstructor", "InstructorID", c => c.Int(nullable: false));
            //CreateIndex("dbo.CourseInstructor", "InstructorID");
            //AddForeignKey("dbo.CourseInstructor", "InstructorID", "dbo.Instructor", "InstructorID", cascadeDelete: true);
            //DropColumn("dbo.CourseInstructor", "IntructorsID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseInstructor", "IntructorsID", c => c.Int(nullable: false));
            DropForeignKey("dbo.CourseInstructor", "InstructorID", "dbo.Instructor");
            DropIndex("dbo.CourseInstructor", new[] { "InstructorID" });
            AlterColumn("dbo.CourseInstructor", "InstructorID", c => c.Int());
            RenameColumn(table: "dbo.CourseInstructor", name: "InstructorID", newName: "Instructors_InstructorID");
            CreateIndex("dbo.CourseInstructor", "Instructors_InstructorID");
            AddForeignKey("dbo.CourseInstructor", "Instructors_InstructorID", "dbo.Instructor", "InstructorID");
        }
    }
}
