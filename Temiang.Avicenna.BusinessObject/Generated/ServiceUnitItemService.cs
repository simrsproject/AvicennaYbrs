/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/23/2020 7:58:28 PM
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
	abstract public class esServiceUnitItemServiceCollection : esEntityCollectionWAuditLog
	{
		public esServiceUnitItemServiceCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ServiceUnitItemServiceCollection";
		}

		#region Query Logic
		protected void InitQuery(esServiceUnitItemServiceQuery query)
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
			this.InitQuery(query as esServiceUnitItemServiceQuery);
		}
		#endregion

		virtual public ServiceUnitItemService DetachEntity(ServiceUnitItemService entity)
		{
			return base.DetachEntity(entity) as ServiceUnitItemService;
		}

		virtual public ServiceUnitItemService AttachEntity(ServiceUnitItemService entity)
		{
			return base.AttachEntity(entity) as ServiceUnitItemService;
		}

		virtual public void Combine(ServiceUnitItemServiceCollection collection)
		{
			base.Combine(collection);
		}

		new public ServiceUnitItemService this[int index]
		{
			get
			{
				return base[index] as ServiceUnitItemService;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceUnitItemService);
		}
	}

	[Serializable]
	abstract public class esServiceUnitItemService : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceUnitItemServiceQuery GetDynamicQuery()
		{
			return null;
		}

		public esServiceUnitItemService()
		{
		}

		public esServiceUnitItemService(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String serviceUnitID, String itemID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, itemID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String serviceUnitID, String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(String serviceUnitID, String itemID)
		{
			esServiceUnitItemServiceQuery query = this.GetDynamicQuery();
			query.Where(query.ServiceUnitID == serviceUnitID, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String serviceUnitID, String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("ServiceUnitID", serviceUnitID);
			parms.Add("ItemID", itemID);
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
						case "ItemID": this.str.ItemID = (string)value; break;
						case "ChartOfAccountId": this.str.ChartOfAccountId = (string)value; break;
						case "SubledgerId": this.str.SubledgerId = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "IsAllowEditByUserVerificated": this.str.IsAllowEditByUserVerificated = (string)value; break;
						case "IsVisible": this.str.IsVisible = (string)value; break;
						case "IdiCode": this.str.IdiCode = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "ChartOfAccountId":

							if (value == null || value is System.Int32)
								this.ChartOfAccountId = (System.Int32?)value;
							break;
						case "SubledgerId":

							if (value == null || value is System.Int32)
								this.SubledgerId = (System.Int32?)value;
							break;
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsAllowEditByUserVerificated":

							if (value == null || value is System.Boolean)
								this.IsAllowEditByUserVerificated = (System.Boolean?)value;
							break;
						case "IsVisible":

							if (value == null || value is System.Boolean)
								this.IsVisible = (System.Boolean?)value;
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
		/// Maps to ServiceUnitItemService.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ServiceUnitItemServiceMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(ServiceUnitItemServiceMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitItemService.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ServiceUnitItemServiceMetadata.ColumnNames.ItemID);
			}

			set
			{
				base.SetSystemString(ServiceUnitItemServiceMetadata.ColumnNames.ItemID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitItemService.ChartOfAccountId
		/// </summary>
		virtual public System.Int32? ChartOfAccountId
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitItemServiceMetadata.ColumnNames.ChartOfAccountId);
			}

			set
			{
				base.SetSystemInt32(ServiceUnitItemServiceMetadata.ColumnNames.ChartOfAccountId, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitItemService.SubledgerId
		/// </summary>
		virtual public System.Int32? SubledgerId
		{
			get
			{
				return base.GetSystemInt32(ServiceUnitItemServiceMetadata.ColumnNames.SubledgerId);
			}

			set
			{
				base.SetSystemInt32(ServiceUnitItemServiceMetadata.ColumnNames.SubledgerId, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitItemService.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitItemServiceMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitItemServiceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitItemService.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitItemServiceMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ServiceUnitItemServiceMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitItemService.IsAllowEditByUserVerificated
		/// </summary>
		virtual public System.Boolean? IsAllowEditByUserVerificated
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitItemServiceMetadata.ColumnNames.IsAllowEditByUserVerificated);
			}

			set
			{
				base.SetSystemBoolean(ServiceUnitItemServiceMetadata.ColumnNames.IsAllowEditByUserVerificated, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitItemService.IsVisible
		/// </summary>
		virtual public System.Boolean? IsVisible
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitItemServiceMetadata.ColumnNames.IsVisible);
			}

			set
			{
				base.SetSystemBoolean(ServiceUnitItemServiceMetadata.ColumnNames.IsVisible, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitItemService.IdiCode
		/// </summary>
		virtual public System.String IdiCode
		{
			get
			{
				return base.GetSystemString(ServiceUnitItemServiceMetadata.ColumnNames.IdiCode);
			}

			set
			{
				base.SetSystemString(ServiceUnitItemServiceMetadata.ColumnNames.IdiCode, value);
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
			public esStrings(esServiceUnitItemService entity)
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
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
			}
			public System.String ChartOfAccountId
			{
				get
				{
					System.Int32? data = entity.ChartOfAccountId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ChartOfAccountId = null;
					else entity.ChartOfAccountId = Convert.ToInt32(value);
				}
			}
			public System.String SubledgerId
			{
				get
				{
					System.Int32? data = entity.SubledgerId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubledgerId = null;
					else entity.SubledgerId = Convert.ToInt32(value);
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
			public System.String IsAllowEditByUserVerificated
			{
				get
				{
					System.Boolean? data = entity.IsAllowEditByUserVerificated;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowEditByUserVerificated = null;
					else entity.IsAllowEditByUserVerificated = Convert.ToBoolean(value);
				}
			}
			public System.String IsVisible
			{
				get
				{
					System.Boolean? data = entity.IsVisible;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsVisible = null;
					else entity.IsVisible = Convert.ToBoolean(value);
				}
			}
			public System.String IdiCode
			{
				get
				{
					System.String data = entity.IdiCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IdiCode = null;
					else entity.IdiCode = Convert.ToString(value);
				}
			}
			private esServiceUnitItemService entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceUnitItemServiceQuery query)
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
				throw new Exception("esServiceUnitItemService can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ServiceUnitItemService : esServiceUnitItemService
	{
	}

	[Serializable]
	abstract public class esServiceUnitItemServiceQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitItemServiceMetadata.Meta();
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		}

		public esQueryItem ChartOfAccountId
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceMetadata.ColumnNames.ChartOfAccountId, esSystemType.Int32);
			}
		}

		public esQueryItem SubledgerId
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceMetadata.ColumnNames.SubledgerId, esSystemType.Int32);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem IsAllowEditByUserVerificated
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceMetadata.ColumnNames.IsAllowEditByUserVerificated, esSystemType.Boolean);
			}
		}

		public esQueryItem IsVisible
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceMetadata.ColumnNames.IsVisible, esSystemType.Boolean);
			}
		}

		public esQueryItem IdiCode
		{
			get
			{
				return new esQueryItem(this, ServiceUnitItemServiceMetadata.ColumnNames.IdiCode, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceUnitItemServiceCollection")]
	public partial class ServiceUnitItemServiceCollection : esServiceUnitItemServiceCollection, IEnumerable<ServiceUnitItemService>
	{
		public ServiceUnitItemServiceCollection()
		{

		}

		public static implicit operator List<ServiceUnitItemService>(ServiceUnitItemServiceCollection coll)
		{
			List<ServiceUnitItemService> list = new List<ServiceUnitItemService>();

			foreach (ServiceUnitItemService emp in coll)
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
				return ServiceUnitItemServiceMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitItemServiceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceUnitItemService(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceUnitItemService();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ServiceUnitItemServiceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitItemServiceQuery();
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
		public bool Load(ServiceUnitItemServiceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ServiceUnitItemService AddNew()
		{
			ServiceUnitItemService entity = base.AddNewEntity() as ServiceUnitItemService;

			return entity;
		}
		public ServiceUnitItemService FindByPrimaryKey(String serviceUnitID, String itemID)
		{
			return base.FindByPrimaryKey(serviceUnitID, itemID) as ServiceUnitItemService;
		}

		#region IEnumerable< ServiceUnitItemService> Members

		IEnumerator<ServiceUnitItemService> IEnumerable<ServiceUnitItemService>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ServiceUnitItemService;
			}
		}

		#endregion

		private ServiceUnitItemServiceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceUnitItemService' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ServiceUnitItemService ({ServiceUnitID, ItemID})")]
	[Serializable]
	public partial class ServiceUnitItemService : esServiceUnitItemService
	{
		public ServiceUnitItemService()
		{
		}

		public ServiceUnitItemService(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitItemServiceMetadata.Meta();
			}
		}

		override protected esServiceUnitItemServiceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitItemServiceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ServiceUnitItemServiceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitItemServiceQuery();
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
		public bool Load(ServiceUnitItemServiceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ServiceUnitItemServiceQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ServiceUnitItemServiceQuery : esServiceUnitItemServiceQuery
	{
		public ServiceUnitItemServiceQuery()
		{

		}

		public ServiceUnitItemServiceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ServiceUnitItemServiceQuery";
		}
	}

	[Serializable]
	public partial class ServiceUnitItemServiceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceUnitItemServiceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ServiceUnitItemServiceMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitItemServiceMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitItemServiceMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitItemServiceMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitItemServiceMetadata.ColumnNames.ChartOfAccountId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitItemServiceMetadata.PropertyNames.ChartOfAccountId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitItemServiceMetadata.ColumnNames.SubledgerId, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ServiceUnitItemServiceMetadata.PropertyNames.SubledgerId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitItemServiceMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitItemServiceMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitItemServiceMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitItemServiceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitItemServiceMetadata.ColumnNames.IsAllowEditByUserVerificated, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitItemServiceMetadata.PropertyNames.IsAllowEditByUserVerificated;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitItemServiceMetadata.ColumnNames.IsVisible, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitItemServiceMetadata.PropertyNames.IsVisible;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitItemServiceMetadata.ColumnNames.IdiCode, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitItemServiceMetadata.PropertyNames.IdiCode;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ServiceUnitItemServiceMetadata Meta()
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
			public const string ItemID = "ItemID";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubledgerId = "SubledgerId";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsAllowEditByUserVerificated = "IsAllowEditByUserVerificated";
			public const string IsVisible = "IsVisible";
			public const string IdiCode = "IdiCode";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ItemID = "ItemID";
			public const string ChartOfAccountId = "ChartOfAccountId";
			public const string SubledgerId = "SubledgerId";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string IsAllowEditByUserVerificated = "IsAllowEditByUserVerificated";
			public const string IsVisible = "IsVisible";
			public const string IdiCode = "IdiCode";
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
			lock (typeof(ServiceUnitItemServiceMetadata))
			{
				if (ServiceUnitItemServiceMetadata.mapDelegates == null)
				{
					ServiceUnitItemServiceMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ServiceUnitItemServiceMetadata.meta == null)
				{
					ServiceUnitItemServiceMetadata.meta = new ServiceUnitItemServiceMetadata();
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
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ChartOfAccountId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("SubledgerId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsAllowEditByUserVerificated", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsVisible", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IdiCode", new esTypeMap("varchar", "System.String"));


				meta.Source = "ServiceUnitItemService";
				meta.Destination = "ServiceUnitItemService";
				meta.spInsert = "proc_ServiceUnitItemServiceInsert";
				meta.spUpdate = "proc_ServiceUnitItemServiceUpdate";
				meta.spDelete = "proc_ServiceUnitItemServiceDelete";
				meta.spLoadAll = "proc_ServiceUnitItemServiceLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceUnitItemServiceLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceUnitItemServiceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
