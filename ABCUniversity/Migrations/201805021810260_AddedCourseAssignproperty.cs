namespace ABCUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCourseAssignproperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseInstructor", "CourseAssignDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseInstructor", "CourseAssignDate");
        }
    }
}
