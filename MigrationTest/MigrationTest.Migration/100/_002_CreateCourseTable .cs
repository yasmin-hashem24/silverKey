using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace MigrationTest.Migration._100;
[Migration(3)]
public class _001_CreateCourseTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("course")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("name").AsString(255).NotNullable().WithDefaultValue("Anonymous")
            .WithColumn("instructor").AsString(255).NotNullable().WithDefaultValue("Anonymous");
    }

}
