/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 5/8/2021 2:02:21 PM
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
	abstract public class esEmployeeTaxStatusCollection : esEntityCollectionWAuditLog
	{
		public esEmployeeTaxStatusCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "EmployeeTaxStatusCollection";
		}

		#region Query Logic
		protected void InitQuery(esEmployeeTaxStatusQuery query)
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
			this.InitQuery(query as esEmployeeTaxStatusQuery);
		}
		#endregion

		virtual public EmployeeTaxStatus DetachEntity(EmployeeTaxStatus entity)
		{
			return base.DetachEntity(entity) as EmployeeTaxStatus;
		}

		virtual public EmployeeTaxStatus AttachEntity(EmployeeTaxStatus entity)
		{
			return base.AttachEntity(entity) as EmployeeTaxStatus;
		}

		virtual public void Combine(EmployeeTaxStatusCollection collection)
		{
			base.Combine(collection);
		}

		new public EmployeeTaxStatus this[int index]
		{
			get
			{
				return base[index] as EmployeeTaxStatus;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(EmployeeTaxStatus);
		}
	}

	[Serializable]
	abstract public class esEmployeeTaxStatus : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esEmployeeTaxStatusQuery GetDynamicQuery()
		{
			return null;
		}

		public esEmployeeTaxStatus()
		{
		}

		public esEmployeeTaxStatus(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 personID, Int32 sPTYear)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personID, sPTYear);
			else
				return LoadByPrimaryKeyStoredProcedure(personID, sPTYear);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 personID, Int32 sPTYear)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(personID, sPTYear);
			else
				return LoadByPrimaryKeyStoredProcedure(personID, sPTYear);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 personID, Int32 sPTYear)
		{
			esEmployeeTaxStatusQuery query = this.GetDynamicQuery();
			query.Where(query.PersonID == personID, query.SPTYear == sPTYear);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 personID, Int32 sPTYear)
		{
			esParameters parms = new esParameters();
			parms.Add("PersonID", personID);
			parms.Add("SPTYear", sPTYear);
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
						case "PersonID": this.str.PersonID = (string)value; break;
						case "SPTYear": this.str.SPTYear = (string)value; break;
						case "SRTaxStatus": this.str.SRTaxStatus = (string)value; break;
						case "CreatedDateTime": this.str.CreatedDateTime = (string)value; break;
						case "CreatedByUserID": this.str.CreatedByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PersonID":

							if (value == null || value is System.Int32)
								this.PersonID = (System.Int32?)value;
							break;
						case "SPTYear":

							if (value == null || value is System.Int32)
								this.SPTYear = (System.Int32?)value;
							break;
						case "CreatedDateTime":

							if (value == null || value is System.DateTime)
								this.CreatedDateTime = (System.DateTime?)value;
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
		/// Maps to EmployeeTaxStatus.PersonID
		/// </summary>
		virtual public System.Int32? PersonID
		{
			get
			{
				return base.GetSystemInt32(EmployeeTaxStatusMetadata.ColumnNames.PersonID);
			}

			set
			{
				base.SetSystemInt32(EmployeeTaxStatusMetadata.ColumnNames.PersonID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTaxStatus.SPTYear
		/// </summary>
		virtual public System.Int32? SPTYear
		{
			get
			{
				return base.GetSystemInt32(EmployeeTaxStatusMetadata.ColumnNames.SPTYear);
			}

			set
			{
				base.SetSystemInt32(EmployeeTaxStatusMetadata.ColumnNames.SPTYear, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTaxStatus.SRTaxStatus
		/// </summary>
		virtual public System.String SRTaxStatus
		{
			get
			{
				return base.GetSystemString(EmployeeTaxStatusMetadata.ColumnNames.SRTaxStatus);
			}

			set
			{
				base.SetSystemString(EmployeeTaxStatusMetadata.ColumnNames.SRTaxStatus, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTaxStatus.CreatedDateTime
		/// </summary>
		virtual public System.DateTime? CreatedDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTaxStatusMetadata.ColumnNames.CreatedDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTaxStatusMetadata.ColumnNames.CreatedDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTaxStatus.CreatedByUserID
		/// </summary>
		virtual public System.String CreatedByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeTaxStatusMetadata.ColumnNames.CreatedByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeTaxStatusMetadata.ColumnNames.CreatedByUserID, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTaxStatus.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(EmployeeTaxStatusMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(EmployeeTaxStatusMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to EmployeeTaxStatus.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(EmployeeTaxStatusMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(EmployeeTaxStatusMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esEmployeeTaxStatus entity)
			{
				this.entity = entity;
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
			public System.String SPTYear
			{
				get
				{
					System.Int32? data = entity.SPTYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SPTYear = null;
					else entity.SPTYear = Convert.ToInt32(value);
				}
			}
			public System.String SRTaxStatus
			{
				get
				{
					System.String data = entity.SRTaxStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRTaxStatus = null;
					else entity.SRTaxStatus = Convert.ToString(value);
				}
			}
			public System.String CreatedDateTime
			{
				get
				{
					System.DateTime? data = entity.CreatedDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedDateTime = null;
					else entity.CreatedDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String CreatedByUserID
			{
				get
				{
					System.String data = entity.CreatedByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserID = null;
					else entity.CreatedByUserID = Convert.ToString(value);
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
			private esEmployeeTaxStatus entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esEmployeeTaxStatusQuery query)
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
				throw new Exception("esEmployeeTaxStatus can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class EmployeeTaxStatus : esEmployeeTaxStatus
	{
	}

	[Serializable]
	abstract public class esEmployeeTaxStatusQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTaxStatusMetadata.Meta();
			}
		}

		public esQueryItem PersonID
		{
			get
			{
				return new esQueryItem(this, EmployeeTaxStatusMetadata.ColumnNames.PersonID, esSystemType.Int32);
			}
		}

		public esQueryItem SPTYear
		{
			get
			{
				return new esQueryItem(this, EmployeeTaxStatusMetadata.ColumnNames.SPTYear, esSystemType.Int32);
			}
		}

		public esQueryItem SRTaxStatus
		{
			get
			{
				return new esQueryItem(this, EmployeeTaxStatusMetadata.ColumnNames.SRTaxStatus, esSystemType.String);
			}
		}

		public esQueryItem CreatedDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeTaxStatusMetadata.ColumnNames.CreatedDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem CreatedByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeTaxStatusMetadata.ColumnNames.CreatedByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, EmployeeTaxStatusMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, EmployeeTaxStatusMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("EmployeeTaxStatusCollection")]
	public partial class EmployeeTaxStatusCollection : esEmployeeTaxStatusCollection, IEnumerable<EmployeeTaxStatus>
	{
		public EmployeeTaxStatusCollection()
		{

		}

		public static implicit operator List<EmployeeTaxStatus>(EmployeeTaxStatusCollection coll)
		{
			List<EmployeeTaxStatus> list = new List<EmployeeTaxStatus>();

			foreach (EmployeeTaxStatus emp in coll)
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
				return EmployeeTaxStatusMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTaxStatusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new EmployeeTaxStatus(row);
		}

		override protected esEntity CreateEntity()
		{
			return new EmployeeTaxStatus();
		}

		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTaxStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTaxStatusQuery();
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
		public bool Load(EmployeeTaxStatusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public EmployeeTaxStatus AddNew()
		{
			EmployeeTaxStatus entity = base.AddNewEntity() as EmployeeTaxStatus;

			return entity;
		}
		public EmployeeTaxStatus FindByPrimaryKey(Int32 personID, Int32 sPTYear)
		{
			return base.FindByPrimaryKey(personID, sPTYear) as EmployeeTaxStatus;
		}

		#region IEnumerable< EmployeeTaxStatus> Members

		IEnumerator<EmployeeTaxStatus> IEnumerable<EmployeeTaxStatus>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as EmployeeTaxStatus;
			}
		}

		#endregion

		private EmployeeTaxStatusQuery query;
	}


	/// <summary>
	/// Encapsulates the 'EmployeeTaxStatus' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("EmployeeTaxStatus ({PersonID, SPTYear})")]
	[Serializable]
	public partial class EmployeeTaxStatus : esEmployeeTaxStatus
	{
		public EmployeeTaxStatus()
		{
		}

		public EmployeeTaxStatus(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmployeeTaxStatusMetadata.Meta();
			}
		}

		override protected esEmployeeTaxStatusQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmployeeTaxStatusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public EmployeeTaxStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmployeeTaxStatusQuery();
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
		public bool Load(EmployeeTaxStatusQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private EmployeeTaxStatusQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class EmployeeTaxStatusQuery : esEmployeeTaxStatusQuery
	{
		public EmployeeTaxStatusQuery()
		{

		}

		public EmployeeTaxStatusQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "EmployeeTaxStatusQuery";
		}
	}

	[Serializable]
	public partial class EmployeeTaxStatusMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmployeeTaxStatusMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmployeeTaxStatusMetadata.ColumnNames.PersonID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeTaxStatusMetadata.PropertyNames.PersonID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTaxStatusMetadata.ColumnNames.SPTYear, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = EmployeeTaxStatusMetadata.PropertyNames.SPTYear;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTaxStatusMetadata.ColumnNames.SRTaxStatus, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTaxStatusMetadata.PropertyNames.SRTaxStatus;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTaxStatusMetadata.ColumnNames.CreatedDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTaxStatusMetadata.PropertyNames.CreatedDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTaxStatusMetadata.ColumnNames.CreatedByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTaxStatusMetadata.PropertyNames.CreatedByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTaxStatusMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = EmployeeTaxStatusMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(EmployeeTaxStatusMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = EmployeeTaxStatusMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public EmployeeTaxStatusMetadata Meta()
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
			public const string PersonID = "PersonID";
			public const string SPTYear = "SPTYear";
			public const string SRTaxStatus = "SRTaxStatus";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string PersonID = "PersonID";
			public const string SPTYear = "SPTYear";
			public const string SRTaxStatus = "SRTaxStatus";
			public const string CreatedDateTime = "CreatedDateTime";
			public const string CreatedByUserID = "CreatedByUserID";
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
			lock (typeof(EmployeeTaxStatusMetadata))
			{
				if (EmployeeTaxStatusMetadata.mapDelegates == null)
				{
					EmployeeTaxStatusMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (EmployeeTaxStatusMetadata.meta == null)
				{
					EmployeeTaxStatusMetadata.meta = new EmployeeTaxStatusMetadata();
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

				meta.AddTypeMap("PersonID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SPTYear", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SRTaxStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "EmployeeTaxStatus";
				meta.Destination = "EmployeeTaxStatus";
				meta.spInsert = "proc_EmployeeTaxStatusInsert";
				meta.spUpdate = "proc_EmployeeTaxStatusUpdate";
				meta.spDelete = "proc_EmployeeTaxStatusDelete";
				meta.spLoadAll = "proc_EmployeeTaxStatusLoadAll";
				meta.spLoadByPrimaryKey = "proc_EmployeeTaxStatusLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmployeeTaxStatusMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
