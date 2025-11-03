/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 2/3/2021 2:13:58 PM
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
	abstract public class esSanitationMaintenanceActivityScheduleCollection : esEntityCollectionWAuditLog
	{
		public esSanitationMaintenanceActivityScheduleCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SanitationMaintenanceActivityScheduleCollection";
		}

		#region Query Logic
		protected void InitQuery(esSanitationMaintenanceActivityScheduleQuery query)
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
			this.InitQuery(query as esSanitationMaintenanceActivityScheduleQuery);
		}
		#endregion

		virtual public SanitationMaintenanceActivitySchedule DetachEntity(SanitationMaintenanceActivitySchedule entity)
		{
			return base.DetachEntity(entity) as SanitationMaintenanceActivitySchedule;
		}

		virtual public SanitationMaintenanceActivitySchedule AttachEntity(SanitationMaintenanceActivitySchedule entity)
		{
			return base.AttachEntity(entity) as SanitationMaintenanceActivitySchedule;
		}

		virtual public void Combine(SanitationMaintenanceActivityScheduleCollection collection)
		{
			base.Combine(collection);
		}

		new public SanitationMaintenanceActivitySchedule this[int index]
		{
			get
			{
				return base[index] as SanitationMaintenanceActivitySchedule;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SanitationMaintenanceActivitySchedule);
		}
	}

	[Serializable]
	abstract public class esSanitationMaintenanceActivitySchedule : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSanitationMaintenanceActivityScheduleQuery GetDynamicQuery()
		{
			return null;
		}

		public esSanitationMaintenanceActivitySchedule()
		{
		}

		public esSanitationMaintenanceActivitySchedule(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String sRWorkTradeItem, String serviceUnitID, DateTime scheduleDate)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRWorkTradeItem, serviceUnitID, scheduleDate);
			else
				return LoadByPrimaryKeyStoredProcedure(sRWorkTradeItem, serviceUnitID, scheduleDate);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRWorkTradeItem, String serviceUnitID, DateTime scheduleDate)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRWorkTradeItem, serviceUnitID, scheduleDate);
			else
				return LoadByPrimaryKeyStoredProcedure(sRWorkTradeItem, serviceUnitID, scheduleDate);
		}

		private bool LoadByPrimaryKeyDynamic(String sRWorkTradeItem, String serviceUnitID, DateTime scheduleDate)
		{
			esSanitationMaintenanceActivityScheduleQuery query = this.GetDynamicQuery();
			query.Where(query.SRWorkTradeItem == sRWorkTradeItem, query.ServiceUnitID == serviceUnitID, query.ScheduleDate == scheduleDate);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String sRWorkTradeItem, String serviceUnitID, DateTime scheduleDate)
		{
			esParameters parms = new esParameters();
			parms.Add("SRWorkTradeItem", sRWorkTradeItem);
			parms.Add("ServiceUnitID", serviceUnitID);
			parms.Add("ScheduleDate", scheduleDate);
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
						case "ScheduleDate": this.str.ScheduleDate = (string)value; break;
						case "PeriodYear": this.str.PeriodYear = (string)value; break;
						case "PeriodDate": this.str.PeriodDate = (string)value; break;
						case "IsProcessed": this.str.IsProcessed = (string)value; break;
						case "IsVoid": this.str.IsVoid = (string)value; break;
						case "VoidDateTime": this.str.VoidDateTime = (string)value; break;
						case "VoidByUserID": this.str.VoidByUserID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ScheduleDate":

							if (value == null || value is System.DateTime)
								this.ScheduleDate = (System.DateTime?)value;
							break;
						case "PeriodDate":

							if (value == null || value is System.DateTime)
								this.PeriodDate = (System.DateTime?)value;
							break;
						case "IsProcessed":

							if (value == null || value is System.Boolean)
								this.IsProcessed = (System.Boolean?)value;
							break;
						case "IsVoid":

							if (value == null || value is System.Boolean)
								this.IsVoid = (System.Boolean?)value;
							break;
						case "VoidDateTime":

							if (value == null || value is System.DateTime)
								this.VoidDateTime = (System.DateTime?)value;
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
		/// Maps to SanitationMaintenanceActivitySchedule.SRWorkTradeItem
		/// </summary>
		virtual public System.String SRWorkTradeItem
		{
			get
			{
				return base.GetSystemString(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.SRWorkTradeItem);
			}

			set
			{
				base.SetSystemString(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.SRWorkTradeItem, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivitySchedule.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivitySchedule.ScheduleDate
		/// </summary>
		virtual public System.DateTime? ScheduleDate
		{
			get
			{
				return base.GetSystemDateTime(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.ScheduleDate);
			}

			set
			{
				base.SetSystemDateTime(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.ScheduleDate, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivitySchedule.PeriodYear
		/// </summary>
		virtual public System.String PeriodYear
		{
			get
			{
				return base.GetSystemString(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.PeriodYear);
			}

			set
			{
				base.SetSystemString(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.PeriodYear, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivitySchedule.PeriodDate
		/// </summary>
		virtual public System.DateTime? PeriodDate
		{
			get
			{
				return base.GetSystemDateTime(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.PeriodDate);
			}

			set
			{
				base.SetSystemDateTime(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.PeriodDate, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivitySchedule.IsProcessed
		/// </summary>
		virtual public System.Boolean? IsProcessed
		{
			get
			{
				return base.GetSystemBoolean(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.IsProcessed);
			}

			set
			{
				base.SetSystemBoolean(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.IsProcessed, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivitySchedule.IsVoid
		/// </summary>
		virtual public System.Boolean? IsVoid
		{
			get
			{
				return base.GetSystemBoolean(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.IsVoid);
			}

			set
			{
				base.SetSystemBoolean(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.IsVoid, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivitySchedule.VoidDateTime
		/// </summary>
		virtual public System.DateTime? VoidDateTime
		{
			get
			{
				return base.GetSystemDateTime(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.VoidDateTime);
			}

			set
			{
				base.SetSystemDateTime(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.VoidDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivitySchedule.VoidByUserID
		/// </summary>
		virtual public System.String VoidByUserID
		{
			get
			{
				return base.GetSystemString(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.VoidByUserID);
			}

			set
			{
				base.SetSystemString(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.VoidByUserID, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivitySchedule.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SanitationMaintenanceActivitySchedule.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSanitationMaintenanceActivitySchedule entity)
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
			public System.String ScheduleDate
			{
				get
				{
					System.DateTime? data = entity.ScheduleDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ScheduleDate = null;
					else entity.ScheduleDate = Convert.ToDateTime(value);
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
			public System.String IsProcessed
			{
				get
				{
					System.Boolean? data = entity.IsProcessed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsProcessed = null;
					else entity.IsProcessed = Convert.ToBoolean(value);
				}
			}
			public System.String IsVoid
			{
				get
				{
					System.Boolean? data = entity.IsVoid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVoid = null;
					else entity.IsVoid = Convert.ToBoolean(value);
				}
			}
			public System.String VoidDateTime
			{
				get
				{
					System.DateTime? data = entity.VoidDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidDateTime = null;
					else entity.VoidDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String VoidByUserID
			{
				get
				{
					System.String data = entity.VoidByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.VoidByUserID = null;
					else entity.VoidByUserID = Convert.ToString(value);
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
			private esSanitationMaintenanceActivitySchedule entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSanitationMaintenanceActivityScheduleQuery query)
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
				throw new Exception("esSanitationMaintenanceActivitySchedule can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SanitationMaintenanceActivitySchedule : esSanitationMaintenanceActivitySchedule
	{
	}

	[Serializable]
	abstract public class esSanitationMaintenanceActivityScheduleQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SanitationMaintenanceActivityScheduleMetadata.Meta();
			}
		}

		public esQueryItem SRWorkTradeItem
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityScheduleMetadata.ColumnNames.SRWorkTradeItem, esSystemType.String);
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityScheduleMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ScheduleDate
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityScheduleMetadata.ColumnNames.ScheduleDate, esSystemType.DateTime);
			}
		}

		public esQueryItem PeriodYear
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityScheduleMetadata.ColumnNames.PeriodYear, esSystemType.String);
			}
		}

		public esQueryItem PeriodDate
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityScheduleMetadata.ColumnNames.PeriodDate, esSystemType.DateTime);
			}
		}

		public esQueryItem IsProcessed
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityScheduleMetadata.ColumnNames.IsProcessed, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVoid
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityScheduleMetadata.ColumnNames.IsVoid, esSystemType.Boolean);
			}
		}

		public esQueryItem VoidDateTime
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityScheduleMetadata.ColumnNames.VoidDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem VoidByUserID
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityScheduleMetadata.ColumnNames.VoidByUserID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityScheduleMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SanitationMaintenanceActivityScheduleMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SanitationMaintenanceActivityScheduleCollection")]
	public partial class SanitationMaintenanceActivityScheduleCollection : esSanitationMaintenanceActivityScheduleCollection, IEnumerable<SanitationMaintenanceActivitySchedule>
	{
		public SanitationMaintenanceActivityScheduleCollection()
		{

		}

		public static implicit operator List<SanitationMaintenanceActivitySchedule>(SanitationMaintenanceActivityScheduleCollection coll)
		{
			List<SanitationMaintenanceActivitySchedule> list = new List<SanitationMaintenanceActivitySchedule>();

			foreach (SanitationMaintenanceActivitySchedule emp in coll)
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
				return SanitationMaintenanceActivityScheduleMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationMaintenanceActivityScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SanitationMaintenanceActivitySchedule(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SanitationMaintenanceActivitySchedule();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SanitationMaintenanceActivityScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationMaintenanceActivityScheduleQuery();
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
		public bool Load(SanitationMaintenanceActivityScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SanitationMaintenanceActivitySchedule AddNew()
		{
			SanitationMaintenanceActivitySchedule entity = base.AddNewEntity() as SanitationMaintenanceActivitySchedule;

			return entity;
		}
		public SanitationMaintenanceActivitySchedule FindByPrimaryKey(String sRWorkTradeItem, String serviceUnitID, DateTime scheduleDate)
		{
			return base.FindByPrimaryKey(sRWorkTradeItem, serviceUnitID, scheduleDate) as SanitationMaintenanceActivitySchedule;
		}

		#region IEnumerable< SanitationMaintenanceActivitySchedule> Members

		IEnumerator<SanitationMaintenanceActivitySchedule> IEnumerable<SanitationMaintenanceActivitySchedule>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SanitationMaintenanceActivitySchedule;
			}
		}

		#endregion

		private SanitationMaintenanceActivityScheduleQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SanitationMaintenanceActivitySchedule' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SanitationMaintenanceActivitySchedule ({SRWorkTradeItem, ServiceUnitID, ScheduleDate})")]
	[Serializable]
	public partial class SanitationMaintenanceActivitySchedule : esSanitationMaintenanceActivitySchedule
	{
		public SanitationMaintenanceActivitySchedule()
		{
		}

		public SanitationMaintenanceActivitySchedule(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SanitationMaintenanceActivityScheduleMetadata.Meta();
			}
		}

		override protected esSanitationMaintenanceActivityScheduleQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationMaintenanceActivityScheduleQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SanitationMaintenanceActivityScheduleQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationMaintenanceActivityScheduleQuery();
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
		public bool Load(SanitationMaintenanceActivityScheduleQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SanitationMaintenanceActivityScheduleQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SanitationMaintenanceActivityScheduleQuery : esSanitationMaintenanceActivityScheduleQuery
	{
		public SanitationMaintenanceActivityScheduleQuery()
		{

		}

		public SanitationMaintenanceActivityScheduleQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SanitationMaintenanceActivityScheduleQuery";
		}
	}

	[Serializable]
	public partial class SanitationMaintenanceActivityScheduleMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SanitationMaintenanceActivityScheduleMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.SRWorkTradeItem, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationMaintenanceActivityScheduleMetadata.PropertyNames.SRWorkTradeItem;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationMaintenanceActivityScheduleMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.ScheduleDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationMaintenanceActivityScheduleMetadata.PropertyNames.ScheduleDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.PeriodYear, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationMaintenanceActivityScheduleMetadata.PropertyNames.PeriodYear;
			c.CharacterMaxLength = 4;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.PeriodDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationMaintenanceActivityScheduleMetadata.PropertyNames.PeriodDate;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.IsProcessed, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SanitationMaintenanceActivityScheduleMetadata.PropertyNames.IsProcessed;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.IsVoid, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = SanitationMaintenanceActivityScheduleMetadata.PropertyNames.IsVoid;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.VoidDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationMaintenanceActivityScheduleMetadata.PropertyNames.VoidDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.VoidByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationMaintenanceActivityScheduleMetadata.PropertyNames.VoidByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationMaintenanceActivityScheduleMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationMaintenanceActivityScheduleMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationMaintenanceActivityScheduleMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SanitationMaintenanceActivityScheduleMetadata Meta()
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
			public const string ScheduleDate = "ScheduleDate";
			public const string PeriodYear = "PeriodYear";
			public const string PeriodDate = "PeriodDate";
			public const string IsProcessed = "IsProcessed";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SRWorkTradeItem = "SRWorkTradeItem";
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ScheduleDate = "ScheduleDate";
			public const string PeriodYear = "PeriodYear";
			public const string PeriodDate = "PeriodDate";
			public const string IsProcessed = "IsProcessed";
			public const string IsVoid = "IsVoid";
			public const string VoidDateTime = "VoidDateTime";
			public const string VoidByUserID = "VoidByUserID";
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
			lock (typeof(SanitationMaintenanceActivityScheduleMetadata))
			{
				if (SanitationMaintenanceActivityScheduleMetadata.mapDelegates == null)
				{
					SanitationMaintenanceActivityScheduleMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SanitationMaintenanceActivityScheduleMetadata.meta == null)
				{
					SanitationMaintenanceActivityScheduleMetadata.meta = new SanitationMaintenanceActivityScheduleMetadata();
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
				meta.AddTypeMap("ScheduleDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("PeriodYear", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PeriodDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsProcessed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVoid", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("VoidDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("VoidByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "SanitationMaintenanceActivitySchedule";
				meta.Destination = "SanitationMaintenanceActivitySchedule";
				meta.spInsert = "proc_SanitationMaintenanceActivityScheduleInsert";
				meta.spUpdate = "proc_SanitationMaintenanceActivityScheduleUpdate";
				meta.spDelete = "proc_SanitationMaintenanceActivityScheduleDelete";
				meta.spLoadAll = "proc_SanitationMaintenanceActivityScheduleLoadAll";
				meta.spLoadByPrimaryKey = "proc_SanitationMaintenanceActivityScheduleLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SanitationMaintenanceActivityScheduleMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
