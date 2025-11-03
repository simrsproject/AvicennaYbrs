/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/2/2021 7:33:58 PM
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
	abstract public class esParamedicScheduleDateItemCollection : esEntityCollectionWAuditLog
	{
		public esParamedicScheduleDateItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ParamedicScheduleDateItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esParamedicScheduleDateItemQuery query)
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
			this.InitQuery(query as esParamedicScheduleDateItemQuery);
		}
		#endregion

		virtual public ParamedicScheduleDateItem DetachEntity(ParamedicScheduleDateItem entity)
		{
			return base.DetachEntity(entity) as ParamedicScheduleDateItem;
		}

		virtual public ParamedicScheduleDateItem AttachEntity(ParamedicScheduleDateItem entity)
		{
			return base.AttachEntity(entity) as ParamedicScheduleDateItem;
		}

		virtual public void Combine(ParamedicScheduleDateItemCollection collection)
		{
			base.Combine(collection);
		}

		new public ParamedicScheduleDateItem this[int index]
		{
			get
			{
				return base[index] as ParamedicScheduleDateItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ParamedicScheduleDateItem);
		}
	}

	[Serializable]
	abstract public class esParamedicScheduleDateItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esParamedicScheduleDateItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esParamedicScheduleDateItem()
		{
		}

		public esParamedicScheduleDateItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String serviceUnitID, String paramedicID, DateTime scheduleDate, String operationalTimeID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, paramedicID, scheduleDate, operationalTimeID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, paramedicID, scheduleDate, operationalTimeID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String serviceUnitID, String paramedicID, DateTime scheduleDate, String operationalTimeID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, paramedicID, scheduleDate, operationalTimeID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, paramedicID, scheduleDate, operationalTimeID);
		}

		private bool LoadByPrimaryKeyDynamic(String serviceUnitID, String paramedicID, DateTime scheduleDate, String operationalTimeID)
		{
			esParamedicScheduleDateItemQuery query = this.GetDynamicQuery();
			query.Where(query.ServiceUnitID == serviceUnitID, query.ParamedicID == paramedicID, query.ScheduleDate == scheduleDate, query.OperationalTimeID == operationalTimeID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String serviceUnitID, String paramedicID, DateTime scheduleDate, String operationalTimeID)
		{
			esParameters parms = new esParameters();
			parms.Add("ServiceUnitID", serviceUnitID);
			parms.Add("ParamedicID", paramedicID);
			parms.Add("ScheduleDate", scheduleDate);
			parms.Add("OperationalTimeID", operationalTimeID);
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
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;
						case "ParamedicID": this.str.ParamedicID = (string)value; break;
						case "ScheduleDate": this.str.ScheduleDate = (string)value; break;
						case "OperationalTimeID": this.str.OperationalTimeID = (string)value; break;
						case "IsIpr": this.str.IsIpr = (string)value; break;
						case "IsOpr": this.str.IsOpr = (string)value; break;
						case "IsEmr": this.str.IsEmr = (string)value; break;
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
						case "IsIpr":

							if (value == null || value is System.Boolean)
								this.IsIpr = (System.Boolean?)value;
							break;
						case "IsOpr":

							if (value == null || value is System.Boolean)
								this.IsOpr = (System.Boolean?)value;
							break;
						case "IsEmr":

							if (value == null || value is System.Boolean)
								this.IsEmr = (System.Boolean?)value;
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
		/// Maps to ParamedicScheduleDateItem.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateItemMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateItemMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDateItem.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateItemMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateItemMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDateItem.ScheduleDate
		/// </summary>
		virtual public System.DateTime? ScheduleDate
		{
			get
			{
				return base.GetSystemDateTime(ParamedicScheduleDateItemMetadata.ColumnNames.ScheduleDate);
			}

			set
			{
				base.SetSystemDateTime(ParamedicScheduleDateItemMetadata.ColumnNames.ScheduleDate, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDateItem.OperationalTimeID
		/// </summary>
		virtual public System.String OperationalTimeID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateItemMetadata.ColumnNames.OperationalTimeID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateItemMetadata.ColumnNames.OperationalTimeID, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDateItem.IsIpr
		/// </summary>
		virtual public System.Boolean? IsIpr
		{
			get
			{
				return base.GetSystemBoolean(ParamedicScheduleDateItemMetadata.ColumnNames.IsIpr);
			}

			set
			{
				base.SetSystemBoolean(ParamedicScheduleDateItemMetadata.ColumnNames.IsIpr, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDateItem.IsOpr
		/// </summary>
		virtual public System.Boolean? IsOpr
		{
			get
			{
				return base.GetSystemBoolean(ParamedicScheduleDateItemMetadata.ColumnNames.IsOpr);
			}

			set
			{
				base.SetSystemBoolean(ParamedicScheduleDateItemMetadata.ColumnNames.IsOpr, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDateItem.IsEmr
		/// </summary>
		virtual public System.Boolean? IsEmr
		{
			get
			{
				return base.GetSystemBoolean(ParamedicScheduleDateItemMetadata.ColumnNames.IsEmr);
			}

			set
			{
				base.SetSystemBoolean(ParamedicScheduleDateItemMetadata.ColumnNames.IsEmr, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDateItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ParamedicScheduleDateItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ParamedicScheduleDateItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ParamedicScheduleDateItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ParamedicScheduleDateItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ParamedicScheduleDateItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esParamedicScheduleDateItem entity)
			{
				this.entity = entity;
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
			public System.String ParamedicID
			{
				get
				{
					System.String data = entity.ParamedicID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParamedicID = null;
					else entity.ParamedicID = Convert.ToString(value);
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
			public System.String OperationalTimeID
			{
				get
				{
					System.String data = entity.OperationalTimeID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OperationalTimeID = null;
					else entity.OperationalTimeID = Convert.ToString(value);
				}
			}
			public System.String IsIpr
			{
				get
				{
					System.Boolean? data = entity.IsIpr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsIpr = null;
					else entity.IsIpr = Convert.ToBoolean(value);
				}
			}
			public System.String IsOpr
			{
				get
				{
					System.Boolean? data = entity.IsOpr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsOpr = null;
					else entity.IsOpr = Convert.ToBoolean(value);
				}
			}
			public System.String IsEmr
			{
				get
				{
					System.Boolean? data = entity.IsEmr;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEmr = null;
					else entity.IsEmr = Convert.ToBoolean(value);
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
			private esParamedicScheduleDateItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esParamedicScheduleDateItemQuery query)
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
				throw new Exception("esParamedicScheduleDateItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ParamedicScheduleDateItem : esParamedicScheduleDateItem
	{
	}

	[Serializable]
	abstract public class esParamedicScheduleDateItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ParamedicScheduleDateItemMetadata.Meta();
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateItemMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateItemMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem ScheduleDate
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateItemMetadata.ColumnNames.ScheduleDate, esSystemType.DateTime);
			}
		}

		public esQueryItem OperationalTimeID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateItemMetadata.ColumnNames.OperationalTimeID, esSystemType.String);
			}
		}

		public esQueryItem IsIpr
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateItemMetadata.ColumnNames.IsIpr, esSystemType.Boolean);
			}
		}

		public esQueryItem IsOpr
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateItemMetadata.ColumnNames.IsOpr, esSystemType.Boolean);
			}
		}

		public esQueryItem IsEmr
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateItemMetadata.ColumnNames.IsEmr, esSystemType.Boolean);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ParamedicScheduleDateItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ParamedicScheduleDateItemCollection")]
	public partial class ParamedicScheduleDateItemCollection : esParamedicScheduleDateItemCollection, IEnumerable<ParamedicScheduleDateItem>
	{
		public ParamedicScheduleDateItemCollection()
		{

		}

		public static implicit operator List<ParamedicScheduleDateItem>(ParamedicScheduleDateItemCollection coll)
		{
			List<ParamedicScheduleDateItem> list = new List<ParamedicScheduleDateItem>();

			foreach (ParamedicScheduleDateItem emp in coll)
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
				return ParamedicScheduleDateItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicScheduleDateItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ParamedicScheduleDateItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ParamedicScheduleDateItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ParamedicScheduleDateItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicScheduleDateItemQuery();
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
		public bool Load(ParamedicScheduleDateItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ParamedicScheduleDateItem AddNew()
		{
			ParamedicScheduleDateItem entity = base.AddNewEntity() as ParamedicScheduleDateItem;

			return entity;
		}
		public ParamedicScheduleDateItem FindByPrimaryKey(String serviceUnitID, String paramedicID, DateTime scheduleDate, String operationalTimeID)
		{
			return base.FindByPrimaryKey(serviceUnitID, paramedicID, scheduleDate, operationalTimeID) as ParamedicScheduleDateItem;
		}

		#region IEnumerable< ParamedicScheduleDateItem> Members

		IEnumerator<ParamedicScheduleDateItem> IEnumerable<ParamedicScheduleDateItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ParamedicScheduleDateItem;
			}
		}

		#endregion

		private ParamedicScheduleDateItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ParamedicScheduleDateItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ParamedicScheduleDateItem ({ServiceUnitID, ParamedicID, ScheduleDate, OperationalTimeID})")]
	[Serializable]
	public partial class ParamedicScheduleDateItem : esParamedicScheduleDateItem
	{
		public ParamedicScheduleDateItem()
		{
		}

		public ParamedicScheduleDateItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ParamedicScheduleDateItemMetadata.Meta();
			}
		}

		override protected esParamedicScheduleDateItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ParamedicScheduleDateItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ParamedicScheduleDateItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ParamedicScheduleDateItemQuery();
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
		public bool Load(ParamedicScheduleDateItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ParamedicScheduleDateItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ParamedicScheduleDateItemQuery : esParamedicScheduleDateItemQuery
	{
		public ParamedicScheduleDateItemQuery()
		{

		}

		public ParamedicScheduleDateItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ParamedicScheduleDateItemQuery";
		}
	}

	[Serializable]
	public partial class ParamedicScheduleDateItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ParamedicScheduleDateItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ParamedicScheduleDateItemMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateItemMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateItemMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateItemMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateItemMetadata.ColumnNames.ScheduleDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicScheduleDateItemMetadata.PropertyNames.ScheduleDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateItemMetadata.ColumnNames.OperationalTimeID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateItemMetadata.PropertyNames.OperationalTimeID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateItemMetadata.ColumnNames.IsIpr, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicScheduleDateItemMetadata.PropertyNames.IsIpr;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateItemMetadata.ColumnNames.IsOpr, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicScheduleDateItemMetadata.PropertyNames.IsOpr;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateItemMetadata.ColumnNames.IsEmr, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ParamedicScheduleDateItemMetadata.PropertyNames.IsEmr;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateItemMetadata.ColumnNames.LastUpdateDateTime, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ParamedicScheduleDateItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ParamedicScheduleDateItemMetadata.ColumnNames.LastUpdateByUserID, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ParamedicScheduleDateItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ParamedicScheduleDateItemMetadata Meta()
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
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ParamedicID = "ParamedicID";
			public const string ScheduleDate = "ScheduleDate";
			public const string OperationalTimeID = "OperationalTimeID";
			public const string IsIpr = "IsIpr";
			public const string IsOpr = "IsOpr";
			public const string IsEmr = "IsEmr";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ParamedicID = "ParamedicID";
			public const string ScheduleDate = "ScheduleDate";
			public const string OperationalTimeID = "OperationalTimeID";
			public const string IsIpr = "IsIpr";
			public const string IsOpr = "IsOpr";
			public const string IsEmr = "IsEmr";
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
			lock (typeof(ParamedicScheduleDateItemMetadata))
			{
				if (ParamedicScheduleDateItemMetadata.mapDelegates == null)
				{
					ParamedicScheduleDateItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ParamedicScheduleDateItemMetadata.meta == null)
				{
					ParamedicScheduleDateItemMetadata.meta = new ParamedicScheduleDateItemMetadata();
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

				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ParamedicID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ScheduleDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("OperationalTimeID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsIpr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsOpr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsEmr", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "ParamedicScheduleDateItem";
				meta.Destination = "ParamedicScheduleDateItem";
				meta.spInsert = "proc_ParamedicScheduleDateItemInsert";
				meta.spUpdate = "proc_ParamedicScheduleDateItemUpdate";
				meta.spDelete = "proc_ParamedicScheduleDateItemDelete";
				meta.spLoadAll = "proc_ParamedicScheduleDateItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_ParamedicScheduleDateItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ParamedicScheduleDateItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
