namespace EntityFrameworkSubClasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.As",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bs", t => t.BId, cascadeDelete: true)
                .Index(t => t.BId);
            
            CreateTable(
                "dbo.Bs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Aas",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SubName = c.String(),
                        CId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.As", t => t.Id)
                .ForeignKey("dbo.Cs", t => t.CId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.CId);
            
            CreateTable(
                "dbo.Bbs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SubName = c.String(),
                        DId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bs", t => t.Id)
                .ForeignKey("dbo.Ds", t => t.DId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.DId);
            
            CreateTable(
                "dbo.Ccs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SubName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cs", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Dds",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SubName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ds", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dds", "Id", "dbo.Ds");
            DropForeignKey("dbo.Ccs", "Id", "dbo.Cs");
            DropForeignKey("dbo.Bbs", "DId", "dbo.Ds");
            DropForeignKey("dbo.Bbs", "Id", "dbo.Bs");
            DropForeignKey("dbo.Aas", "CId", "dbo.Cs");
            DropForeignKey("dbo.Aas", "Id", "dbo.As");
            DropForeignKey("dbo.As", "BId", "dbo.Bs");
            DropIndex("dbo.Dds", new[] { "Id" });
            DropIndex("dbo.Ccs", new[] { "Id" });
            DropIndex("dbo.Bbs", new[] { "DId" });
            DropIndex("dbo.Bbs", new[] { "Id" });
            DropIndex("dbo.Aas", new[] { "CId" });
            DropIndex("dbo.Aas", new[] { "Id" });
            DropIndex("dbo.As", new[] { "BId" });
            DropTable("dbo.Dds");
            DropTable("dbo.Ccs");
            DropTable("dbo.Bbs");
            DropTable("dbo.Aas");
            DropTable("dbo.Cs");
            DropTable("dbo.Ds");
            DropTable("dbo.Bs");
            DropTable("dbo.As");
        }
    }
}
