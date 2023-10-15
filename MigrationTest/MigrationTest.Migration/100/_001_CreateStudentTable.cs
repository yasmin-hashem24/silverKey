using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace MigrationTest.Migration._100;
[Migration(1)]
public class _001_CreateStudentTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("student")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("email").AsString(255).NotNullable().WithDefaultValue("Anonymous")
            .WithColumn("name").AsString(255).NotNullable().WithDefaultValue("Anonymous")
            .WithColumn("grade").AsString(255).NotNullable().WithDefaultValue("Anonymous");
    }

}
