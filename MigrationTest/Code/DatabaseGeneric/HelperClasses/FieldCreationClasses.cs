﻿//////////////////////////////////////////////////////////////
// <auto-generated>This code was generated by LLBLGen Pro 5.10.</auto-generated>
//////////////////////////////////////////////////////////////
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates
// Templates vendor: Solutions Design.
//////////////////////////////////////////////////////////////
using System;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace MigrationTest.HelperClasses
{
	/// <summary>Field Creation Class for entity CourseEntity</summary>
	public partial class CourseFields
	{
		/// <summary>Creates a new CourseEntity.Id field instance</summary>
		public static EntityField2 Id { get { return ModelInfoProviderSingleton.GetInstance().CreateField2(CourseFieldIndex.Id); }}
		/// <summary>Creates a new CourseEntity.Instructor field instance</summary>
		public static EntityField2 Instructor { get { return ModelInfoProviderSingleton.GetInstance().CreateField2(CourseFieldIndex.Instructor); }}
		/// <summary>Creates a new CourseEntity.Name field instance</summary>
		public static EntityField2 Name { get { return ModelInfoProviderSingleton.GetInstance().CreateField2(CourseFieldIndex.Name); }}
	}

	/// <summary>Field Creation Class for entity StudentEntity</summary>
	public partial class StudentFields
	{
		/// <summary>Creates a new StudentEntity.Email field instance</summary>
		public static EntityField2 Email { get { return ModelInfoProviderSingleton.GetInstance().CreateField2(StudentFieldIndex.Email); }}
		/// <summary>Creates a new StudentEntity.Grade field instance</summary>
		public static EntityField2 Grade { get { return ModelInfoProviderSingleton.GetInstance().CreateField2(StudentFieldIndex.Grade); }}
		/// <summary>Creates a new StudentEntity.Id field instance</summary>
		public static EntityField2 Id { get { return ModelInfoProviderSingleton.GetInstance().CreateField2(StudentFieldIndex.Id); }}
		/// <summary>Creates a new StudentEntity.Name field instance</summary>
		public static EntityField2 Name { get { return ModelInfoProviderSingleton.GetInstance().CreateField2(StudentFieldIndex.Name); }}
	}

	/// <summary>Field Creation Class for entity StudentCourseEntity</summary>
	public partial class StudentCourseFields
	{
		/// <summary>Creates a new StudentCourseEntity.CourseId field instance</summary>
		public static EntityField2 CourseId { get { return ModelInfoProviderSingleton.GetInstance().CreateField2(StudentCourseFieldIndex.CourseId); }}
		/// <summary>Creates a new StudentCourseEntity.Id field instance</summary>
		public static EntityField2 Id { get { return ModelInfoProviderSingleton.GetInstance().CreateField2(StudentCourseFieldIndex.Id); }}
		/// <summary>Creates a new StudentCourseEntity.RegistrationDate field instance</summary>
		public static EntityField2 RegistrationDate { get { return ModelInfoProviderSingleton.GetInstance().CreateField2(StudentCourseFieldIndex.RegistrationDate); }}
		/// <summary>Creates a new StudentCourseEntity.StudentId field instance</summary>
		public static EntityField2 StudentId { get { return ModelInfoProviderSingleton.GetInstance().CreateField2(StudentCourseFieldIndex.StudentId); }}
	}
	

}