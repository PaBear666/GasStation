namespace GasStation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGasStationMigration5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transports", "FuelID", "dbo.Fuels");
            DropIndex("dbo.Transports", new[] { "FuelID" });
            AlterColumn("dbo.Transports", "FuelId", c => c.Int());
            CreateIndex("dbo.Transports", "FuelId");
            AddForeignKey("dbo.Transports", "FuelId", "dbo.Fuels", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transports", "FuelId", "dbo.Fuels");
            DropIndex("dbo.Transports", new[] { "FuelId" });
            AlterColumn("dbo.Transports", "FuelId", c => c.Int(nullable: false));
            CreateIndex("dbo.Transports", "FuelID");
            AddForeignKey("dbo.Transports", "FuelID", "dbo.Fuels", "ID", cascadeDelete: true);
        }
    }
}
