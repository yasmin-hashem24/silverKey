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
    .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
    .WithColumn("studentId").AsInt32().NotNullable().ForeignKey("student", "Id")
    .WithColumn("courseId").AsInt32().NotNullable().ForeignKey("course", "Id")
    .WithColumn("registrationDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime);
   
    }
     
}
