/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/16/2020 8:13:13 PM
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
	abstract public class esThrScheduleCollection : esEntityCollectionWAuditLog
	{
		public esThrScheduleCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ThrScheduleCollection";
		}

		#region Query Logic
		protected void InitQuery(esThrScheduleQuery query)
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
			this.InitQuery(query as esThrScheduleQuery);
		}
		#endregion

		virtual public ThrSchedule DetachEntity(ThrSchedule entity)
		{
			return base.DetachEntity(entity) as ThrSchedule;
		}

		virtual public ThrSchedule AttachEntity(ThrSchedule entity)
		{
			return base.AttachEntity(entity) as ThrSchedule;
		}

		virtual public void Combine(ThrScheduleCollection collection)
		{
			base.Combine(collection);
		}

		new public ThrSchedule this[int index]
		{
			get
			{
				return base[index] as ThrSchedule;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ThrSchedule);
		}
	}

	[Serializable]
	abstract public class esThrSchedule : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esThrScheduleQuery GetDynamicQuery()
		{
			return null;
		}

		public esThrSchedule()
		{
		}

		public esThrSchedule(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Int32 counterID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(counterID);
			else
				return LoadByPrimaryKeyStoredProcedure(counterID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Int32 counterID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(counterID);
			else
				return LoadByPrimaryKeyStoredProcedure(counterID);
		}

		private bool LoadByPrimaryKeyDynamic(Int32 counterID)
		{
			esThrScheduleQuery query = this.GetDynamicQuery();
			query.Where(query.CounterID == counterID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Int32 counterID)
		{
			esParameters parms = new esParameters();
			parms.Add("CounterID", counterID);
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
						case "CounterID": this.str.CounterID = (string)value; break;
						case "PayrollPeriodID": this.str.PayrollPeriodID = (string)value; break;
						case "PayrollPeriodName": this.str.PayrollPeriodName = (string)value; break;
						case "PayDate": this.str.PayDate = (string)value; break;
						case "SPTYear": this.str.SPTYear = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "CounterID":

							if (value == null || value is System.Int32)
								this.CounterID = (System.Int32?)value;
							break;
						case "PayrollPeriodID":

							if (value == null || value is System.Int32)
								this.PayrollPeriodID = (System.Int32?)value;
							break;
						case "PayDate":

							if (value == null || value is System.DateTime)
								this.PayDate = (System.DateTime?)value;
							break;
						case "SPTYear":

							if (value == null || value is System.Int32)
								this.SPTYear = (System.Int32?)value;
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
		/// Maps to ThrSchedule.CounterID
		/// </summary>
		virtual public System.Int32? CounterID
		{
			get
			{
				return base.GetSystemInt32(ThrScheduleMetadata.ColumnNames.CounterID);
			}

			set
			{
				base.SetSystemInt32(ThrScheduleMetadata.ColumnNames.CounterID, value);
			}
		}
		/// <summary>
		/// Maps to ThrSchedule.PayrollPeriodID
		/// </summary>
		virtual public System.Int32? PayrollPeriodID
		{
			get
			{
				return base.GetSystemInt32(ThrScheduleMetadata.ColumnNames.PayrollPeriodID);
			}

			set
			{
				base.SetSystemInt32(ThrScheduleMetadata.ColumnNames.PayrollPeriodID, value);
			}
		}
		/// <summary>
		/// Maps to ThrSchedule.PayrollPeriodName
		/// </summary>
		virtual public System.String PayrollPeriodName
		{
			get
			{
				return base.GetSystemString(ThrScheduleMetadata.ColumnNames.PayrollPeriodName);
			}

			set
			{
				base.SetSystemString(ThrScheduleMetadata.ColumnNames.PayrollPeriodName, value);
			}
		}
		/// <summary>
		/// Maps to ThrSchedule.PayDate
		/// </summary>
		virtual public System.DateTime? PayDate
		{
			get
			{
				return base.GetSystemDateTime(ThrScheduleMetadata.ColumnNames.PayDate);
			}

			set
			{
				base.SetSystemDateTime(ThrScheduleMetadata.ColumnNames.PayDate, value);
			}
		}
		/// <summary>
		/// Maps to ThrSchedule.SPTYear
		/// </summary>
		virtual public System.Int32? SPTYear
		{
			get
			{
				return base.GetSystemInt32(ThrScheduleMetadata.ColumnNames.SPTYear);
			}

			set
			{
				base.SetSystemInt32(ThrScheduleMetadata.ColumnNames.SPTYear, value);
			}
		}
		/// <summary>
		/// Maps to ThrSchedule.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ThrScheduleMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ThrScheduleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ThrSchedule.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ThrScheduleMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ThrScheduleMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esThrSchedule entity)
			{
				this.entity = entity;
			}
			public System.String CounterID
			{
				get
				{
					System.Int32? data = entity.CounterID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CounterID = null;
					else entity.CounterID = Convert.ToInt32(value);
				}
			}
			public System.String PayrollPeriodID
			{
				get
				{
					System.Int32? data = entity.PayrollPeriodID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayrollPeriodID = null;
					else entity.PayrollPeriodID = Convert.ToInt32(value);
				}
			}
			public System.String PayrollPeriodName
			{
				get
				{
					System.String data = entity.PayrollPeriodName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayrollPeriodName = null;
					else entity.PayrollPeriodName = Convert.ToString(value);
				}
			}
			public System.String PayDate
			{
				get
				{
					System.DateTime? data = entity.PayDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PayDate = null;
					else entity.PayDate = Convert.ToDateTime(value);
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
			private esThrSchedule entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esThrScheduleQuery query)
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
				throw new Exception("esThrSchedule can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ThrSchedule : esThrSchedule
	{
	}

	[Serializable]
	abstract public class esThrScheduleQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ThrScheduleMetadata.Meta();
			}
		}

		public esQueryItem CounterID
		{
			get
			{
				return new esQueryItem(this, ThrScheduleMetadata.ColumnNames.CounterID, esSystemType.Int32);
			}
		}

		public esQueryItem PayrollPeriodID
		{
			get
			{
				return new esQueryItem(this, ThrScheduleMetadata.ColumnNames.PayrollPeriodID, esSystemType.Int32);
			}
		}

		public esQueryItem PayrollPeriodName
		{
			get
			{
				return new esQueryItem(this, ThrScheduleMetadata.ColumnNames.PayrollPeriodName, esSystemType.String);
			}
		}

		public esQueryItem PayDate
		{
			get
			{
				return new esQueryItem(this, ThrScheduleMetadata.ColumnNames.PayDate, esSystemType.DateTime);
			}
		}

		public esQueryItem SPTYear
		{
			get
			{
				return new esQueryItem(this, ThrScheduleMetadata.ColumnNames.SPTYear, esSystemType.Int32);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ThrScheduleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ThrScheduleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ThrScheduleCollection")]
	public partial class ThrScheduleCollection : esThrScheduleCollection, IEnumerable<ThrSchedule>
	{
		public ThrScheduleCollection()
		{

		}

		public static implicit operator List<ThrSchedule>(ThrScheduleCollection coll)
		{
			List<ThrSchedule> list = new List<ThrSchedule>();

			foreach (ThrSchedule emp in coll)
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
				return ThrScheduleMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ThrScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ThrSchedule(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ThrSchedule();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ThrScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ThrScheduleQuery();
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
		public bool Load(ThrScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ThrSchedule AddNew()
		{
			ThrSchedule entity = base.AddNewEntity() as ThrSchedule;

			return entity;
		}
		public ThrSchedule FindByPrimaryKey(Int32 counterID)
		{
			return base.FindByPrimaryKey(counterID) as ThrSchedule;
		}

		#region IEnumerable< ThrSchedule> Members

		IEnumerator<ThrSchedule> IEnumerable<ThrSchedule>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ThrSchedule;
			}
		}

		#endregion

		private ThrScheduleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ThrSchedule' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ThrSchedule ({CounterID})")]
	[Serializable]
	public partial class ThrSchedule : esThrSchedule
	{
		public ThrSchedule()
		{
		}

		public ThrSchedule(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ThrScheduleMetadata.Meta();
			}
		}

		override protected esThrScheduleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ThrScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ThrScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ThrScheduleQuery();
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
		public bool Load(ThrScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ThrScheduleQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ThrScheduleQuery : esThrScheduleQuery
	{
		public ThrScheduleQuery()
		{

		}

		public ThrScheduleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ThrScheduleQuery";
		}
	}

	[Serializable]
	public partial class ThrScheduleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ThrScheduleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ThrScheduleMetadata.ColumnNames.CounterID, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ThrScheduleMetadata.PropertyNames.CounterID;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ThrScheduleMetadata.ColumnNames.PayrollPeriodID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ThrScheduleMetadata.PropertyNames.PayrollPeriodID;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ThrScheduleMetadata.ColumnNames.PayrollPeriodName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ThrScheduleMetadata.PropertyNames.PayrollPeriodName;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ThrScheduleMetadata.ColumnNames.PayDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ThrScheduleMetadata.PropertyNames.PayDate;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ThrScheduleMetadata.ColumnNames.SPTYear, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ThrScheduleMetadata.PropertyNames.SPTYear;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ThrScheduleMetadata.ColumnNames.LastUpdateDateTime, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ThrScheduleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ThrScheduleMetadata.ColumnNames.LastUpdateByUserID, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ThrScheduleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ThrScheduleMetadata Meta()
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
			public const string CounterID = "CounterID";
			public const string PayrollPeriodID = "PayrollPeriodID";
			public const string PayrollPeriodName = "PayrollPeriodName";
			public const string PayDate = "PayDate";
			public const string SPTYear = "SPTYear";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string CounterID = "CounterID";
			public const string PayrollPeriodID = "PayrollPeriodID";
			public const string PayrollPeriodName = "PayrollPeriodName";
			public const string PayDate = "PayDate";
			public const string SPTYear = "SPTYear";
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
			lock (typeof(ThrScheduleMetadata))
			{
				if (ThrScheduleMetadata.mapDelegates == null)
				{
					ThrScheduleMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ThrScheduleMetadata.meta == null)
				{
					ThrScheduleMetadata.meta = new ThrScheduleMetadata();
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

				meta.AddTypeMap("CounterID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PayrollPeriodID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PayrollPeriodName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PayDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("SPTYear", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ThrSchedule";
				meta.Destination = "ThrSchedule";
				meta.spInsert = "proc_ThrScheduleInsert";
				meta.spUpdate = "proc_ThrScheduleUpdate";
				meta.spDelete = "proc_ThrScheduleDelete";
				meta.spLoadAll = "proc_ThrScheduleLoadAll";
				meta.spLoadByPrimaryKey = "proc_ThrScheduleLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ThrScheduleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
