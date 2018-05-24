namespace ABCUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedInstructorClass : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.CourseInstructor", "ID", "CourseInstructorID");

            //DropPrimaryKey("dbo.CourseInstructor");
            //AddColumn("dbo.CourseInstructor", "CourseInstructorID", c => c.Int(nullable: false, identity: true));
            //AddPrimaryKey("dbo.CourseInstructor", "CourseInstructorID");
            //DropColumn("dbo.CourseInstructor", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseInstructor", "ID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.CourseInstructor");
            DropColumn("dbo.CourseInstructor", "CourseInstructorID");
            AddPrimaryKey("dbo.CourseInstructor", "ID");
        }
    }
}
