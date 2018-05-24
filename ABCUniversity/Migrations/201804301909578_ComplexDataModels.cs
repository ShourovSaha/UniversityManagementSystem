namespace ABCUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexDataModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrollment", "CourseID", "dbo.Course");
            DropPrimaryKey("dbo.Course");
            CreateTable(
                "dbo.CourseInstructor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        IntructorsID = c.Int(nullable: false),
                        Instructors_InstructorID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Instructor", t => t.Instructors_InstructorID)
                .Index(t => t.CourseID)
                .Index(t => t.Instructors_InstructorID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(nullable: false),
                        InstructorID = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.Instructor", t => t.InstructorID)
                .Index(t => t.InstructorID);
            
            CreateTable(
                "dbo.Instructor",
                c => new
                    {
                        InstructorID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 10),
                        FirstName = c.String(nullable: false, maxLength: 10),
                        HireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InstructorID);
            
            CreateTable(
                "dbo.OfficeAssignment",
                c => new
                    {
                        InstructorID = c.Int(nullable: false),
                        Location = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.InstructorID)
                .ForeignKey("dbo.Instructor", t => t.InstructorID)
                .Index(t => t.InstructorID);
            
            CreateTable(
                "dbo.InstructorCourse",
                c => new
                    {
                        Instructor_InstructorID = c.Int(nullable: false),
                        Course_CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Instructor_InstructorID, t.Course_CourseID })
                .ForeignKey("dbo.Instructor", t => t.Instructor_InstructorID, cascadeDelete: true)
                .ForeignKey("dbo.Course", t => t.Course_CourseID, cascadeDelete: true)
                .Index(t => t.Instructor_InstructorID)
                .Index(t => t.Course_CourseID);
            
            AddColumn("dbo.Course", "DepartmentID", c => c.Int());
            AddColumn("dbo.Student", "Department_DepartmentID", c => c.Int());
            AlterColumn("dbo.Course", "CourseID", c => c.Int(nullable: false));
            AlterColumn("dbo.Course", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Student", "LastName", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Student", "FirstName", c => c.String(nullable: false, maxLength: 10));
            AddPrimaryKey("dbo.Course", "CourseID");
            CreateIndex("dbo.Course", "DepartmentID");
            CreateIndex("dbo.Student", "Department_DepartmentID");
            AddForeignKey("dbo.Course", "DepartmentID", "dbo.Department", "DepartmentID");
            AddForeignKey("dbo.Student", "Department_DepartmentID", "dbo.Department", "DepartmentID");
            AddForeignKey("dbo.Enrollment", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollment", "CourseID", "dbo.Course");
            DropForeignKey("dbo.CourseInstructor", "Instructors_InstructorID", "dbo.Instructor");
            DropForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Student", "Department_DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Course", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Department", "InstructorID", "dbo.Instructor");
            DropForeignKey("dbo.OfficeAssignment", "InstructorID", "dbo.Instructor");
            DropForeignKey("dbo.InstructorCourse", "Course_CourseID", "dbo.Course");
            DropForeignKey("dbo.InstructorCourse", "Instructor_InstructorID", "dbo.Instructor");
            DropIndex("dbo.InstructorCourse", new[] { "Course_CourseID" });
            DropIndex("dbo.InstructorCourse", new[] { "Instructor_InstructorID" });
            DropIndex("dbo.Student", new[] { "Department_DepartmentID" });
            DropIndex("dbo.OfficeAssignment", new[] { "InstructorID" });
            DropIndex("dbo.Department", new[] { "InstructorID" });
            DropIndex("dbo.Course", new[] { "DepartmentID" });
            DropIndex("dbo.CourseInstructor", new[] { "Instructors_InstructorID" });
            DropIndex("dbo.CourseInstructor", new[] { "CourseID" });
            DropPrimaryKey("dbo.Course");
            AlterColumn("dbo.Student", "FirstName", c => c.String(maxLength: 10));
            AlterColumn("dbo.Student", "LastName", c => c.String(maxLength: 10));
            AlterColumn("dbo.Course", "Title", c => c.String());
            AlterColumn("dbo.Course", "CourseID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Student", "Department_DepartmentID");
            DropColumn("dbo.Course", "DepartmentID");
            DropTable("dbo.InstructorCourse");
            DropTable("dbo.OfficeAssignment");
            DropTable("dbo.Instructor");
            DropTable("dbo.Department");
            DropTable("dbo.CourseInstructor");
            AddPrimaryKey("dbo.Course", "CourseID");
            AddForeignKey("dbo.Enrollment", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
        }
    }
}
