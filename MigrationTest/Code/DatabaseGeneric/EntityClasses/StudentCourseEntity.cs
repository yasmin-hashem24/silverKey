﻿//////////////////////////////////////////////////////////////
// <auto-generated>This code was generated by LLBLGen Pro 5.10.</auto-generated>
//////////////////////////////////////////////////////////////
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates
// Templates vendor: Solutions Design.
//////////////////////////////////////////////////////////////
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using MigrationTest.HelperClasses;
using MigrationTest.FactoryClasses;
using MigrationTest.RelationClasses;

using SD.LLBLGen.Pro.ORMSupportClasses;

namespace MigrationTest.EntityClasses
{
	// __LLBLGENPRO_USER_CODE_REGION_START AdditionalNamespaces
	// __LLBLGENPRO_USER_CODE_REGION_END
	/// <summary>Entity class which represents the entity 'StudentCourse'.<br/><br/></summary>
	[Serializable]
	public partial class StudentCourseEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private CourseEntity _course;
		private StudentEntity _student;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static StudentCourseEntityStaticMetaData _staticMetaData = new StudentCourseEntityStaticMetaData();
		private static StudentCourseRelations _relationsFactory = new StudentCourseRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name Course</summary>
			public static readonly string Course = "Course";
			/// <summary>Member name Student</summary>
			public static readonly string Student = "Student";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class StudentCourseEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public StudentCourseEntityStaticMetaData()
			{
				SetEntityCoreInfo("StudentCourseEntity", InheritanceHierarchyType.None, false, (int)MigrationTest.EntityType.StudentCourseEntity, typeof(StudentCourseEntity), typeof(StudentCourseEntityFactory), false);
				AddNavigatorMetaData<StudentCourseEntity, CourseEntity>("Course", "StudentCourses", (a, b) => a._course = b, a => a._course, (a, b) => a.Course = b, MigrationTest.RelationClasses.StaticStudentCourseRelations.CourseEntityUsingCourseIdStatic, ()=>new StudentCourseRelations().CourseEntityUsingCourseId, null, new int[] { (int)StudentCourseFieldIndex.CourseId }, null, true, (int)MigrationTest.EntityType.CourseEntity);
				AddNavigatorMetaData<StudentCourseEntity, StudentEntity>("Student", "StudentCourses", (a, b) => a._student = b, a => a._student, (a, b) => a.Student = b, MigrationTest.RelationClasses.StaticStudentCourseRelations.StudentEntityUsingStudentIdStatic, ()=>new StudentCourseRelations().StudentEntityUsingStudentId, null, new int[] { (int)StudentCourseFieldIndex.StudentId }, null, true, (int)MigrationTest.EntityType.StudentEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static StudentCourseEntity()
		{
		}

		/// <summary> CTor</summary>
		public StudentCourseEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public StudentCourseEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this StudentCourseEntity</param>
		public StudentCourseEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for StudentCourse which data should be fetched into this StudentCourse object</param>
		public StudentCourseEntity(System.Int32 id) : this(id, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for StudentCourse which data should be fetched into this StudentCourse object</param>
		/// <param name="validator">The custom validator object for this StudentCourseEntity</param>
		public StudentCourseEntity(System.Int32 id, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected StudentCourseEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Course' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoCourse() { return CreateRelationInfoForNavigator("Course"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Student' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoStudent() { return CreateRelationInfoForNavigator("Student"); }
		
		/// <inheritdoc/>
		protected override EntityStaticMetaDataBase GetEntityStaticMetaData() {	return _staticMetaData; }

		/// <summary>Initializes the class members</summary>
		private void InitClassMembers()
		{
			PerformDependencyInjection();
			// __LLBLGENPRO_USER_CODE_REGION_START InitClassMembers
			// __LLBLGENPRO_USER_CODE_REGION_END
			OnInitClassMembersComplete();
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this StudentCourseEntity</param>
		/// <param name="fields">Fields of this entity</param>
		private void InitClassEmpty(IValidator validator, IEntityFields2 fields)
		{
			OnInitializing();
			this.Fields = fields ?? CreateFields();
			this.Validator = validator;
			InitClassMembers();
			// __LLBLGENPRO_USER_CODE_REGION_START InitClassEmpty
			// __LLBLGENPRO_USER_CODE_REGION_END

			OnInitialized();
		}

		/// <summary>The relations object holding all relations of this entity with other entity classes.</summary>
		public static StudentCourseRelations Relations { get { return _relationsFactory; } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Course' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathCourse { get { return _staticMetaData.GetPrefetchPathElement("Course", CommonEntityBase.CreateEntityCollection<CourseEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Student' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathStudent { get { return _staticMetaData.GetPrefetchPathElement("Student", CommonEntityBase.CreateEntityCollection<StudentEntity>()); } }

		/// <summary>The CourseId property of the Entity StudentCourse<br/><br/></summary>
		/// <remarks>Mapped on  table field: "student_course"."courseId".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 CourseId
		{
			get { return (System.Int32)GetValue((int)StudentCourseFieldIndex.CourseId, true); }
			set	{ SetValue((int)StudentCourseFieldIndex.CourseId, value); }
		}

		/// <summary>The Id property of the Entity StudentCourse<br/><br/></summary>
		/// <remarks>Mapped on  table field: "student_course"."Id".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)StudentCourseFieldIndex.Id, true); }
			set { SetValue((int)StudentCourseFieldIndex.Id, value); }		}

		/// <summary>The RegistrationDate property of the Entity StudentCourse<br/><br/></summary>
		/// <remarks>Mapped on  table field: "student_course"."registrationDate".<br/>Table field type characteristics (type, precision, scale, length): Timestamp, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime RegistrationDate
		{
			get { return (System.DateTime)GetValue((int)StudentCourseFieldIndex.RegistrationDate, true); }
			set	{ SetValue((int)StudentCourseFieldIndex.RegistrationDate, value); }
		}

		/// <summary>The StudentId property of the Entity StudentCourse<br/><br/></summary>
		/// <remarks>Mapped on  table field: "student_course"."studentId".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 StudentId
		{
			get { return (System.Int32)GetValue((int)StudentCourseFieldIndex.StudentId, true); }
			set	{ SetValue((int)StudentCourseFieldIndex.StudentId, value); }
		}

		/// <summary>Gets / sets related entity of type 'CourseEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(false)]
		public virtual CourseEntity Course
		{
			get { return _course; }
			set { SetSingleRelatedEntityNavigator(value, "Course"); }
		}

		/// <summary>Gets / sets related entity of type 'StudentEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(false)]
		public virtual StudentEntity Student
		{
			get { return _student; }
			set { SetSingleRelatedEntityNavigator(value, "Student"); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace MigrationTest
{
	public enum StudentCourseFieldIndex
	{
		///<summary>CourseId. </summary>
		CourseId,
		///<summary>Id. </summary>
		Id,
		///<summary>RegistrationDate. </summary>
		RegistrationDate,
		///<summary>StudentId. </summary>
		StudentId,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace MigrationTest.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: StudentCourse. </summary>
	public partial class StudentCourseRelations: RelationFactory
	{

		/// <summary>Returns a new IEntityRelation object, between StudentCourseEntity and CourseEntity over the m:1 relation they have, using the relation between the fields: StudentCourse.CourseId - Course.Id</summary>
		public virtual IEntityRelation CourseEntityUsingCourseId
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.ManyToOne, "Course", false, new[] { CourseFields.Id, StudentCourseFields.CourseId }); }
		}

		/// <summary>Returns a new IEntityRelation object, between StudentCourseEntity and StudentEntity over the m:1 relation they have, using the relation between the fields: StudentCourse.StudentId - Student.Id</summary>
		public virtual IEntityRelation StudentEntityUsingStudentId
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.ManyToOne, "Student", false, new[] { StudentFields.Id, StudentCourseFields.StudentId }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticStudentCourseRelations
	{
		internal static readonly IEntityRelation CourseEntityUsingCourseIdStatic = new StudentCourseRelations().CourseEntityUsingCourseId;
		internal static readonly IEntityRelation StudentEntityUsingStudentIdStatic = new StudentCourseRelations().StudentEntityUsingStudentId;

		/// <summary>CTor</summary>
		static StaticStudentCourseRelations() { }
	}
}
