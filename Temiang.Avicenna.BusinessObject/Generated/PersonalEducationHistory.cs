/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 3/22/2021 7:47:25 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;
using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;

namespace Temiang.Avicenna.BusinessObject
{
	[Serializable]
	abstract public class esPersonalEducationHistoryCollection : esEntityCollectionWAuditLog
	{
		public esPersonalEducationHistoryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PersonalEducationHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonalEducationHistoryQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			query.es2.Connection = ((IEntityCollection)this).Connection;
		}

		protected bool OnQueryLoaded(DataTable table)
		{
			this.PopulateCollection(table);
			return (this.RowCount > 0) ? true : false;
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery(query as esPersonalEducationHistoryQuery);
		}
		#endregion

		virtual public PersonalEducationHistory DetachEntity(PersonalEducationHistory entity)
		{
			return base.DetachEntity(entity) as PersonalEducationHistory;
		}

		virtual public PersonalEducationHistory AttachEntity(PersonalEducationHistory entity)
		{
			return base.AttachEntity(entity) as PersonalEducationHistory;
		}

		virtual public void Combine(PersonalEducationHistoryCollection collection)
		{
			base.Combine(collection);
		}

		new public PersonalEducationHistory this[int index]
		{
			get
			{
				return base[index] as PersonalEducationHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalEducationHistory);
		}
	}

	[Serializable]
	abstract public class esPersonalEducationHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalEducationHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonalEducationHistory()
		{
		}

		public esPersonalEducationHistory(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 personalEducationHistoryID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalEducationHistoryID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalEducationHistoryID);
		}

		/// <summary>
		/// Loads an entity by primary key
		/// </summary>
		/// <remarks>
		/// Requires primary keys be defined on all tables.
		/// If a table does not have a primary key set,
		/// this method will not compile.
		/// </remarks>
		/// <param name="sqlAccessType">Either esSqlAccessType StoredProcedure or DynamicSQL</param>
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personalEducationHistoryID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalEducationHistoryID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalEducationHistoryID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 personalEducationHistoryID)
		{
			esPersonalEducationHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.PersonalEducationHistoryID == personalEducationHistoryID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 personalEducationHistoryID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonalEducationHistoryID", personalEducationHistoryID);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion

		#region Properties

		public override void SetProperties(IDictionary values)
		{
			foreach (string propertyName in values.Keys)
			{
				this.SetProperty(propertyName, values[propertyName]);
			}
		}

		public override void SetProperty(string name, object value)
		{
			if (this.Row == null) this.AddNew();

			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if (value == null || value is System.String)
				{
					// Use the strongly typed property
					switch (name)
					{
						case "PersonalEducationHistoryID": this.str.PersonalEducationHistoryID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SREducationLevel": this.str.SREducationLevel = (string)value; break;
						case "InstitutionName": this.str.InstitutionName = (string)value; break;
						case "Location": this.str.Location = (string)value; break;
						case "StartYear": this.str.StartYear = (string)value; break;
						case "EndYear": this.str.EndYear = (string)value; break;
						case "Gpa": this.str.Gpa = (string)value; break;
						case "Achievement": this.str.Achievement = (string)value; break;
						case "Note": this.str.Note = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "Majors": this.str.Majors = (string)value; break;
						case "GraduateDate": this.str.GraduateDate = (string)value; break;
						case "DiplomaNo": this.str.DiplomaNo = (string)value; break;
						case "DiplomaVerificationNo": this.str.DiplomaVerificationNo = (string)value; break;
						case "EducationalAdjustmentDate": this.str.EducationalAdjustmentDate = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonalEducationHistoryID":

							if (value == null || value is System.Int32)
								this.PersonalEducationHistoryID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "Gpa":

							if (value == null || value is System.Decimal)
								this.Gpa = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "GraduateDate":

							if (value == null || value is System.DateTime)
								this.GraduateDate = (System.DateTime?)value;
							break;
						case "EducationalAdjustmentDate":

							if (value == null || value is System.DateTime)
								this.EducationalAdjustmentDate = (System.DateTime?)value;
							break;

						default:
							break;
					}
				}
			}
			else if (this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to PersonalEducationHistory.PersonalEducationHistoryID
		/// </summary>
		virtual public System.Int32? PersonalEducationHistoryID
		{
			get
			{
				return base.GetSystemInt32(PersonalEducationHistoryMetadata.ColumnNames.PersonalEducationHistoryID);
			}

			set
			{
				base.SetSystemInt32(PersonalEducationHistoryMetadata.ColumnNames.PersonalEducationHistoryID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PersonalEducationHistoryMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(PersonalEducationHistoryMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.SREducationLevel
		/// </summary>
		virtual public System.String SREducationLevel
		{
			get
			{
				return base.GetSystemString(PersonalEducationHistoryMetadata.ColumnNames.SREducationLevel);
			}

			set
			{
				base.SetSystemString(PersonalEducationHistoryMetadata.ColumnNames.SREducationLevel, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.InstitutionName
		/// </summary>
		virtual public System.String InstitutionName
		{
			get
			{
				return base.GetSystemString(PersonalEducationHistoryMetadata.ColumnNames.InstitutionName);
			}

			set
			{
				base.SetSystemString(PersonalEducationHistoryMetadata.ColumnNames.InstitutionName, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.Location
		/// </summary>
		virtual public System.String Location
		{
			get
			{
				return base.GetSystemString(PersonalEducationHistoryMetadata.ColumnNames.Location);
			}

			set
			{
				base.SetSystemString(PersonalEducationHistoryMetadata.ColumnNames.Location, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.StartYear
		/// </summary>
		virtual public System.String StartYear
		{
			get
			{
				return base.GetSystemString(PersonalEducationHistoryMetadata.ColumnNames.StartYear);
			}

			set
			{
				base.SetSystemString(PersonalEducationHistoryMetadata.ColumnNames.StartYear, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.EndYear
		/// </summary>
		virtual public System.String EndYear
		{
			get
			{
				return base.GetSystemString(PersonalEducationHistoryMetadata.ColumnNames.EndYear);
			}

			set
			{
				base.SetSystemString(PersonalEducationHistoryMetadata.ColumnNames.EndYear, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.Gpa
		/// </summary>
		virtual public System.Decimal? Gpa
		{
			get
			{
				return base.GetSystemDecimal(PersonalEducationHistoryMetadata.ColumnNames.Gpa);
			}

			set
			{
				base.SetSystemDecimal(PersonalEducationHistoryMetadata.ColumnNames.Gpa, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.Achievement
		/// </summary>
		virtual public System.String Achievement
		{
			get
			{
				return base.GetSystemString(PersonalEducationHistoryMetadata.ColumnNames.Achievement);
			}

			set
			{
				base.SetSystemString(PersonalEducationHistoryMetadata.ColumnNames.Achievement, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.Note
		/// </summary>
		virtual public System.String Note
		{
			get
			{
				return base.GetSystemString(PersonalEducationHistoryMetadata.ColumnNames.Note);
			}

			set
			{
				base.SetSystemString(PersonalEducationHistoryMetadata.ColumnNames.Note, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonalEducationHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PersonalEducationHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonalEducationHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PersonalEducationHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.Majors
		/// </summary>
		virtual public System.String Majors
		{
			get
			{
				return base.GetSystemString(PersonalEducationHistoryMetadata.ColumnNames.Majors);
			}

			set
			{
				base.SetSystemString(PersonalEducationHistoryMetadata.ColumnNames.Majors, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.GraduateDate
		/// </summary>
		virtual public System.DateTime? GraduateDate
		{
			get
			{
				return base.GetSystemDateTime(PersonalEducationHistoryMetadata.ColumnNames.GraduateDate);
			}

			set
			{
				base.SetSystemDateTime(PersonalEducationHistoryMetadata.ColumnNames.GraduateDate, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.DiplomaNo
		/// </summary>
		virtual public System.String DiplomaNo
		{
			get
			{
				return base.GetSystemString(PersonalEducationHistoryMetadata.ColumnNames.DiplomaNo);
			}

			set
			{
				base.SetSystemString(PersonalEducationHistoryMetadata.ColumnNames.DiplomaNo, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.DiplomaVerificationNo
		/// </summary>
		virtual public System.String DiplomaVerificationNo
		{
			get
			{
				return base.GetSystemString(PersonalEducationHistoryMetadata.ColumnNames.DiplomaVerificationNo);
			}

			set
			{
				base.SetSystemString(PersonalEducationHistoryMetadata.ColumnNames.DiplomaVerificationNo, value);
			}
		}
		/// <summary>
		/// Maps to PersonalEducationHistory.EducationalAdjustmentDate
		/// </summary>
		virtual public System.DateTime? EducationalAdjustmentDate
		{
			get
			{
				return base.GetSystemDateTime(PersonalEducationHistoryMetadata.ColumnNames.EducationalAdjustmentDate);
			}

			set
			{
				base.SetSystemDateTime(PersonalEducationHistoryMetadata.ColumnNames.EducationalAdjustmentDate, value);
			}
		}

		#endregion

		#region String Properties

		/// <summary>
		/// Converts an entity's properties to
		/// and from strings.
		/// </summary>
		/// <remarks>
		/// The str properties Get and Set provide easy conversion
		/// between a string and a property's data type. Not all
		/// data types will get a str property.
		/// </remarks>
		/// <example>
		/// Set a datetime from a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// entity.str.HireDate = "2007-01-01 00:00:00";
		/// entity.Save();
		/// </code>
		/// Get a datetime as a string.
		/// <code>
		/// Employees entity = new Employees();
		/// entity.LoadByPrimaryKey(10);
		/// string theDate = entity.str.HireDate;
		/// </code>
		/// </example>
		[BrowsableAttribute(false)]
		public esStrings str
		{
			get
			{
				if (esstrings == null)
				{
					esstrings = new esStrings(this);
				}
				return esstrings;
			}
		}

		[Serializable]
		sealed public class esStrings
		{
			public esStrings(esPersonalEducationHistory entity)
			{
				this.entity = entity;
			}
			public System.String PersonalEducationHistoryID
			{
				get
				{
					System.Int32? data = entity.PersonalEducationHistoryID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonalEducationHistoryID = null;
					else entity.PersonalEducationHistoryID = Convert.ToInt32(value);
				}
			}
			public System.String PersonID
			{
				get
				{
					System.Int32? data = entity.PersonID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonID = null;
					else entity.PersonID = Convert.ToInt32(value);
				}
			}
			public System.String SREducationLevel
			{
				get
				{
					System.String data = entity.SREducationLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationLevel = null;
					else entity.SREducationLevel = Convert.ToString(value);
				}
			}
			public System.String InstitutionName
			{
				get
				{
					System.String data = entity.InstitutionName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InstitutionName = null;
					else entity.InstitutionName = Convert.ToString(value);
				}
			}
			public System.String Location
			{
				get
				{
					System.String data = entity.Location;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Location = null;
					else entity.Location = Convert.ToString(value);
				}
			}
			public System.String StartYear
			{
				get
				{
					System.String data = entity.StartYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartYear = null;
					else entity.StartYear = Convert.ToString(value);
				}
			}
			public System.String EndYear
			{
				get
				{
					System.String data = entity.EndYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndYear = null;
					else entity.EndYear = Convert.ToString(value);
				}
			}
			public System.String Gpa
			{
				get
				{
					System.Decimal? data = entity.Gpa;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Gpa = null;
					else entity.Gpa = Convert.ToDecimal(value);
				}
			}
			public System.String Achievement
			{
				get
				{
					System.String data = entity.Achievement;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Achievement = null;
					else entity.Achievement = Convert.ToString(value);
				}
			}
			public System.String Note
			{
				get
				{
					System.String data = entity.Note;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Note = null;
					else entity.Note = Convert.ToString(value);
				}
			}
			public System.String LastUpdateDateTime
			{
				get
				{
					System.DateTime? data = entity.LastUpdateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateDateTime = null;
					else entity.LastUpdateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String LastUpdateByUserID
			{
				get
				{
					System.String data = entity.LastUpdateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdateByUserID = null;
					else entity.LastUpdateByUserID = Convert.ToString(value);
				}
			}
			public System.String Majors
			{
				get
				{
					System.String data = entity.Majors;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Majors = null;
					else entity.Majors = Convert.ToString(value);
				}
			}
			public System.String GraduateDate
			{
				get
				{
					System.DateTime? data = entity.GraduateDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GraduateDate = null;
					else entity.GraduateDate = Convert.ToDateTime(value);
				}
			}
			public System.String DiplomaNo
			{
				get
				{
					System.String data = entity.DiplomaNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiplomaNo = null;
					else entity.DiplomaNo = Convert.ToString(value);
				}
			}
			public System.String DiplomaVerificationNo
			{
				get
				{
					System.String data = entity.DiplomaVerificationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiplomaVerificationNo = null;
					else entity.DiplomaVerificationNo = Convert.ToString(value);
				}
			}
			public System.String EducationalAdjustmentDate
			{
				get
				{
					System.DateTime? data = entity.EducationalAdjustmentDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EducationalAdjustmentDate = null;
					else entity.EducationalAdjustmentDate = Convert.ToDateTime(value);
				}
			}
			private esPersonalEducationHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalEducationHistoryQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			query.es2.Connection = ((IEntity)this).Connection;
		}

		[System.Diagnostics.DebuggerNonUserCode]
		protected bool OnQueryLoaded(DataTable table)
		{
			bool dataFound = this.PopulateEntity(table);

			if (this.RowCount > 1)
			{
				throw new Exception("esPersonalEducationHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PersonalEducationHistory : esPersonalEducationHistory
	{
	}

	[Serializable]
	abstract public class esPersonalEducationHistoryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PersonalEducationHistoryMetadata.Meta();
			}
		}

		public esQueryItem PersonalEducationHistoryID
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.PersonalEducationHistoryID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SREducationLevel
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.SREducationLevel, esSystemType.String);
			}
		}

		public esQueryItem InstitutionName
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.InstitutionName, esSystemType.String);
			}
		}

		public esQueryItem Location
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.Location, esSystemType.String);
			}
		}

		public esQueryItem StartYear
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.StartYear, esSystemType.String);
			}
		}

		public esQueryItem EndYear
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.EndYear, esSystemType.String);
			}
		}

		public esQueryItem Gpa
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.Gpa, esSystemType.Decimal);
			}
		}

		public esQueryItem Achievement
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.Achievement, esSystemType.String);
			}
		}

		public esQueryItem Note
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.Note, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem Majors
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.Majors, esSystemType.String);
			}
		}

		public esQueryItem GraduateDate
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.GraduateDate, esSystemType.DateTime);
			}
		}

		public esQueryItem DiplomaNo
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.DiplomaNo, esSystemType.String);
			}
		}

		public esQueryItem DiplomaVerificationNo
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.DiplomaVerificationNo, esSystemType.String);
			}
		}

		public esQueryItem EducationalAdjustmentDate
		{
			get
			{
				return new esQueryItem(this, PersonalEducationHistoryMetadata.ColumnNames.EducationalAdjustmentDate, esSystemType.DateTime);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalEducationHistoryCollection")]
	public partial class PersonalEducationHistoryCollection : esPersonalEducationHistoryCollection, IEnumerable<PersonalEducationHistory>
	{
		public PersonalEducationHistoryCollection()
		{

		}

		public static implicit operator List<PersonalEducationHistory>(PersonalEducationHistoryCollection coll)
		{
			List<PersonalEducationHistory> list = new List<PersonalEducationHistory>();

			foreach (PersonalEducationHistory emp in coll)
			{
				list.Add(emp);
			}

			return list;
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalEducationHistoryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalEducationHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalEducationHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalEducationHistory();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PersonalEducationHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalEducationHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}

		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one record was loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(PersonalEducationHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PersonalEducationHistory AddNew()
		{
			PersonalEducationHistory entity = base.AddNewEntity() as PersonalEducationHistory;

			return entity;
		}
		public PersonalEducationHistory FindByPrimaryKey(Int32 personalEducationHistoryID)
		{
			return base.FindByPrimaryKey(personalEducationHistoryID) as PersonalEducationHistory;
		}

		#region IEnumerable< PersonalEducationHistory> Members

		IEnumerator<PersonalEducationHistory> IEnumerable<PersonalEducationHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PersonalEducationHistory;
			}
		}

		#endregion

		private PersonalEducationHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalEducationHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PersonalEducationHistory ({PersonalEducationHistoryID})")]
	[Serializable]
	public partial class PersonalEducationHistory : esPersonalEducationHistory
	{
		public PersonalEducationHistory()
		{
		}

		public PersonalEducationHistory(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalEducationHistoryMetadata.Meta();
			}
		}

		override protected esPersonalEducationHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalEducationHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PersonalEducationHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalEducationHistoryQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		/// <summary>
		/// Useful for building up conditional queries.
		/// In most cases, before loading an entity or collection,
		/// you should instantiate a new one. This method was added
		/// to handle specialized circumstances, and should not be
		/// used as a substitute for that.
		/// </summary>
		/// <remarks>
		/// This just sets obj.Query to null/Nothing.
		/// In most cases, you will 'new' your object before
		/// loading it, rather than calling this method.
		/// It only affects obj.Query.Load(), so is not useful
		/// when Joins are involved, or for many other situations.
		/// Because it clears out any obj.Query.Where clauses,
		/// it can be useful for building conditional queries on the fly.
		/// <code>
		/// public bool ReQuery(string lastName, string firstName)
		/// {
		///     this.QueryReset();
		///     
		///     if(!String.IsNullOrEmpty(lastName))
		///     {
		///         this.Query.Where(
		///             this.Query.LastName == lastName);
		///     }
		///     if(!String.IsNullOrEmpty(firstName))
		///     {
		///         this.Query.Where(
		///             this.Query.FirstName == firstName);
		///     }
		///     
		///     return this.Query.Load();
		/// }
		/// </code>
		/// <code lang="vbnet">
		/// Public Function ReQuery(ByVal lastName As String, _
		///     ByVal firstName As String) As Boolean
		/// 
		///     Me.QueryReset()
		/// 
		///     If Not [String].IsNullOrEmpty(lastName) Then
		///         Me.Query.Where(Me.Query.LastName = lastName)
		///     End If
		///     If Not [String].IsNullOrEmpty(firstName) Then
		///         Me.Query.Where(Me.Query.FirstName = firstName)
		///     End If
		/// 
		///     Return Me.Query.Load()
		/// End Function
		/// </code>
		/// </remarks>
		public void QueryReset()
		{
			this.query = null;
		}

		/// <summary>
		/// Used to custom load a Join query.
		/// Returns true if at least one row is loaded.
		/// For an entity, an exception will be thrown
		/// if more than one row is loaded.
		/// </summary>
		/// <remarks>
		/// Provides support for InnerJoin, LeftJoin,
		/// RightJoin, and FullJoin. You must provide an alias
		/// for each query when instantiating them.
		/// <code>
		/// EmployeeCollection collection = new EmployeeCollection();
		/// 
		/// EmployeeQuery emp = new EmployeeQuery("eq");
		/// CustomerQuery cust = new CustomerQuery("cq");
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName);
		/// emp.LeftJoin(cust).On(emp.EmployeeID == cust.StaffAssigned);
		/// 
		/// collection.Load(emp);
		/// </code>
		/// <code lang="vbnet">
		/// Dim collection As New EmployeeCollection()
		/// 
		/// Dim emp As New EmployeeQuery("eq")
		/// Dim cust As New CustomerQuery("cq")
		/// 
		/// emp.Select(emp.EmployeeID, emp.LastName, cust.CustomerName)
		/// emp.LeftJoin(cust).On(emp.EmployeeID = cust.StaffAssigned)
		/// 
		/// collection.Load(emp)
		/// </code>
		/// </remarks>
		/// <param name="query">The query object instance name.</param>
		/// <returns>True if at least one record was loaded.</returns>
		public bool Load(PersonalEducationHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PersonalEducationHistoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PersonalEducationHistoryQuery : esPersonalEducationHistoryQuery
	{
		public PersonalEducationHistoryQuery()
		{

		}

		public PersonalEducationHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PersonalEducationHistoryQuery";
		}
	}

	[Serializable]
	public partial class PersonalEducationHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalEducationHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.PersonalEducationHistoryID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.PersonalEducationHistoryID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.SREducationLevel, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.SREducationLevel;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.InstitutionName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.InstitutionName;
			c.CharacterMaxLength = 50;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.Location, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.Location;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.StartYear, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.StartYear;
			c.CharacterMaxLength = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.EndYear, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.EndYear;
			c.CharacterMaxLength = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.Gpa, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.Gpa;
			c.NumericPrecision = 3;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.Achievement, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.Achievement;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.Note, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.Note;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.LastUpdateDateTime, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.LastUpdateByUserID, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.Majors, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.Majors;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.GraduateDate, 13, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.GraduateDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.DiplomaNo, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.DiplomaNo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.DiplomaVerificationNo, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.DiplomaVerificationNo;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalEducationHistoryMetadata.ColumnNames.EducationalAdjustmentDate, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalEducationHistoryMetadata.PropertyNames.EducationalAdjustmentDate;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public PersonalEducationHistoryMetadata Meta()
		{
			return meta;
		}

		public Guid DataID
		{
			get { return base._dataID; }
		}

		public bool MultiProviderMode
		{
			get { return false; }
		}

		public esColumnMetadataCollection Columns
		{
			get { return base._columns; }
		}

		#region ColumnNames
		public class ColumnNames
		{
			public const string PersonalEducationHistoryID = "PersonalEducationHistoryID";
			public const string PersonID = "PersonID";
			public const string SREducationLevel = "SREducationLevel";
			public const string InstitutionName = "InstitutionName";
			public const string Location = "Location";
			public const string StartYear = "StartYear";
			public const string EndYear = "EndYear";
			public const string Gpa = "Gpa";
			public const string Achievement = "Achievement";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Majors = "Majors";
			public const string GraduateDate = "GraduateDate";
			public const string DiplomaNo = "DiplomaNo";
			public const string DiplomaVerificationNo = "DiplomaVerificationNo";
			public const string EducationalAdjustmentDate = "EducationalAdjustmentDate";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonalEducationHistoryID = "PersonalEducationHistoryID";
			public const string PersonID = "PersonID";
			public const string SREducationLevel = "SREducationLevel";
			public const string InstitutionName = "InstitutionName";
			public const string Location = "Location";
			public const string StartYear = "StartYear";
			public const string EndYear = "EndYear";
			public const string Gpa = "Gpa";
			public const string Achievement = "Achievement";
			public const string Note = "Note";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Majors = "Majors";
			public const string GraduateDate = "GraduateDate";
			public const string DiplomaNo = "DiplomaNo";
			public const string DiplomaVerificationNo = "DiplomaVerificationNo";
			public const string EducationalAdjustmentDate = "EducationalAdjustmentDate";
		}
		#endregion

		public esProviderSpecificMetadata GetProviderMetadata(string mapName)
		{
			MapToMeta mapMethod = mapDelegates[mapName];

			if (mapMethod != null)
				return mapMethod(mapName);
			else
				return null;
		}

		#region MAP esDefault

		static private int RegisterDelegateesDefault()
		{
			// This is only executed once per the life of the application
			lock (typeof(PersonalEducationHistoryMetadata))
			{
				if (PersonalEducationHistoryMetadata.mapDelegates == null)
				{
					PersonalEducationHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PersonalEducationHistoryMetadata.meta == null)
				{
					PersonalEducationHistoryMetadata.meta = new PersonalEducationHistoryMetadata();
				}

				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if (!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();

				meta.AddTypeMap("PersonalEducationHistoryID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREducationLevel", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InstitutionName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Location", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartYear", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("EndYear", new esTypeMap("char", "System.String"));
				meta.AddTypeMap("Gpa", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Achievement", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Note", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Majors", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GraduateDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DiplomaNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DiplomaVerificationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EducationalAdjustmentDate", new esTypeMap("datetime", "System.DateTime"));


				meta.Source = "PersonalEducationHistory";
				meta.Destination = "PersonalEducationHistory";
				meta.spInsert = "proc_PersonalEducationHistoryInsert";
				meta.spUpdate = "proc_PersonalEducationHistoryUpdate";
				meta.spDelete = "proc_PersonalEducationHistoryDelete";
				meta.spLoadAll = "proc_PersonalEducationHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalEducationHistoryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalEducationHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
