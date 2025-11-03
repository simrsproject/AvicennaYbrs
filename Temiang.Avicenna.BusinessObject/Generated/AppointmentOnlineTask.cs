/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/8/2022 11:43:00 AM
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
	abstract public class esAppointmentOnlineTaskCollection : esEntityCollectionWAuditLog
	{
		public esAppointmentOnlineTaskCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppointmentOnlineTaskCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppointmentOnlineTaskQuery query)
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
			this.InitQuery(query as esAppointmentOnlineTaskQuery);
		}
		#endregion

		virtual public AppointmentOnlineTask DetachEntity(AppointmentOnlineTask entity)
		{
			return base.DetachEntity(entity) as AppointmentOnlineTask;
		}

		virtual public AppointmentOnlineTask AttachEntity(AppointmentOnlineTask entity)
		{
			return base.AttachEntity(entity) as AppointmentOnlineTask;
		}

		virtual public void Combine(AppointmentOnlineTaskCollection collection)
		{
			base.Combine(collection);
		}

		new public AppointmentOnlineTask this[int index]
		{
			get
			{
				return base[index] as AppointmentOnlineTask;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppointmentOnlineTask);
		}
	}

	[Serializable]
	abstract public class esAppointmentOnlineTask : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppointmentOnlineTaskQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppointmentOnlineTask()
		{
		}

		public esAppointmentOnlineTask(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String appointmentNo, String taskId)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(appointmentNo, taskId);
			else
				return LoadByPrimaryKeyStoredProcedure(appointmentNo, taskId);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String appointmentNo, String taskId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(appointmentNo, taskId);
			else
				return LoadByPrimaryKeyStoredProcedure(appointmentNo, taskId);
		}

		private bool LoadByPrimaryKeyDynamic(String appointmentNo, String taskId)
		{
			esAppointmentOnlineTaskQuery query = this.GetDynamicQuery();
			query.Where(query.AppointmentNo == appointmentNo, query.TaskId == taskId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String appointmentNo, String taskId)
		{
			esParameters parms = new esParameters();
			parms.Add("AppointmentNo", appointmentNo);
			parms.Add("TaskId", taskId);
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
						case "AppointmentNo": this.str.AppointmentNo = (string)value; break;
						case "TaskId": this.str.TaskId = (string)value; break;
						case "Timestamp": this.str.Timestamp = (string)value; break;
						case "LastUpdatedDate": this.str.LastUpdatedDate = (string)value; break;
						case "IsSended": this.str.IsSended = (string)value; break;
						case "Attempt": this.str.Attempt = (string)value; break;
						case "IsError": this.str.IsError = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "LastUpdatedDate":

							if (value == null || value is System.DateTime)
								this.LastUpdatedDate = (System.DateTime?)value;
							break;
						case "IsSended":

							if (value == null || value is System.Boolean)
								this.IsSended = (System.Boolean?)value;
							break;
						case "Attempt":

							if (value == null || value is System.Byte)
								this.Attempt = (System.Byte?)value;
							break;
						case "IsError":

							if (value == null || value is System.Boolean)
								this.IsError = (System.Boolean?)value;
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
		/// Maps to AppointmentOnlineTask.AppointmentNo
		/// </summary>
		virtual public System.String AppointmentNo
		{
			get
			{
				return base.GetSystemString(AppointmentOnlineTaskMetadata.ColumnNames.AppointmentNo);
			}

			set
			{
				base.SetSystemString(AppointmentOnlineTaskMetadata.ColumnNames.AppointmentNo, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentOnlineTask.TaskId
		/// </summary>
		virtual public System.String TaskId
		{
			get
			{
				return base.GetSystemString(AppointmentOnlineTaskMetadata.ColumnNames.TaskId);
			}

			set
			{
				base.SetSystemString(AppointmentOnlineTaskMetadata.ColumnNames.TaskId, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentOnlineTask.Timestamp
		/// </summary>
		virtual public System.String Timestamp
		{
			get
			{
				return base.GetSystemString(AppointmentOnlineTaskMetadata.ColumnNames.Timestamp);
			}

			set
			{
				base.SetSystemString(AppointmentOnlineTaskMetadata.ColumnNames.Timestamp, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentOnlineTask.LastUpdatedDate
		/// </summary>
		virtual public System.DateTime? LastUpdatedDate
		{
			get
			{
				return base.GetSystemDateTime(AppointmentOnlineTaskMetadata.ColumnNames.LastUpdatedDate);
			}

			set
			{
				base.SetSystemDateTime(AppointmentOnlineTaskMetadata.ColumnNames.LastUpdatedDate, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentOnlineTask.IsSended
		/// </summary>
		virtual public System.Boolean? IsSended
		{
			get
			{
				return base.GetSystemBoolean(AppointmentOnlineTaskMetadata.ColumnNames.IsSended);
			}

			set
			{
				base.SetSystemBoolean(AppointmentOnlineTaskMetadata.ColumnNames.IsSended, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentOnlineTask.Attempt
		/// </summary>
		virtual public System.Byte? Attempt
		{
			get
			{
				return base.GetSystemByte(AppointmentOnlineTaskMetadata.ColumnNames.Attempt);
			}

			set
			{
				base.SetSystemByte(AppointmentOnlineTaskMetadata.ColumnNames.Attempt, value);
			}
		}
		/// <summary>
		/// Maps to AppointmentOnlineTask.IsError
		/// </summary>
		virtual public System.Boolean? IsError
		{
			get
			{
				return base.GetSystemBoolean(AppointmentOnlineTaskMetadata.ColumnNames.IsError);
			}

			set
			{
				base.SetSystemBoolean(AppointmentOnlineTaskMetadata.ColumnNames.IsError, value);
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
			public esStrings(esAppointmentOnlineTask entity)
			{
				this.entity = entity;
			}
			public System.String AppointmentNo
			{
				get
				{
					System.String data = entity.AppointmentNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AppointmentNo = null;
					else entity.AppointmentNo = Convert.ToString(value);
				}
			}
			public System.String TaskId
			{
				get
				{
					System.String data = entity.TaskId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaskId = null;
					else entity.TaskId = Convert.ToString(value);
				}
			}
			public System.String Timestamp
			{
				get
				{
					System.String data = entity.Timestamp;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Timestamp = null;
					else entity.Timestamp = Convert.ToString(value);
				}
			}
			public System.String LastUpdatedDate
			{
				get
				{
					System.DateTime? data = entity.LastUpdatedDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastUpdatedDate = null;
					else entity.LastUpdatedDate = Convert.ToDateTime(value);
				}
			}
			public System.String IsSended
			{
				get
				{
					System.Boolean? data = entity.IsSended;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSended = null;
					else entity.IsSended = Convert.ToBoolean(value);
				}
			}
			public System.String Attempt
			{
				get
				{
					System.Byte? data = entity.Attempt;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Attempt = null;
					else entity.Attempt = Convert.ToByte(value);
				}
			}
			public System.String IsError
			{
				get
				{
					System.Boolean? data = entity.IsError;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsError = null;
					else entity.IsError = Convert.ToBoolean(value);
				}
			}
			private esAppointmentOnlineTask entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppointmentOnlineTaskQuery query)
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
				throw new Exception("esAppointmentOnlineTask can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppointmentOnlineTask : esAppointmentOnlineTask
	{
	}

	[Serializable]
	abstract public class esAppointmentOnlineTaskQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppointmentOnlineTaskMetadata.Meta();
			}
		}

		public esQueryItem AppointmentNo
		{
			get
			{
				return new esQueryItem(this, AppointmentOnlineTaskMetadata.ColumnNames.AppointmentNo, esSystemType.String);
			}
		}

		public esQueryItem TaskId
		{
			get
			{
				return new esQueryItem(this, AppointmentOnlineTaskMetadata.ColumnNames.TaskId, esSystemType.String);
			}
		}

		public esQueryItem Timestamp
		{
			get
			{
				return new esQueryItem(this, AppointmentOnlineTaskMetadata.ColumnNames.Timestamp, esSystemType.String);
			}
		}

		public esQueryItem LastUpdatedDate
		{
			get
			{
				return new esQueryItem(this, AppointmentOnlineTaskMetadata.ColumnNames.LastUpdatedDate, esSystemType.DateTime);
			}
		}

		public esQueryItem IsSended
		{
			get
			{
				return new esQueryItem(this, AppointmentOnlineTaskMetadata.ColumnNames.IsSended, esSystemType.Boolean);
			}
		}

		public esQueryItem Attempt
		{
			get
			{
				return new esQueryItem(this, AppointmentOnlineTaskMetadata.ColumnNames.Attempt, esSystemType.Byte);
			}
		}

		public esQueryItem IsError
		{
			get
			{
				return new esQueryItem(this, AppointmentOnlineTaskMetadata.ColumnNames.IsError, esSystemType.Boolean);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppointmentOnlineTaskCollection")]
	public partial class AppointmentOnlineTaskCollection : esAppointmentOnlineTaskCollection, IEnumerable<AppointmentOnlineTask>
	{
		public AppointmentOnlineTaskCollection()
		{

		}

		public static implicit operator List<AppointmentOnlineTask>(AppointmentOnlineTaskCollection coll)
		{
			List<AppointmentOnlineTask> list = new List<AppointmentOnlineTask>();

			foreach (AppointmentOnlineTask emp in coll)
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
				return AppointmentOnlineTaskMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppointmentOnlineTaskQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppointmentOnlineTask(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppointmentOnlineTask();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppointmentOnlineTaskQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppointmentOnlineTaskQuery();
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
		public bool Load(AppointmentOnlineTaskQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppointmentOnlineTask AddNew()
		{
			AppointmentOnlineTask entity = base.AddNewEntity() as AppointmentOnlineTask;

			return entity;
		}
		public AppointmentOnlineTask FindByPrimaryKey(String appointmentNo, String taskId)
		{
			return base.FindByPrimaryKey(appointmentNo, taskId) as AppointmentOnlineTask;
		}

		#region IEnumerable< AppointmentOnlineTask> Members

		IEnumerator<AppointmentOnlineTask> IEnumerable<AppointmentOnlineTask>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppointmentOnlineTask;
			}
		}

		#endregion

		private AppointmentOnlineTaskQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppointmentOnlineTask' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppointmentOnlineTask ({AppointmentNo, TaskId})")]
	[Serializable]
	public partial class AppointmentOnlineTask : esAppointmentOnlineTask
	{
		public AppointmentOnlineTask()
		{
		}

		public AppointmentOnlineTask(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppointmentOnlineTaskMetadata.Meta();
			}
		}

		override protected esAppointmentOnlineTaskQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppointmentOnlineTaskQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppointmentOnlineTaskQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppointmentOnlineTaskQuery();
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
		public bool Load(AppointmentOnlineTaskQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppointmentOnlineTaskQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppointmentOnlineTaskQuery : esAppointmentOnlineTaskQuery
	{
		public AppointmentOnlineTaskQuery()
		{

		}

		public AppointmentOnlineTaskQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppointmentOnlineTaskQuery";
		}
	}

	[Serializable]
	public partial class AppointmentOnlineTaskMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppointmentOnlineTaskMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppointmentOnlineTaskMetadata.ColumnNames.AppointmentNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentOnlineTaskMetadata.PropertyNames.AppointmentNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentOnlineTaskMetadata.ColumnNames.TaskId, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentOnlineTaskMetadata.PropertyNames.TaskId;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentOnlineTaskMetadata.ColumnNames.Timestamp, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppointmentOnlineTaskMetadata.PropertyNames.Timestamp;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentOnlineTaskMetadata.ColumnNames.LastUpdatedDate, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppointmentOnlineTaskMetadata.PropertyNames.LastUpdatedDate;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentOnlineTaskMetadata.ColumnNames.IsSended, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppointmentOnlineTaskMetadata.PropertyNames.IsSended;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentOnlineTaskMetadata.ColumnNames.Attempt, 5, typeof(System.Byte), esSystemType.Byte);
			c.PropertyName = AppointmentOnlineTaskMetadata.PropertyNames.Attempt;
			c.NumericPrecision = 3;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppointmentOnlineTaskMetadata.ColumnNames.IsError, 6, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppointmentOnlineTaskMetadata.PropertyNames.IsError;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppointmentOnlineTaskMetadata Meta()
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
			public const string AppointmentNo = "AppointmentNo";
			public const string TaskId = "TaskId";
			public const string Timestamp = "Timestamp";
			public const string LastUpdatedDate = "LastUpdatedDate";
			public const string IsSended = "IsSended";
			public const string Attempt = "Attempt";
			public const string IsError = "IsError";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string AppointmentNo = "AppointmentNo";
			public const string TaskId = "TaskId";
			public const string Timestamp = "Timestamp";
			public const string LastUpdatedDate = "LastUpdatedDate";
			public const string IsSended = "IsSended";
			public const string Attempt = "Attempt";
			public const string IsError = "IsError";
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
			lock (typeof(AppointmentOnlineTaskMetadata))
			{
				if (AppointmentOnlineTaskMetadata.mapDelegates == null)
				{
					AppointmentOnlineTaskMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppointmentOnlineTaskMetadata.meta == null)
				{
					AppointmentOnlineTaskMetadata.meta = new AppointmentOnlineTaskMetadata();
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

				meta.AddTypeMap("AppointmentNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TaskId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Timestamp", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdatedDate", new esTypeMap("datetime2", "System.DateTime"));
				meta.AddTypeMap("IsSended", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Attempt", new esTypeMap("tinyint", "System.Byte"));
				meta.AddTypeMap("IsError", new esTypeMap("bit", "System.Boolean"));


				meta.Source = "AppointmentOnlineTask";
				meta.Destination = "AppointmentOnlineTask";
				meta.spInsert = "proc_AppointmentOnlineTaskInsert";
				meta.spUpdate = "proc_AppointmentOnlineTaskUpdate";
				meta.spDelete = "proc_AppointmentOnlineTaskDelete";
				meta.spLoadAll = "proc_AppointmentOnlineTaskLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppointmentOnlineTaskLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppointmentOnlineTaskMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
