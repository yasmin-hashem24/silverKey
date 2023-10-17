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
            .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("email").AsString(255).NotNullable()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("grade").AsString(255).NotNullable();
    }

}
