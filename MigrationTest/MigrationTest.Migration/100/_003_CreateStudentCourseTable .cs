using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace MigrationTest.Migration._100;
[Migration(4)]
public class _001_CreateStudentCourseTable : AutoReversingMigration
{
    public override void Up()
    {
        Create.Table("student_course")
    .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
    .WithColumn("student_id").AsInt32().NotNullable().ForeignKey("student", "id")
    .WithColumn("course_id").AsInt32().NotNullable().ForeignKey("course", "id")
    .WithColumn("registration_date").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);
   
    }
     
}
