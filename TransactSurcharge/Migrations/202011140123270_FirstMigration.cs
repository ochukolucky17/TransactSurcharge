namespace TransactSurcharge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankTransfers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransRef = c.String(),
                        BeneficiaryName = c.String(),
                        BankName = c.Int(nullable: false),
                        AccountNumber = c.String(),
                        Narration = c.String(),
                        Amount = c.Double(nullable: false),
                        Charge = c.Double(nullable: false),
                        DebitAmount = c.Double(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BankTransfers");
        }
    }
}
