using FluentMigrator;

namespace Customers.Migration._100;

[Migration(1)]
public class _001_AddStudent : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("Students")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("Email").AsString(255).NotNullable().WithDefaultValue("Anonymous")
            .WithColumn("Name").AsString(255).NotNullable().WithDefaultValue("Anonymous");
    }

    
}
