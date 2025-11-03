/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/3/2021 9:59:56 AM
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
	abstract public class esSanitationMaintenanceActivitySchedulePeriodDateCollection : esEntityCollectionWAuditLog
	{
		public esSanitationMaintenanceActivitySchedulePeriodDateCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SanitationMaintenanceActivitySchedulePeriodDateCollection";
		}

		#region Query Logic
		protected void InitQuery(esSanitationMaintenanceActivitySchedulePeriodDateQuery query)
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
			this.InitQuery(query as esSanitationMaintenanceActivitySchedulePeriodDateQuery);
		}
		#endregion

		virtual public SanitationMaintenanceActivitySchedulePeriodDate DetachEntity(SanitationMaintenanceActivitySchedulePeriodDate entity)
		{
			return base.DetachEntity(entity) as SanitationMaintenanceActivitySchedulePeriodDate;
		}

		virtual public SanitationMaintenanceActivitySchedulePeriodDate AttachEntity(SanitationMaintenanceActivitySchedulePeriodDate entity)
		{
			return base.AttachEntity(entity) as SanitationMaintenanceActivitySchedulePeriodDate;
		}

		virtual public void Combine(SanitationMaintenanceActivitySchedulePeriodDateCollection collection)
		{
			base.Combine(collection);
		}

		new public SanitationMaintenanceActivitySchedulePeriodDate this[int index]
		{
			get
			{
				return base[index] as SanitationMaintenanceActivitySchedulePeriodDate;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SanitationMaintenanceActivitySchedulePeriodDate);
		}
	}

	[Serializable]
	abstract public class esSanitationMaintenanceActivitySchedulePeriodDate : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSanitationMaintenanceActivitySchedulePeriodDateQuery GetDynamicQuery()
		{
			return null;
		}

		public esSanitationMaintenanceActivitySchedulePeriodDate()
		{
		}

		public esSanitationMaintenanceActivitySchedulePeriodDate(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String sRWorkTradeItem, String serviceUnitID, String periodYear, DateTime periodDate)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRWorkTradeItem, serviceUnitID, periodYear, periodDate);
			else
				return LoadByPrimaryKeyStoredProcedure(sRWorkTradeItem, serviceUnitID, periodYear, periodDate);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRWorkTradeItem, String serviceUnitID, String periodYear, DateTime periodDate)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRWorkTradeItem, serviceUnitID, periodYear, periodDate);
			else
				return LoadByPrimaryKeyStoredProcedure(sRWorkTradeItem, serviceUnitID, periodYear, periodDate);
		}

		private bool LoadByPrimaryKeyDynamic(String sRWorkTradeItem, String serviceUnitID, String periodYear, DateTime periodDate)
		{
			esSanitationMaintenanceActivitySchedulePeriodDateQuery query = this.GetDynamicQuery();
			query.Where(query.SRWorkTradeItem == sRWorkTradeItem, query.ServiceUnitID == serviceUnitID, query.PeriodYear == periodYear, query.PeriodDate == periodDate);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String sRWorkTradeItem, String serviceUnitID, String periodYear, DateTime periodDate)
		{
			esParameters parms = new esParameters();
			parms.Add("SRWorkTradeItem", sRWorkTradeItem);
			parms.Add("ServiceUnitID", serviceUnitID);
			parms.Add("PeriodYear", periodYear);
			parms.Add("PeriodDate", periodDate);
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
						case "SRWorkTradeItem": this.str.SRWorkTradeItem = (string)value; break;
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "PeriodYear": this.str.PeriodYear = (string)value; break;
						case "PeriodDate": this.str.PeriodDate = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "PeriodDate":

							if (value == null || value is System.DateTime)
								this.PeriodDate = (System.DateTime?)value;
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
		/// Maps to SanitationMaintenanceActivitySchedulePeriodDate.SRWorkTradeItem
		/// </summary>
		virtual public System.String SRWorkTradeItem
		{
			get
			{
				return base.GetSystemString(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.SRWorkTradeItem);
			}

			set
			{
				base.SetSystemString(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.SRWorkTradeItem, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivitySchedulePeriodDate.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivitySchedulePeriodDate.PeriodYear
		/// </summary>
		virtual public System.String PeriodYear
		{
			get
			{
				return base.GetSystemString(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.PeriodYear);
			}

			set
			{
				base.SetSystemString(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.PeriodYear, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivitySchedulePeriodDate.PeriodDate
		/// </summary>
		virtual public System.DateTime? PeriodDate
		{
			get
			{
				return base.GetSystemDateTime(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.PeriodDate);
			}

			set
			{
				base.SetSystemDateTime(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.PeriodDate, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivitySchedulePeriodDate.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivitySchedulePeriodDate.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSanitationMaintenanceActivitySchedulePeriodDate entity)
			{
				this.entity = entity;
			}
			public System.String SRWorkTradeItem
			{
				get
				{
					System.String data = entity.SRWorkTradeItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWorkTradeItem = null;
					else entity.SRWorkTradeItem = Convert.ToString(value);
				}
			}
			public System.String ServiceUnitID
			{
				get
				{
					System.String data = entity.ServiceUnitID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ServiceUnitID = null;
					else entity.ServiceUnitID = Convert.ToString(value);
				}
			}
			public System.String PeriodYear
			{
				get
				{
					System.String data = entity.PeriodYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodYear = null;
					else entity.PeriodYear = Convert.ToString(value);
				}
			}
			public System.String PeriodDate
			{
				get
				{
					System.DateTime? data = entity.PeriodDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodDate = null;
					else entity.PeriodDate = Convert.ToDateTime(value);
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
			private esSanitationMaintenanceActivitySchedulePeriodDate entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSanitationMaintenanceActivitySchedulePeriodDateQuery query)
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
				throw new Exception("esSanitationMaintenanceActivitySchedulePeriodDate can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SanitationMaintenanceActivitySchedulePeriodDate : esSanitationMaintenanceActivitySchedulePeriodDate
	{
	}

	[Serializable]
	abstract public class esSanitationMaintenanceActivitySchedulePeriodDateQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SanitationMaintenanceActivitySchedulePeriodDateMetadata.Meta();
			}
		}

		public esQueryItem SRWorkTradeItem
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.SRWorkTradeItem, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem PeriodYear
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.PeriodYear, esSystemType.String);
			}
		}

		public esQueryItem PeriodDate
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.PeriodDate, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SanitationMaintenanceActivitySchedulePeriodDateCollection")]
	public partial class SanitationMaintenanceActivitySchedulePeriodDateCollection : esSanitationMaintenanceActivitySchedulePeriodDateCollection, IEnumerable<SanitationMaintenanceActivitySchedulePeriodDate>
	{
		public SanitationMaintenanceActivitySchedulePeriodDateCollection()
		{

		}

		public static implicit operator List<SanitationMaintenanceActivitySchedulePeriodDate>(SanitationMaintenanceActivitySchedulePeriodDateCollection coll)
		{
			List<SanitationMaintenanceActivitySchedulePeriodDate> list = new List<SanitationMaintenanceActivitySchedulePeriodDate>();

			foreach (SanitationMaintenanceActivitySchedulePeriodDate emp in coll)
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
				return SanitationMaintenanceActivitySchedulePeriodDateMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationMaintenanceActivitySchedulePeriodDateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SanitationMaintenanceActivitySchedulePeriodDate(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SanitationMaintenanceActivitySchedulePeriodDate();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SanitationMaintenanceActivitySchedulePeriodDateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationMaintenanceActivitySchedulePeriodDateQuery();
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
		public bool Load(SanitationMaintenanceActivitySchedulePeriodDateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SanitationMaintenanceActivitySchedulePeriodDate AddNew()
		{
			SanitationMaintenanceActivitySchedulePeriodDate entity = base.AddNewEntity() as SanitationMaintenanceActivitySchedulePeriodDate;

			return entity;
		}
		public SanitationMaintenanceActivitySchedulePeriodDate FindByPrimaryKey(String sRWorkTradeItem, String serviceUnitID, String periodYear, DateTime periodDate)
		{
			return base.FindByPrimaryKey(sRWorkTradeItem, serviceUnitID, periodYear, periodDate) as SanitationMaintenanceActivitySchedulePeriodDate;
		}

		#region IEnumerable< SanitationMaintenanceActivitySchedulePeriodDate> Members

		IEnumerator<SanitationMaintenanceActivitySchedulePeriodDate> IEnumerable<SanitationMaintenanceActivitySchedulePeriodDate>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SanitationMaintenanceActivitySchedulePeriodDate;
			}
		}

		#endregion

		private SanitationMaintenanceActivitySchedulePeriodDateQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SanitationMaintenanceActivitySchedulePeriodDate' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SanitationMaintenanceActivitySchedulePeriodDate ({SRWorkTradeItem, ServiceUnitID, PeriodYear, PeriodDate})")]
	[Serializable]
	public partial class SanitationMaintenanceActivitySchedulePeriodDate : esSanitationMaintenanceActivitySchedulePeriodDate
	{
		public SanitationMaintenanceActivitySchedulePeriodDate()
		{
		}

		public SanitationMaintenanceActivitySchedulePeriodDate(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SanitationMaintenanceActivitySchedulePeriodDateMetadata.Meta();
			}
		}

		override protected esSanitationMaintenanceActivitySchedulePeriodDateQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationMaintenanceActivitySchedulePeriodDateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SanitationMaintenanceActivitySchedulePeriodDateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationMaintenanceActivitySchedulePeriodDateQuery();
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
		public bool Load(SanitationMaintenanceActivitySchedulePeriodDateQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SanitationMaintenanceActivitySchedulePeriodDateQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SanitationMaintenanceActivitySchedulePeriodDateQuery : esSanitationMaintenanceActivitySchedulePeriodDateQuery
	{
		public SanitationMaintenanceActivitySchedulePeriodDateQuery()
		{

		}

		public SanitationMaintenanceActivitySchedulePeriodDateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SanitationMaintenanceActivitySchedulePeriodDateQuery";
		}
	}

	[Serializable]
	public partial class SanitationMaintenanceActivitySchedulePeriodDateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SanitationMaintenanceActivitySchedulePeriodDateMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.SRWorkTradeItem, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationMaintenanceActivitySchedulePeriodDateMetadata.PropertyNames.SRWorkTradeItem;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationMaintenanceActivitySchedulePeriodDateMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.PeriodYear, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationMaintenanceActivitySchedulePeriodDateMetadata.PropertyNames.PeriodYear;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 4;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.PeriodDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationMaintenanceActivitySchedulePeriodDateMetadata.PropertyNames.PeriodDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationMaintenanceActivitySchedulePeriodDateMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivitySchedulePeriodDateMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationMaintenanceActivitySchedulePeriodDateMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SanitationMaintenanceActivitySchedulePeriodDateMetadata Meta()
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
			public const string SRWorkTradeItem = "SRWorkTradeItem";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string PeriodYear = "PeriodYear";
			public const string PeriodDate = "PeriodDate";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SRWorkTradeItem = "SRWorkTradeItem";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string PeriodYear = "PeriodYear";
			public const string PeriodDate = "PeriodDate";
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
			lock (typeof(SanitationMaintenanceActivitySchedulePeriodDateMetadata))
			{
				if (SanitationMaintenanceActivitySchedulePeriodDateMetadata.mapDelegates == null)
				{
					SanitationMaintenanceActivitySchedulePeriodDateMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SanitationMaintenanceActivitySchedulePeriodDateMetadata.meta == null)
				{
					SanitationMaintenanceActivitySchedulePeriodDateMetadata.meta = new SanitationMaintenanceActivitySchedulePeriodDateMetadata();
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

				meta.AddTypeMap("SRWorkTradeItem", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodYear", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "SanitationMaintenanceActivitySchedulePeriodDate";
				meta.Destination = "SanitationMaintenanceActivitySchedulePeriodDate";
				meta.spInsert = "proc_SanitationMaintenanceActivitySchedulePeriodDateInsert";
				meta.spUpdate = "proc_SanitationMaintenanceActivitySchedulePeriodDateUpdate";
				meta.spDelete = "proc_SanitationMaintenanceActivitySchedulePeriodDateDelete";
				meta.spLoadAll = "proc_SanitationMaintenanceActivitySchedulePeriodDateLoadAll";
				meta.spLoadByPrimaryKey = "proc_SanitationMaintenanceActivitySchedulePeriodDateLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SanitationMaintenanceActivitySchedulePeriodDateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
