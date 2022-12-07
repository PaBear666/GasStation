namespace GasStation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGasStationMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fuels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Cost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Topologies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Construction = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Transports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FuelVolume = c.Int(nullable: false),
                        FuelID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Fuels", t => t.FuelID, cascadeDelete: true)
                .Index(t => t.FuelID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        UserRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transports", "FuelID", "dbo.Fuels");
            DropIndex("dbo.Transports", new[] { "FuelID" });
            DropTable("dbo.Users");
            DropTable("dbo.Transports");
            DropTable("dbo.Topologies");
            DropTable("dbo.Fuels");
        }
    }
}
