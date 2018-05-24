namespace ABCUniversity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecorrectedEntityes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrollment", "StudentID", "dbo.Student");
            DropPrimaryKey("dbo.Student");
            DropColumn("dbo.Student", "ID");
            AddColumn("dbo.Student", "StudentID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Student", "StudentID");
            AddForeignKey("dbo.Enrollment", "StudentID", "dbo.Student", "StudentID", cascadeDelete: true);
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Student", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Enrollment", "StudentID", "dbo.Student");
            DropPrimaryKey("dbo.Student");
            DropColumn("dbo.Student", "StudentID");
            AddPrimaryKey("dbo.Student", "ID");
            AddForeignKey("dbo.Enrollment", "StudentID", "dbo.Student", "ID", cascadeDelete: true);
        }
    }
}
