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
	/// <summary>Entity class which represents the entity 'Student'.<br/><br/></summary>
	[Serializable]
	public partial class StudentEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private EntityCollection<StudentCourseEntity> _studentCourses;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static StudentEntityStaticMetaData _staticMetaData = new StudentEntityStaticMetaData();
		private static StudentRelations _relationsFactory = new StudentRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name StudentCourses</summary>
			public static readonly string StudentCourses = "StudentCourses";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class StudentEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public StudentEntityStaticMetaData()
			{
				SetEntityCoreInfo("StudentEntity", InheritanceHierarchyType.None, false, (int)MigrationTest.EntityType.StudentEntity, typeof(StudentEntity), typeof(StudentEntityFactory), false);
				AddNavigatorMetaData<StudentEntity, EntityCollection<StudentCourseEntity>>("StudentCourses", a => a._studentCourses, (a, b) => a._studentCourses = b, a => a.StudentCourses, () => new StudentRelations().StudentCourseEntityUsingStudentId, typeof(StudentCourseEntity), (int)MigrationTest.EntityType.StudentCourseEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static StudentEntity()
		{
		}

		/// <summary> CTor</summary>
		public StudentEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public StudentEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this StudentEntity</param>
		public StudentEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Student which data should be fetched into this Student object</param>
		public StudentEntity(System.Int32 id) : this(id, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="id">PK value for Student which data should be fetched into this Student object</param>
		/// <param name="validator">The custom validator object for this StudentEntity</param>
		public StudentEntity(System.Int32 id, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.Id = id;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected StudentEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'StudentCourse' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoStudentCourses() { return CreateRelationInfoForNavigator("StudentCourses"); }
		
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
		/// <param name="validator">The validator object for this StudentEntity</param>
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
		public static StudentRelations Relations { get { return _relationsFactory; } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'StudentCourse' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathStudentCourses { get { return _staticMetaData.GetPrefetchPathElement("StudentCourses", CommonEntityBase.CreateEntityCollection<StudentCourseEntity>()); } }

		/// <summary>The Email property of the Entity Student<br/><br/></summary>
		/// <remarks>Mapped on  table field: "student"."email".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 255.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Email
		{
			get { return (System.String)GetValue((int)StudentFieldIndex.Email, true); }
			set	{ SetValue((int)StudentFieldIndex.Email, value); }
		}

		/// <summary>The Grade property of the Entity Student<br/><br/></summary>
		/// <remarks>Mapped on  table field: "student"."grade".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 255.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Grade
		{
			get { return (System.String)GetValue((int)StudentFieldIndex.Grade, true); }
			set	{ SetValue((int)StudentFieldIndex.Grade, value); }
		}

		/// <summary>The Id property of the Entity Student<br/><br/></summary>
		/// <remarks>Mapped on  table field: "student"."Id".<br/>Table field type characteristics (type, precision, scale, length): Integer, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 Id
		{
			get { return (System.Int32)GetValue((int)StudentFieldIndex.Id, true); }
			set { SetValue((int)StudentFieldIndex.Id, value); }		}

		/// <summary>The Name property of the Entity Student<br/><br/></summary>
		/// <remarks>Mapped on  table field: "student"."name".<br/>Table field type characteristics (type, precision, scale, length): Varchar, 0, 0, 255.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)StudentFieldIndex.Name, true); }
			set	{ SetValue((int)StudentFieldIndex.Name, value); }
		}

		/// <summary>Gets the EntityCollection with the related entities of type 'StudentCourseEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(StudentCourseEntity))]
		public virtual EntityCollection<StudentCourseEntity> StudentCourses { get { return GetOrCreateEntityCollection<StudentCourseEntity, StudentCourseEntityFactory>("Student", true, false, ref _studentCourses); } }

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace MigrationTest
{
	public enum StudentFieldIndex
	{
		///<summary>Email. </summary>
		Email,
		///<summary>Grade. </summary>
		Grade,
		///<summary>Id. </summary>
		Id,
		///<summary>Name. </summary>
		Name,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace MigrationTest.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Student. </summary>
	public partial class StudentRelations: RelationFactory
	{
		/// <summary>Returns a new IEntityRelation object, between StudentEntity and StudentCourseEntity over the 1:n relation they have, using the relation between the fields: Student.Id - StudentCourse.StudentId</summary>
		public virtual IEntityRelation StudentCourseEntityUsingStudentId
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "StudentCourses", true, new[] { StudentFields.Id, StudentCourseFields.StudentId }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticStudentRelations
	{
		internal static readonly IEntityRelation StudentCourseEntityUsingStudentIdStatic = new StudentRelations().StudentCourseEntityUsingStudentId;

		/// <summary>CTor</summary>
		static StaticStudentRelations() { }
	}
}
