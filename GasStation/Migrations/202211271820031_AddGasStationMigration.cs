namespace GasStation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGasStationMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "UserRole" });
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Name" });
            CreateIndex("dbo.Users", "UserRole", unique: true);
        }
    }
}
