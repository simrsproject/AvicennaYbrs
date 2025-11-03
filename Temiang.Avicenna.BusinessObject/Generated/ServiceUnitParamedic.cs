/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 11/19/2024 11:08:14 AM
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
	abstract public class esServiceUnitParamedicCollection : esEntityCollectionWAuditLog
	{
		public esServiceUnitParamedicCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "ServiceUnitParamedicCollection";
		}

		#region Query Logic
		protected void InitQuery(esServiceUnitParamedicQuery query)
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
			this.InitQuery(query as esServiceUnitParamedicQuery);
		}
		#endregion

		virtual public ServiceUnitParamedic DetachEntity(ServiceUnitParamedic entity)
		{
			return base.DetachEntity(entity) as ServiceUnitParamedic;
		}

		virtual public ServiceUnitParamedic AttachEntity(ServiceUnitParamedic entity)
		{
			return base.AttachEntity(entity) as ServiceUnitParamedic;
		}

		virtual public void Combine(ServiceUnitParamedicCollection collection)
		{
			base.Combine(collection);
		}

		new public ServiceUnitParamedic this[int index]
		{
			get
			{
				return base[index] as ServiceUnitParamedic;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ServiceUnitParamedic);
		}
	}

	[Serializable]
	abstract public class esServiceUnitParamedic : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esServiceUnitParamedicQuery GetDynamicQuery()
		{
			return null;
		}

		public esServiceUnitParamedic()
		{
		}

		public esServiceUnitParamedic(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String serviceUnitID, String paramedicID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, paramedicID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, paramedicID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String serviceUnitID, String paramedicID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(serviceUnitID, paramedicID);
			else
				return LoadByPrimaryKeyStoredProcedure(serviceUnitID, paramedicID);
		}

		private bool LoadByPrimaryKeyDynamic(String serviceUnitID, String paramedicID)
		{
			esServiceUnitParamedicQuery query = this.GetDynamicQuery();
			query.Where(query.ServiceUnitID == serviceUnitID, query.ParamedicID == paramedicID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String serviceUnitID, String paramedicID)
		{
			esParameters parms = new esParameters();
			parms.Add("ServiceUnitID", serviceUnitID);
			parms.Add("ParamedicID", paramedicID);
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
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "DefaultRoomID": this.str.DefaultRoomID = (string)value; break;
						case "IsUsingQue": this.str.IsUsingQue = (string)value; break;
						case "IsAcceptBPJS": this.str.IsAcceptBPJS = (string)value; break;
						case "IsAcceptNonBPJS": this.str.IsAcceptNonBPJS = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "LastUpdateDateTime":

							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						case "IsUsingQue":

							if (value == null || value is System.Boolean)
								this.IsUsingQue = (System.Boolean?)value;
							break;
						case "IsAcceptBPJS":

							if (value == null || value is System.Boolean)
								this.IsAcceptBPJS = (System.Boolean?)value;
							break;
						case "IsAcceptNonBPJS":

							if (value == null || value is System.Boolean)
								this.IsAcceptNonBPJS = (System.Boolean?)value;
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
		/// Maps to ServiceUnitParamedic.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(ServiceUnitParamedicMetadata.ColumnNames.ServiceUnitID);
			}

			set
			{
				base.SetSystemString(ServiceUnitParamedicMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitParamedic.ParamedicID
		/// </summary>
		virtual public System.String ParamedicID
		{
			get
			{
				return base.GetSystemString(ServiceUnitParamedicMetadata.ColumnNames.ParamedicID);
			}

			set
			{
				base.SetSystemString(ServiceUnitParamedicMetadata.ColumnNames.ParamedicID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitParamedic.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ServiceUnitParamedicMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(ServiceUnitParamedicMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitParamedic.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ServiceUnitParamedicMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(ServiceUnitParamedicMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitParamedic.DefaultRoomID
		/// </summary>
		virtual public System.String DefaultRoomID
		{
			get
			{
				return base.GetSystemString(ServiceUnitParamedicMetadata.ColumnNames.DefaultRoomID);
			}

			set
			{
				base.SetSystemString(ServiceUnitParamedicMetadata.ColumnNames.DefaultRoomID, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitParamedic.IsUsingQue
		/// </summary>
		virtual public System.Boolean? IsUsingQue
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitParamedicMetadata.ColumnNames.IsUsingQue);
			}

			set
			{
				base.SetSystemBoolean(ServiceUnitParamedicMetadata.ColumnNames.IsUsingQue, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitParamedic.IsAcceptBPJS
		/// </summary>
		virtual public System.Boolean? IsAcceptBPJS
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitParamedicMetadata.ColumnNames.IsAcceptBPJS);
			}

			set
			{
				base.SetSystemBoolean(ServiceUnitParamedicMetadata.ColumnNames.IsAcceptBPJS, value);
			}
		}
		/// <summary>
		/// Maps to ServiceUnitParamedic.IsAcceptNonBPJS
		/// </summary>
		virtual public System.Boolean? IsAcceptNonBPJS
		{
			get
			{
				return base.GetSystemBoolean(ServiceUnitParamedicMetadata.ColumnNames.IsAcceptNonBPJS);
			}

			set
			{
				base.SetSystemBoolean(ServiceUnitParamedicMetadata.ColumnNames.IsAcceptNonBPJS, value);
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
			public esStrings(esServiceUnitParamedic entity)
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
			public System.String DefaultRoomID
			{
				get
				{
					System.String data = entity.DefaultRoomID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DefaultRoomID = null;
					else entity.DefaultRoomID = Convert.ToString(value);
				}
			}
			public System.String IsUsingQue
			{
				get
				{
					System.Boolean? data = entity.IsUsingQue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsUsingQue = null;
					else entity.IsUsingQue = Convert.ToBoolean(value);
				}
			}
			public System.String IsAcceptBPJS
			{
				get
				{
					System.Boolean? data = entity.IsAcceptBPJS;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAcceptBPJS = null;
					else entity.IsAcceptBPJS = Convert.ToBoolean(value);
				}
			}
			public System.String IsAcceptNonBPJS
			{
				get
				{
					System.Boolean? data = entity.IsAcceptNonBPJS;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAcceptNonBPJS = null;
					else entity.IsAcceptNonBPJS = Convert.ToBoolean(value);
				}
			}
			private esServiceUnitParamedic entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esServiceUnitParamedicQuery query)
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
				throw new Exception("esServiceUnitParamedic can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class ServiceUnitParamedic : esServiceUnitParamedic
	{
	}

	[Serializable]
	abstract public class esServiceUnitParamedicQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitParamedicMetadata.Meta();
			}
		}

		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitParamedicMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		}

		public esQueryItem ParamedicID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitParamedicMetadata.ColumnNames.ParamedicID, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ServiceUnitParamedicMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitParamedicMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem DefaultRoomID
		{
			get
			{
				return new esQueryItem(this, ServiceUnitParamedicMetadata.ColumnNames.DefaultRoomID, esSystemType.String);
			}
		}

		public esQueryItem IsUsingQue
		{
			get
			{
				return new esQueryItem(this, ServiceUnitParamedicMetadata.ColumnNames.IsUsingQue, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAcceptBPJS
		{
			get
			{
				return new esQueryItem(this, ServiceUnitParamedicMetadata.ColumnNames.IsAcceptBPJS, esSystemType.Boolean);
			}
		}

		public esQueryItem IsAcceptNonBPJS
		{
			get
			{
				return new esQueryItem(this, ServiceUnitParamedicMetadata.ColumnNames.IsAcceptNonBPJS, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ServiceUnitParamedicCollection")]
	public partial class ServiceUnitParamedicCollection : esServiceUnitParamedicCollection, IEnumerable<ServiceUnitParamedic>
	{
		public ServiceUnitParamedicCollection()
		{

		}

		public static implicit operator List<ServiceUnitParamedic>(ServiceUnitParamedicCollection coll)
		{
			List<ServiceUnitParamedic> list = new List<ServiceUnitParamedic>();

			foreach (ServiceUnitParamedic emp in coll)
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
				return ServiceUnitParamedicMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitParamedicQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ServiceUnitParamedic(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ServiceUnitParamedic();
		}

		#endregion

		[BrowsableAttribute(false)]
		public ServiceUnitParamedicQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitParamedicQuery();
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
		public bool Load(ServiceUnitParamedicQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public ServiceUnitParamedic AddNew()
		{
			ServiceUnitParamedic entity = base.AddNewEntity() as ServiceUnitParamedic;

			return entity;
		}
		public ServiceUnitParamedic FindByPrimaryKey(String serviceUnitID, String paramedicID)
		{
			return base.FindByPrimaryKey(serviceUnitID, paramedicID) as ServiceUnitParamedic;
		}

		#region IEnumerable< ServiceUnitParamedic> Members

		IEnumerator<ServiceUnitParamedic> IEnumerable<ServiceUnitParamedic>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as ServiceUnitParamedic;
			}
		}

		#endregion

		private ServiceUnitParamedicQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ServiceUnitParamedic' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("ServiceUnitParamedic ({ServiceUnitID, ParamedicID})")]
	[Serializable]
	public partial class ServiceUnitParamedic : esServiceUnitParamedic
	{
		public ServiceUnitParamedic()
		{
		}

		public ServiceUnitParamedic(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ServiceUnitParamedicMetadata.Meta();
			}
		}

		override protected esServiceUnitParamedicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ServiceUnitParamedicQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public ServiceUnitParamedicQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ServiceUnitParamedicQuery();
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
		public bool Load(ServiceUnitParamedicQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private ServiceUnitParamedicQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class ServiceUnitParamedicQuery : esServiceUnitParamedicQuery
	{
		public ServiceUnitParamedicQuery()
		{

		}

		public ServiceUnitParamedicQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "ServiceUnitParamedicQuery";
		}
	}

	[Serializable]
	public partial class ServiceUnitParamedicMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ServiceUnitParamedicMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ServiceUnitParamedicMetadata.ColumnNames.ServiceUnitID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitParamedicMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitParamedicMetadata.ColumnNames.ParamedicID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitParamedicMetadata.PropertyNames.ParamedicID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitParamedicMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ServiceUnitParamedicMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitParamedicMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitParamedicMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitParamedicMetadata.ColumnNames.DefaultRoomID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ServiceUnitParamedicMetadata.PropertyNames.DefaultRoomID;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitParamedicMetadata.ColumnNames.IsUsingQue, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitParamedicMetadata.PropertyNames.IsUsingQue;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitParamedicMetadata.ColumnNames.IsAcceptBPJS, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitParamedicMetadata.PropertyNames.IsAcceptBPJS;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(ServiceUnitParamedicMetadata.ColumnNames.IsAcceptNonBPJS, 7, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ServiceUnitParamedicMetadata.PropertyNames.IsAcceptNonBPJS;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public ServiceUnitParamedicMetadata Meta()
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
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DefaultRoomID = "DefaultRoomID";
			public const string IsUsingQue = "IsUsingQue";
			public const string IsAcceptBPJS = "IsAcceptBPJS";
			public const string IsAcceptNonBPJS = "IsAcceptNonBPJS";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ServiceUnitID = "ServiceUnitID";
			public const string ParamedicID = "ParamedicID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string DefaultRoomID = "DefaultRoomID";
			public const string IsUsingQue = "IsUsingQue";
			public const string IsAcceptBPJS = "IsAcceptBPJS";
			public const string IsAcceptNonBPJS = "IsAcceptNonBPJS";
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
			lock (typeof(ServiceUnitParamedicMetadata))
			{
				if (ServiceUnitParamedicMetadata.mapDelegates == null)
				{
					ServiceUnitParamedicMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (ServiceUnitParamedicMetadata.meta == null)
				{
					ServiceUnitParamedicMetadata.meta = new ServiceUnitParamedicMetadata();
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
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DefaultRoomID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsUsingQue", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAcceptBPJS", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAcceptNonBPJS", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "ServiceUnitParamedic";
				meta.Destination = "ServiceUnitParamedic";
				meta.spInsert = "proc_ServiceUnitParamedicInsert";
				meta.spUpdate = "proc_ServiceUnitParamedicUpdate";
				meta.spDelete = "proc_ServiceUnitParamedicDelete";
				meta.spLoadAll = "proc_ServiceUnitParamedicLoadAll";
				meta.spLoadByPrimaryKey = "proc_ServiceUnitParamedicLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ServiceUnitParamedicMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
