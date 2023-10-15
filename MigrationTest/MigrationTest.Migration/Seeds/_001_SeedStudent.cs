using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
namespace MigrationTest.Migration.Seeds;
[Migration(2)]

public class _001_SeedStudent: AutoReversingMigration
{
    public override void Up()
    {
        Insert.IntoTable("student").Row(new { email = "yasmin.hashem201.com", name = "Yasmina" , grade="senior-2" });

    }
}
