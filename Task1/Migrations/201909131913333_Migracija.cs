namespace Task1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migracija : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StavkaRacunas", "Artikal_ID", "dbo.Proizvods");
            DropForeignKey("dbo.Racuns", "StavkaRacuna_ID", "dbo.StavkaRacunas");
            DropIndex("dbo.Racuns", new[] { "StavkaRacuna_ID" });
            DropIndex("dbo.StavkaRacunas", new[] { "Artikal_ID" });
            AlterColumn("dbo.StavkaRacunas", "Artikal_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.StavkaRacunas", "Artikal_ID");
            CreateIndex("dbo.StavkaRacunas", "Racuni_ID");
            AddForeignKey("dbo.StavkaRacunas", "Artikal_ID", "dbo.Proizvods", "ID", cascadeDelete: true);
            AddForeignKey("dbo.StavkaRacunas", "Racuni_ID", "dbo.Racuns", "ID", cascadeDelete: true);
            DropColumn("dbo.Racuns", "StavkaRacuna_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Racuns", "StavkaRacuna_ID", c => c.Int());
            DropForeignKey("dbo.StavkaRacunas", "Racuni_ID", "dbo.Racuns");
            DropForeignKey("dbo.StavkaRacunas", "Artikal_ID", "dbo.Proizvods");
            DropIndex("dbo.StavkaRacunas", new[] { "Racuni_ID" });
            DropIndex("dbo.StavkaRacunas", new[] { "Artikal_ID" });
            AlterColumn("dbo.StavkaRacunas", "Artikal_ID", c => c.Int());
            RenameColumn(table: "dbo.StavkaRacunas", name: "Racuni_ID", newName: "StavkaRacuna_ID");
            CreateIndex("dbo.StavkaRacunas", "Artikal_ID");
            CreateIndex("dbo.Racuns", "StavkaRacuna_ID");
            AddForeignKey("dbo.Racuns", "StavkaRacuna_ID", "dbo.StavkaRacunas", "ID");
            AddForeignKey("dbo.StavkaRacunas", "Artikal_ID", "dbo.Proizvods", "ID");
        }
    }
}
