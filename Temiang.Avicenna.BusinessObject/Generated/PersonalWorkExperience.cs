/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/21/2022 7:42:35 PM
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
	abstract public class esPersonalWorkExperienceCollection : esEntityCollectionWAuditLog
	{
		public esPersonalWorkExperienceCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "PersonalWorkExperienceCollection";
		}

		#region Query Logic
		protected void InitQuery(esPersonalWorkExperienceQuery query)
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
			this.InitQuery(query as esPersonalWorkExperienceQuery);
		}
		#endregion

		virtual public PersonalWorkExperience DetachEntity(PersonalWorkExperience entity)
		{
			return base.DetachEntity(entity) as PersonalWorkExperience;
		}

		virtual public PersonalWorkExperience AttachEntity(PersonalWorkExperience entity)
		{
			return base.AttachEntity(entity) as PersonalWorkExperience;
		}

		virtual public void Combine(PersonalWorkExperienceCollection collection)
		{
			base.Combine(collection);
		}

		new public PersonalWorkExperience this[int index]
		{
			get
			{
				return base[index] as PersonalWorkExperience;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(PersonalWorkExperience);
		}
	}

	[Serializable]
	abstract public class esPersonalWorkExperience : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esPersonalWorkExperienceQuery GetDynamicQuery()
		{
			return null;
		}

		public esPersonalWorkExperience()
		{
		}

		public esPersonalWorkExperience(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 personalWorkExperienceID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalWorkExperienceID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalWorkExperienceID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personalWorkExperienceID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personalWorkExperienceID);
			else
				return LoadByPrimaryKeyStoredProcedure(personalWorkExperienceID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 personalWorkExperienceID)
		{
			esPersonalWorkExperienceQuery query = this.GetDynamicQuery();
			query.Where(query.PersonalWorkExperienceID == personalWorkExperienceID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 personalWorkExperienceID)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonalWorkExperienceID", personalWorkExperienceID);
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
						case "PersonalWorkExperienceID": this.str.PersonalWorkExperienceID = (string)value; break;
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SRLineBisnis": this.str.SRLineBisnis = (string)value; break;
						case "StartDate": this.str.StartDate = (string)value; break;
						case "EndDate": this.str.EndDate = (string)value; break;
						case "StartYear": this.str.StartYear = (string)value; break;
						case "EndYear": this.str.EndYear = (string)value; break;
						case "Company": this.str.Company = (string)value; break;
						case "Division": this.str.Division = (string)value; break;
						case "Location": this.str.Location = (string)value; break;
						case "JobDesc": this.str.JobDesc = (string)value; break;
						case "SupervisorName": this.str.SupervisorName = (string)value; break;
						case "LastSalary": this.str.LastSalary = (string)value; break;
						case "ReasonOfLeaving": this.str.ReasonOfLeaving = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonalWorkExperienceID":

							if (value == null || value is System.Int32)
								this.PersonalWorkExperienceID = (System.Int32?)value;
							break;
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "StartDate":

							if (value == null || value is System.DateTime)
								this.StartDate = (System.DateTime?)value;
							break;
						case "EndDate":

							if (value == null || value is System.DateTime)
								this.EndDate = (System.DateTime?)value;
							break;
						case "LastSalary":

							if (value == null || value is System.Decimal)
								this.LastSalary = (System.Decimal?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
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
		/// Maps to PersonalWorkExperience.PersonalWorkExperienceID
		/// </summary>
		virtual public System.Int32? PersonalWorkExperienceID
		{
			get
			{
				return base.GetSystemInt32(PersonalWorkExperienceMetadata.ColumnNames.PersonalWorkExperienceID);
			}

			set
			{
				base.SetSystemInt32(PersonalWorkExperienceMetadata.ColumnNames.PersonalWorkExperienceID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalWorkExperience.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(PersonalWorkExperienceMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(PersonalWorkExperienceMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to PersonalWorkExperience.SRLineBisnis
		/// </summary>
		virtual public System.String SRLineBisnis
		{
			get
			{
				return base.GetSystemString(PersonalWorkExperienceMetadata.ColumnNames.SRLineBisnis);
			}

			set
			{
				base.SetSystemString(PersonalWorkExperienceMetadata.ColumnNames.SRLineBisnis, value);
			}
		}
		/// <summary>
		/// Maps to PersonalWorkExperience.StartDate
		/// </summary>
		virtual public System.DateTime? StartDate
		{
			get
			{
				return base.GetSystemDateTime(PersonalWorkExperienceMetadata.ColumnNames.StartDate);
			}

			set
			{
				base.SetSystemDateTime(PersonalWorkExperienceMetadata.ColumnNames.StartDate, value);
			}
		}
		/// <summary>
		/// Maps to PersonalWorkExperience.EndDate
		/// </summary>
		virtual public System.DateTime? EndDate
		{
			get
			{
				return base.GetSystemDateTime(PersonalWorkExperienceMetadata.ColumnNames.EndDate);
			}

			set
			{
				base.SetSystemDateTime(PersonalWorkExperienceMetadata.ColumnNames.EndDate, value);
			}
		}
		/// <summary>
		/// Maps to PersonalWorkExperience.StartYear
		/// </summary>
		virtual public System.String StartYear
		{
			get
			{
				return base.GetSystemString(PersonalWorkExperienceMetadata.ColumnNames.StartYear);
			}

			set
			{
				base.SetSystemString(PersonalWorkExperienceMetadata.ColumnNames.StartYear, value);
			}
		}
		/// <summary>
		/// Maps to PersonalWorkExperience.EndYear
		/// </summary>
		virtual public System.String EndYear
		{
			get
			{
				return base.GetSystemString(PersonalWorkExperienceMetadata.ColumnNames.EndYear);
			}

			set
			{
				base.SetSystemString(PersonalWorkExperienceMetadata.ColumnNames.EndYear, value);
			}
		}
		/// <summary>
		/// Maps to PersonalWorkExperience.Company
		/// </summary>
		virtual public System.String Company
		{
			get
			{
				return base.GetSystemString(PersonalWorkExperienceMetadata.ColumnNames.Company);
			}

			set
			{
				base.SetSystemString(PersonalWorkExperienceMetadata.ColumnNames.Company, value);
			}
		}
		/// <summary>
		/// Maps to PersonalWorkExperience.Division
		/// </summary>
		virtual public System.String Division
		{
			get
			{
				return base.GetSystemString(PersonalWorkExperienceMetadata.ColumnNames.Division);
			}

			set
			{
				base.SetSystemString(PersonalWorkExperienceMetadata.ColumnNames.Division, value);
			}
		}
		/// <summary>
		/// Maps to PersonalWorkExperience.Location
		/// </summary>
		virtual public System.String Location
		{
			get
			{
				return base.GetSystemString(PersonalWorkExperienceMetadata.ColumnNames.Location);
			}

			set
			{
				base.SetSystemString(PersonalWorkExperienceMetadata.ColumnNames.Location, value);
			}
		}
		/// <summary>
		/// Maps to PersonalWorkExperience.JobDesc
		/// </summary>
		virtual public System.String JobDesc
		{
			get
			{
				return base.GetSystemString(PersonalWorkExperienceMetadata.ColumnNames.JobDesc);
			}

			set
			{
				base.SetSystemString(PersonalWorkExperienceMetadata.ColumnNames.JobDesc, value);
			}
		}
		/// <summary>
		/// Maps to PersonalWorkExperience.SupervisorName
		/// </summary>
		virtual public System.String SupervisorName
		{
			get
			{
				return base.GetSystemString(PersonalWorkExperienceMetadata.ColumnNames.SupervisorName);
			}

			set
			{
				base.SetSystemString(PersonalWorkExperienceMetadata.ColumnNames.SupervisorName, value);
			}
		}
		/// <summary>
		/// Maps to PersonalWorkExperience.LastSalary
		/// </summary>
		virtual public System.Decimal? LastSalary
		{
			get
			{
				return base.GetSystemDecimal(PersonalWorkExperienceMetadata.ColumnNames.LastSalary);
			}

			set
			{
				base.SetSystemDecimal(PersonalWorkExperienceMetadata.ColumnNames.LastSalary, value);
			}
		}
		/// <summary>
		/// Maps to PersonalWorkExperience.ReasonOfLeaving
		/// </summary>
		virtual public System.String ReasonOfLeaving
		{
			get
			{
				return base.GetSystemString(PersonalWorkExperienceMetadata.ColumnNames.ReasonOfLeaving);
			}

			set
			{
				base.SetSystemString(PersonalWorkExperienceMetadata.ColumnNames.ReasonOfLeaving, value);
			}
		}
		/// <summary>
		/// Maps to PersonalWorkExperience.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(PersonalWorkExperienceMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(PersonalWorkExperienceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to PersonalWorkExperience.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(PersonalWorkExperienceMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(PersonalWorkExperienceMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esPersonalWorkExperience entity)
			{
				this.entity = entity;
			}
			public System.String PersonalWorkExperienceID
			{
				get
				{
					System.Int32? data = entity.PersonalWorkExperienceID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PersonalWorkExperienceID = null;
					else entity.PersonalWorkExperienceID = Convert.ToInt32(value);
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
			public System.String SRLineBisnis
			{
				get
				{
					System.String data = entity.SRLineBisnis;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRLineBisnis = null;
					else entity.SRLineBisnis = Convert.ToString(value);
				}
			}
			public System.String StartDate
			{
				get
				{
					System.DateTime? data = entity.StartDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StartDate = null;
					else entity.StartDate = Convert.ToDateTime(value);
				}
			}
			public System.String EndDate
			{
				get
				{
					System.DateTime? data = entity.EndDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EndDate = null;
					else entity.EndDate = Convert.ToDateTime(value);
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
			public System.String Company
			{
				get
				{
					System.String data = entity.Company;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Company = null;
					else entity.Company = Convert.ToString(value);
				}
			}
			public System.String Division
			{
				get
				{
					System.String data = entity.Division;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Division = null;
					else entity.Division = Convert.ToString(value);
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
			public System.String JobDesc
			{
				get
				{
					System.String data = entity.JobDesc;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.JobDesc = null;
					else entity.JobDesc = Convert.ToString(value);
				}
			}
			public System.String SupervisorName
			{
				get
				{
					System.String data = entity.SupervisorName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SupervisorName = null;
					else entity.SupervisorName = Convert.ToString(value);
				}
			}
			public System.String LastSalary
			{
				get
				{
					System.Decimal? data = entity.LastSalary;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastSalary = null;
					else entity.LastSalary = Convert.ToDecimal(value);
				}
			}
			public System.String ReasonOfLeaving
			{
				get
				{
					System.String data = entity.ReasonOfLeaving;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReasonOfLeaving = null;
					else entity.ReasonOfLeaving = Convert.ToString(value);
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
			private esPersonalWorkExperience entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esPersonalWorkExperienceQuery query)
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
				throw new Exception("esPersonalWorkExperience can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class PersonalWorkExperience : esPersonalWorkExperience
	{
	}

	[Serializable]
	abstract public class esPersonalWorkExperienceQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return PersonalWorkExperienceMetadata.Meta();
			}
		}

		public esQueryItem PersonalWorkExperienceID
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.PersonalWorkExperienceID, esSystemType.Int32);
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SRLineBisnis
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.SRLineBisnis, esSystemType.String);
			}
		}

		public esQueryItem StartDate
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.StartDate, esSystemType.DateTime);
			}
		}

		public esQueryItem EndDate
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.EndDate, esSystemType.DateTime);
			}
		}

		public esQueryItem StartYear
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.StartYear, esSystemType.String);
			}
		}

		public esQueryItem EndYear
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.EndYear, esSystemType.String);
			}
		}

		public esQueryItem Company
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.Company, esSystemType.String);
			}
		}

		public esQueryItem Division
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.Division, esSystemType.String);
			}
		}

		public esQueryItem Location
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.Location, esSystemType.String);
			}
		}

		public esQueryItem JobDesc
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.JobDesc, esSystemType.String);
			}
		}

		public esQueryItem SupervisorName
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.SupervisorName, esSystemType.String);
			}
		}

		public esQueryItem LastSalary
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.LastSalary, esSystemType.Decimal);
			}
		}

		public esQueryItem ReasonOfLeaving
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.ReasonOfLeaving, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, PersonalWorkExperienceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("PersonalWorkExperienceCollection")]
	public partial class PersonalWorkExperienceCollection : esPersonalWorkExperienceCollection, IEnumerable<PersonalWorkExperience>
	{
		public PersonalWorkExperienceCollection()
		{

		}

		public static implicit operator List<PersonalWorkExperience>(PersonalWorkExperienceCollection coll)
		{
			List<PersonalWorkExperience> list = new List<PersonalWorkExperience>();

			foreach (PersonalWorkExperience emp in coll)
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
				return PersonalWorkExperienceMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalWorkExperienceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new PersonalWorkExperience(row);
		}

		override protected esEntity CreateEntity()
		{
			return new PersonalWorkExperience();
		}

		#endregion

		[BrowsableAttribute(false)]
		public PersonalWorkExperienceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalWorkExperienceQuery();
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
		public bool Load(PersonalWorkExperienceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public PersonalWorkExperience AddNew()
		{
			PersonalWorkExperience entity = base.AddNewEntity() as PersonalWorkExperience;

			return entity;
		}
		public PersonalWorkExperience FindByPrimaryKey(Int32 personalWorkExperienceID)
		{
			return base.FindByPrimaryKey(personalWorkExperienceID) as PersonalWorkExperience;
		}

		#region IEnumerable< PersonalWorkExperience> Members

		IEnumerator<PersonalWorkExperience> IEnumerable<PersonalWorkExperience>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as PersonalWorkExperience;
			}
		}

		#endregion

		private PersonalWorkExperienceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'PersonalWorkExperience' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("PersonalWorkExperience ({PersonalWorkExperienceID})")]
	[Serializable]
	public partial class PersonalWorkExperience : esPersonalWorkExperience
	{
		public PersonalWorkExperience()
		{
		}

		public PersonalWorkExperience(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PersonalWorkExperienceMetadata.Meta();
			}
		}

		override protected esPersonalWorkExperienceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PersonalWorkExperienceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public PersonalWorkExperienceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PersonalWorkExperienceQuery();
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
		public bool Load(PersonalWorkExperienceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private PersonalWorkExperienceQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class PersonalWorkExperienceQuery : esPersonalWorkExperienceQuery
	{
		public PersonalWorkExperienceQuery()
		{

		}

		public PersonalWorkExperienceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "PersonalWorkExperienceQuery";
		}
	}

	[Serializable]
	public partial class PersonalWorkExperienceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PersonalWorkExperienceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.PersonalWorkExperienceID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.PersonalWorkExperienceID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.PersonID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.PersonID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.SRLineBisnis, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.SRLineBisnis;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.StartDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.StartDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.EndDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.EndDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.StartYear, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.StartYear;
			c.CharacterMaxLength = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.EndYear, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.EndYear;
			c.CharacterMaxLength = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.Company, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.Company;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.Division, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.Division;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.Location, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.Location;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.JobDesc, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.JobDesc;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.SupervisorName, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.SupervisorName;
			c.CharacterMaxLength = 50;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.LastSalary, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.LastSalary;
			c.NumericPrecision = 19;
			c.NumericScale = 4;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.ReasonOfLeaving, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.ReasonOfLeaving;
			c.CharacterMaxLength = 16;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.LastUpdateDateTime, 14, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);

			c = new esColumnMetadata(PersonalWorkExperienceMetadata.ColumnNames.LastUpdateByUserID, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = PersonalWorkExperienceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);


		}
		#endregion

		static public PersonalWorkExperienceMetadata Meta()
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
			public const string PersonalWorkExperienceID = "PersonalWorkExperienceID";
			public const string PersonID = "PersonID";
			public const string SRLineBisnis = "SRLineBisnis";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string StartYear = "StartYear";
			public const string EndYear = "EndYear";
			public const string Company = "Company";
			public const string Division = "Division";
			public const string Location = "Location";
			public const string JobDesc = "JobDesc";
			public const string SupervisorName = "SupervisorName";
			public const string LastSalary = "LastSalary";
			public const string ReasonOfLeaving = "ReasonOfLeaving";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonalWorkExperienceID = "PersonalWorkExperienceID";
			public const string PersonID = "PersonID";
			public const string SRLineBisnis = "SRLineBisnis";
			public const string StartDate = "StartDate";
			public const string EndDate = "EndDate";
			public const string StartYear = "StartYear";
			public const string EndYear = "EndYear";
			public const string Company = "Company";
			public const string Division = "Division";
			public const string Location = "Location";
			public const string JobDesc = "JobDesc";
			public const string SupervisorName = "SupervisorName";
			public const string LastSalary = "LastSalary";
			public const string ReasonOfLeaving = "ReasonOfLeaving";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
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
			lock (typeof(PersonalWorkExperienceMetadata))
			{
				if (PersonalWorkExperienceMetadata.mapDelegates == null)
				{
					PersonalWorkExperienceMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (PersonalWorkExperienceMetadata.meta == null)
				{
					PersonalWorkExperienceMetadata.meta = new PersonalWorkExperienceMetadata();
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

				meta.AddTypeMap("PersonalWorkExperienceID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRLineBisnis", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StartDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("EndDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("StartYear", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("EndYear", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Company", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Division", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Location", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("JobDesc", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SupervisorName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastSalary", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("ReasonOfLeaving", new esTypeMap("text", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "PersonalWorkExperience";
				meta.Destination = "PersonalWorkExperience";
				meta.spInsert = "proc_PersonalWorkExperienceInsert";
				meta.spUpdate = "proc_PersonalWorkExperienceUpdate";
				meta.spDelete = "proc_PersonalWorkExperienceDelete";
				meta.spLoadAll = "proc_PersonalWorkExperienceLoadAll";
				meta.spLoadByPrimaryKey = "proc_PersonalWorkExperienceLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PersonalWorkExperienceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
