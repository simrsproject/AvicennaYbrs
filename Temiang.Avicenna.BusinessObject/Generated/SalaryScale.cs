/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/19/2022 5:07:21 PM
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
	abstract public class esSalaryScaleCollection : esEntityCollectionWAuditLog
	{
		public esSalaryScaleCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SalaryScaleCollection";
		}

		#region Query Logic
		protected void InitQuery(esSalaryScaleQuery query)
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
			this.InitQuery(query as esSalaryScaleQuery);
		}
		#endregion

		virtual public SalaryScale DetachEntity(SalaryScale entity)
		{
			return base.DetachEntity(entity) as SalaryScale;
		}

		virtual public SalaryScale AttachEntity(SalaryScale entity)
		{
			return base.AttachEntity(entity) as SalaryScale;
		}

		virtual public void Combine(SalaryScaleCollection collection)
		{
			base.Combine(collection);
		}

		new public SalaryScale this[int index]
		{
			get
			{
				return base[index] as SalaryScale;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SalaryScale);
		}
	}

	[Serializable]
	abstract public class esSalaryScale : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSalaryScaleQuery GetDynamicQuery()
		{
			return null;
		}

		public esSalaryScale()
		{
		}

		public esSalaryScale(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 salaryScaleID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(salaryScaleID);
			else
				return LoadByPrimaryKeyStoredProcedure(salaryScaleID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 salaryScaleID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(salaryScaleID);
			else
				return LoadByPrimaryKeyStoredProcedure(salaryScaleID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 salaryScaleID)
		{
			esSalaryScaleQuery query = this.GetDynamicQuery();
			query.Where(query.SalaryScaleID == salaryScaleID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 salaryScaleID)
		{
			esParameters parms = new esParameters();
			parms.Add("SalaryScaleID", salaryScaleID);
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
						case "SalaryScaleID": this.str.SalaryScaleID = (string)value; break;
						case "SalaryScaleCode": this.str.SalaryScaleCode = (string)value; break;
						case "SalaryScaleName": this.str.SalaryScaleName = (string)value; break;
						case "PositionGradeID": this.str.PositionGradeID = (string)value; break;
						case "SREmploymentType": this.str.SREmploymentType = (string)value; break;
						case "SRProfessionGroup": this.str.SRProfessionGroup = (string)value; break;
						case "SREducationGroup": this.str.SREducationGroup = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "IsActive": this.str.IsActive = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "SalaryScaleID":

							if (value == null || value is System.Int32)
								this.SalaryScaleID = (System.Int32?)value;
							break;
						case "PositionGradeID":

							if (value == null || value is System.Int32)
								this.PositionGradeID = (System.Int32?)value;
							break;
						case "IsActive":

							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to SalaryScale.SalaryScaleID
		/// </summary>
		virtual public System.Int32? SalaryScaleID
		{
			get
			{
				return base.GetSystemInt32(SalaryScaleMetadata.ColumnNames.SalaryScaleID);
			}

			set
			{
				base.SetSystemInt32(SalaryScaleMetadata.ColumnNames.SalaryScaleID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryScale.SalaryScaleCode
		/// </summary>
		virtual public System.String SalaryScaleCode
		{
			get
			{
				return base.GetSystemString(SalaryScaleMetadata.ColumnNames.SalaryScaleCode);
			}

			set
			{
				base.SetSystemString(SalaryScaleMetadata.ColumnNames.SalaryScaleCode, value);
			}
		}
		/// <summary>
		/// Maps to SalaryScale.SalaryScaleName
		/// </summary>
		virtual public System.String SalaryScaleName
		{
			get
			{
				return base.GetSystemString(SalaryScaleMetadata.ColumnNames.SalaryScaleName);
			}

			set
			{
				base.SetSystemString(SalaryScaleMetadata.ColumnNames.SalaryScaleName, value);
			}
		}
		/// <summary>
		/// Maps to SalaryScale.PositionGradeID
		/// </summary>
		virtual public System.Int32? PositionGradeID
		{
			get
			{
				return base.GetSystemInt32(SalaryScaleMetadata.ColumnNames.PositionGradeID);
			}

			set
			{
				base.SetSystemInt32(SalaryScaleMetadata.ColumnNames.PositionGradeID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryScale.SREmploymentType
		/// </summary>
		virtual public System.String SREmploymentType
		{
			get
			{
				return base.GetSystemString(SalaryScaleMetadata.ColumnNames.SREmploymentType);
			}

			set
			{
				base.SetSystemString(SalaryScaleMetadata.ColumnNames.SREmploymentType, value);
			}
		}
		/// <summary>
		/// Maps to SalaryScale.SRProfessionGroup
		/// </summary>
		virtual public System.String SRProfessionGroup
		{
			get
			{
				return base.GetSystemString(SalaryScaleMetadata.ColumnNames.SRProfessionGroup);
			}

			set
			{
				base.SetSystemString(SalaryScaleMetadata.ColumnNames.SRProfessionGroup, value);
			}
		}
		/// <summary>
		/// Maps to SalaryScale.SREducationGroup
		/// </summary>
		virtual public System.String SREducationGroup
		{
			get
			{
				return base.GetSystemString(SalaryScaleMetadata.ColumnNames.SREducationGroup);
			}

			set
			{
				base.SetSystemString(SalaryScaleMetadata.ColumnNames.SREducationGroup, value);
			}
		}
		/// <summary>
		/// Maps to SalaryScale.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(SalaryScaleMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(SalaryScaleMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to SalaryScale.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(SalaryScaleMetadata.ColumnNames.IsActive);
			}

			set
			{
				base.SetSystemBoolean(SalaryScaleMetadata.ColumnNames.IsActive, value);
			}
		}
		/// <summary>
		/// Maps to SalaryScale.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SalaryScaleMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SalaryScaleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SalaryScale.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SalaryScaleMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SalaryScaleMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSalaryScale entity)
			{
				this.entity = entity;
			}
			public System.String SalaryScaleID
			{
				get
				{
					System.Int32? data = entity.SalaryScaleID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryScaleID = null;
					else entity.SalaryScaleID = Convert.ToInt32(value);
				}
			}
			public System.String SalaryScaleCode
			{
				get
				{
					System.String data = entity.SalaryScaleCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryScaleCode = null;
					else entity.SalaryScaleCode = Convert.ToString(value);
				}
			}
			public System.String SalaryScaleName
			{
				get
				{
					System.String data = entity.SalaryScaleName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryScaleName = null;
					else entity.SalaryScaleName = Convert.ToString(value);
				}
			}
			public System.String PositionGradeID
			{
				get
				{
					System.Int32? data = entity.PositionGradeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PositionGradeID = null;
					else entity.PositionGradeID = Convert.ToInt32(value);
				}
			}
			public System.String SREmploymentType
			{
				get
				{
					System.String data = entity.SREmploymentType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREmploymentType = null;
					else entity.SREmploymentType = Convert.ToString(value);
				}
			}
			public System.String SRProfessionGroup
			{
				get
				{
					System.String data = entity.SRProfessionGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRProfessionGroup = null;
					else entity.SRProfessionGroup = Convert.ToString(value);
				}
			}
			public System.String SREducationGroup
			{
				get
				{
					System.String data = entity.SREducationGroup;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SREducationGroup = null;
					else entity.SREducationGroup = Convert.ToString(value);
				}
			}
			public System.String Notes
			{
				get
				{
					System.String data = entity.Notes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Notes = null;
					else entity.Notes = Convert.ToString(value);
				}
			}
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			private esSalaryScale entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSalaryScaleQuery query)
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
				throw new Exception("esSalaryScale can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SalaryScale : esSalaryScale
	{
	}

	[Serializable]
	abstract public class esSalaryScaleQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SalaryScaleMetadata.Meta();
			}
		}

		public esQueryItem SalaryScaleID
		{
			get
			{
				return new esQueryItem(this, SalaryScaleMetadata.ColumnNames.SalaryScaleID, esSystemType.Int32);
			}
		}

		public esQueryItem SalaryScaleCode
		{
			get
			{
				return new esQueryItem(this, SalaryScaleMetadata.ColumnNames.SalaryScaleCode, esSystemType.String);
			}
		}

		public esQueryItem SalaryScaleName
		{
			get
			{
				return new esQueryItem(this, SalaryScaleMetadata.ColumnNames.SalaryScaleName, esSystemType.String);
			}
		}

		public esQueryItem PositionGradeID
		{
			get
			{
				return new esQueryItem(this, SalaryScaleMetadata.ColumnNames.PositionGradeID, esSystemType.Int32);
			}
		}

		public esQueryItem SREmploymentType
		{
			get
			{
				return new esQueryItem(this, SalaryScaleMetadata.ColumnNames.SREmploymentType, esSystemType.String);
			}
		}

		public esQueryItem SRProfessionGroup
		{
			get
			{
				return new esQueryItem(this, SalaryScaleMetadata.ColumnNames.SRProfessionGroup, esSystemType.String);
			}
		}

		public esQueryItem SREducationGroup
		{
			get
			{
				return new esQueryItem(this, SalaryScaleMetadata.ColumnNames.SREducationGroup, esSystemType.String);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, SalaryScaleMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, SalaryScaleMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SalaryScaleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SalaryScaleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SalaryScaleCollection")]
	public partial class SalaryScaleCollection : esSalaryScaleCollection, IEnumerable<SalaryScale>
	{
		public SalaryScaleCollection()
		{

		}

		public static implicit operator List<SalaryScale>(SalaryScaleCollection coll)
		{
			List<SalaryScale> list = new List<SalaryScale>();

			foreach (SalaryScale emp in coll)
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
				return SalaryScaleMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SalaryScaleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SalaryScale(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SalaryScale();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SalaryScaleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SalaryScaleQuery();
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
		public bool Load(SalaryScaleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SalaryScale AddNew()
		{
			SalaryScale entity = base.AddNewEntity() as SalaryScale;

			return entity;
		}
		public SalaryScale FindByPrimaryKey(Int32 salaryScaleID)
		{
			return base.FindByPrimaryKey(salaryScaleID) as SalaryScale;
		}

		#region IEnumerable< SalaryScale> Members

		IEnumerator<SalaryScale> IEnumerable<SalaryScale>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SalaryScale;
			}
		}

		#endregion

		private SalaryScaleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SalaryScale' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SalaryScale ({SalaryScaleID})")]
	[Serializable]
	public partial class SalaryScale : esSalaryScale
	{
		public SalaryScale()
		{
		}

		public SalaryScale(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SalaryScaleMetadata.Meta();
			}
		}

		override protected esSalaryScaleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SalaryScaleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SalaryScaleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SalaryScaleQuery();
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
		public bool Load(SalaryScaleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SalaryScaleQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SalaryScaleQuery : esSalaryScaleQuery
	{
		public SalaryScaleQuery()
		{

		}

		public SalaryScaleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SalaryScaleQuery";
		}
	}

	[Serializable]
	public partial class SalaryScaleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SalaryScaleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SalaryScaleMetadata.ColumnNames.SalaryScaleID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryScaleMetadata.PropertyNames.SalaryScaleID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryScaleMetadata.ColumnNames.SalaryScaleCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryScaleMetadata.PropertyNames.SalaryScaleCode;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryScaleMetadata.ColumnNames.SalaryScaleName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryScaleMetadata.PropertyNames.SalaryScaleName;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryScaleMetadata.ColumnNames.PositionGradeID, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryScaleMetadata.PropertyNames.PositionGradeID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryScaleMetadata.ColumnNames.SREmploymentType, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryScaleMetadata.PropertyNames.SREmploymentType;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryScaleMetadata.ColumnNames.SRProfessionGroup, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryScaleMetadata.PropertyNames.SRProfessionGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryScaleMetadata.ColumnNames.SREducationGroup, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryScaleMetadata.PropertyNames.SREducationGroup;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryScaleMetadata.ColumnNames.Notes, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryScaleMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 255;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryScaleMetadata.ColumnNames.IsActive, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SalaryScaleMetadata.PropertyNames.IsActive;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryScaleMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SalaryScaleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryScaleMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryScaleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SalaryScaleMetadata Meta()
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
			public const string SalaryScaleID = "SalaryScaleID";
			public const string SalaryScaleCode = "SalaryScaleCode";
			public const string SalaryScaleName = "SalaryScaleName";
			public const string PositionGradeID = "PositionGradeID";
			public const string SREmploymentType = "SREmploymentType";
			public const string SRProfessionGroup = "SRProfessionGroup";
			public const string SREducationGroup = "SREducationGroup";
			public const string Notes = "Notes";
			public const string IsActive = "IsActive";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SalaryScaleID = "SalaryScaleID";
			public const string SalaryScaleCode = "SalaryScaleCode";
			public const string SalaryScaleName = "SalaryScaleName";
			public const string PositionGradeID = "PositionGradeID";
			public const string SREmploymentType = "SREmploymentType";
			public const string SRProfessionGroup = "SRProfessionGroup";
			public const string SREducationGroup = "SREducationGroup";
			public const string Notes = "Notes";
			public const string IsActive = "IsActive";
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
			lock (typeof(SalaryScaleMetadata))
			{
				if (SalaryScaleMetadata.mapDelegates == null)
				{
					SalaryScaleMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SalaryScaleMetadata.meta == null)
				{
					SalaryScaleMetadata.meta = new SalaryScaleMetadata();
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

				meta.AddTypeMap("SalaryScaleID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SalaryScaleCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SalaryScaleName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PositionGradeID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SREmploymentType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SRProfessionGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SREducationGroup", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "SalaryScale";
				meta.Destination = "SalaryScale";
				meta.spInsert = "proc_SalaryScaleInsert";
				meta.spUpdate = "proc_SalaryScaleUpdate";
				meta.spDelete = "proc_SalaryScaleDelete";
				meta.spLoadAll = "proc_SalaryScaleLoadAll";
				meta.spLoadByPrimaryKey = "proc_SalaryScaleLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SalaryScaleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
