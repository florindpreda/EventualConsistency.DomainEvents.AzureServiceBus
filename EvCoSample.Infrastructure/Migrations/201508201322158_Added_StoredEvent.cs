namespace EvCoSample.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_StoredEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoredEvents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeName = c.String(),
                        OccurredOn = c.DateTime(nullable: false),
                        SerializedBody = c.String(),
                        IsForwarded = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StoredEvents");
        }
    }
}
