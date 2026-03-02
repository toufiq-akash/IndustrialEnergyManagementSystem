namespace IndustrialEnergyManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Machines",
                c => new
                    {
                        MachineId = c.Int(nullable: false, identity: true),
                        MachineName = c.String(),
                        PowerRatingKW = c.Double(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MachineId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Machines", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Machines", new[] { "DepartmentId" });
            DropTable("dbo.Machines");
            DropTable("dbo.Departments");
        }
    }
}
