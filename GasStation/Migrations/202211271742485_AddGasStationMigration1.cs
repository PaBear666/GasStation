namespace GasStation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGasStationMigration1 : DbMigration
    {
        public override void Up()
        {
           
            AddColumn("dbo.Transports", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Fuels", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.Topologies", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Topologies", "Construction", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));

        
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "UserRole" });
            DropIndex("dbo.Transports", new[] { "Name" });
            DropIndex("dbo.Topologies", new[] { "Name" });
            DropIndex("dbo.Fuels", new[] { "Type" });
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Topologies", "Construction", c => c.String());
            AlterColumn("dbo.Topologies", "Name", c => c.String());
            AlterColumn("dbo.Fuels", "Type", c => c.String());
            DropColumn("dbo.Transports", "Name");
        }
    }
}
