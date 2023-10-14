using FluentMigrator;
namespace LLGB.Migration.Seeds
{
    [Migration(2)]
    public class _001_seedStudent : AutoReversingMigration
    {
        public override void Up()
        {
            Insert.IntoTable("student").Row(new { Email = "yasmin.hashem201.com", Name = "Yasmina" });
           
        }

    }


}

