/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 7/21/2023 10:50:59 AM
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
	abstract public class esRegistrationCloseOpenHistoryCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationCloseOpenHistoryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RegistrationCloseOpenHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationCloseOpenHistoryQuery query)
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
			this.InitQuery(query as esRegistrationCloseOpenHistoryQuery);
		}
		#endregion

		virtual public RegistrationCloseOpenHistory DetachEntity(RegistrationCloseOpenHistory entity)
		{
			return base.DetachEntity(entity) as RegistrationCloseOpenHistory;
		}

		virtual public RegistrationCloseOpenHistory AttachEntity(RegistrationCloseOpenHistory entity)
		{
			return base.AttachEntity(entity) as RegistrationCloseOpenHistory;
		}

		virtual public void Combine(RegistrationCloseOpenHistoryCollection collection)
		{
			base.Combine(collection);
		}

		new public RegistrationCloseOpenHistory this[int index]
		{
			get
			{
				return base[index] as RegistrationCloseOpenHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationCloseOpenHistory);
		}
	}

	[Serializable]
	abstract public class esRegistrationCloseOpenHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationCloseOpenHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationCloseOpenHistory()
		{
		}

		public esRegistrationCloseOpenHistory(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(Guid registrationCloseOpenId)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationCloseOpenId);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationCloseOpenId);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, Guid registrationCloseOpenId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationCloseOpenId);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationCloseOpenId);
		}

		private bool LoadByPrimaryKeyDynamic(Guid registrationCloseOpenId)
		{
			esRegistrationCloseOpenHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationCloseOpenId == registrationCloseOpenId);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(Guid registrationCloseOpenId)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationCloseOpenId", registrationCloseOpenId);
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
						case "RegistrationCloseOpenId": this.str.RegistrationCloseOpenId = (string)value; break;
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "StatusId": this.str.StatusId = (string)value; break;
						case "IsTrue": this.str.IsTrue = (string)value; break;
						case "Notes": this.str.Notes = (string)value; break;
						case "Reason": this.str.Reason = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "RegistrationCloseOpenId":

							if (value == null || value is System.Guid)
								this.RegistrationCloseOpenId = (System.Guid?)value;
							break;
						case "IsTrue":

							if (value == null || value is System.Boolean)
								this.IsTrue = (System.Boolean?)value;
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
		/// Maps to RegistrationCloseOpenHistory.RegistrationCloseOpenId
		/// </summary>
		virtual public System.Guid? RegistrationCloseOpenId
		{
			get
			{
				return base.GetSystemGuid(RegistrationCloseOpenHistoryMetadata.ColumnNames.RegistrationCloseOpenId);
			}

			set
			{
				base.SetSystemGuid(RegistrationCloseOpenHistoryMetadata.ColumnNames.RegistrationCloseOpenId, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCloseOpenHistory.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationCloseOpenHistoryMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationCloseOpenHistoryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCloseOpenHistory.StatusId
		/// </summary>
		virtual public System.String StatusId
		{
			get
			{
				return base.GetSystemString(RegistrationCloseOpenHistoryMetadata.ColumnNames.StatusId);
			}

			set
			{
				base.SetSystemString(RegistrationCloseOpenHistoryMetadata.ColumnNames.StatusId, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCloseOpenHistory.IsTrue
		/// </summary>
		virtual public System.Boolean? IsTrue
		{
			get
			{
				return base.GetSystemBoolean(RegistrationCloseOpenHistoryMetadata.ColumnNames.IsTrue);
			}

			set
			{
				base.SetSystemBoolean(RegistrationCloseOpenHistoryMetadata.ColumnNames.IsTrue, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCloseOpenHistory.Notes
		/// </summary>
		virtual public System.String Notes
		{
			get
			{
				return base.GetSystemString(RegistrationCloseOpenHistoryMetadata.ColumnNames.Notes);
			}

			set
			{
				base.SetSystemString(RegistrationCloseOpenHistoryMetadata.ColumnNames.Notes, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCloseOpenHistory.Reason
		/// </summary>
		virtual public System.String Reason
		{
			get
			{
				return base.GetSystemString(RegistrationCloseOpenHistoryMetadata.ColumnNames.Reason);
			}

			set
			{
				base.SetSystemString(RegistrationCloseOpenHistoryMetadata.ColumnNames.Reason, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCloseOpenHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationCloseOpenHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationCloseOpenHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationCloseOpenHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationCloseOpenHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationCloseOpenHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRegistrationCloseOpenHistory entity)
			{
				this.entity = entity;
			}
			public System.String RegistrationCloseOpenId
			{
				get
				{
					System.Guid? data = entity.RegistrationCloseOpenId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationCloseOpenId = null;
					else entity.RegistrationCloseOpenId = new Guid(value);
				}
			}
			public System.String RegistrationNo
			{
				get
				{
					System.String data = entity.RegistrationNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RegistrationNo = null;
					else entity.RegistrationNo = Convert.ToString(value);
				}
			}
			public System.String StatusId
			{
				get
				{
					System.String data = entity.StatusId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StatusId = null;
					else entity.StatusId = Convert.ToString(value);
				}
			}
			public System.String IsTrue
			{
				get
				{
					System.Boolean? data = entity.IsTrue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTrue = null;
					else entity.IsTrue = Convert.ToBoolean(value);
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
			public System.String Reason
			{
				get
				{
					System.String data = entity.Reason;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Reason = null;
					else entity.Reason = Convert.ToString(value);
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
			private esRegistrationCloseOpenHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationCloseOpenHistoryQuery query)
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
				throw new Exception("esRegistrationCloseOpenHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationCloseOpenHistory : esRegistrationCloseOpenHistory
	{
	}

	[Serializable]
	abstract public class esRegistrationCloseOpenHistoryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RegistrationCloseOpenHistoryMetadata.Meta();
			}
		}

		public esQueryItem RegistrationCloseOpenId
		{
			get
			{
				return new esQueryItem(this, RegistrationCloseOpenHistoryMetadata.ColumnNames.RegistrationCloseOpenId, esSystemType.Guid);
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationCloseOpenHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem StatusId
		{
			get
			{
				return new esQueryItem(this, RegistrationCloseOpenHistoryMetadata.ColumnNames.StatusId, esSystemType.String);
			}
		}

		public esQueryItem IsTrue
		{
			get
			{
				return new esQueryItem(this, RegistrationCloseOpenHistoryMetadata.ColumnNames.IsTrue, esSystemType.Boolean);
			}
		}

		public esQueryItem Notes
		{
			get
			{
				return new esQueryItem(this, RegistrationCloseOpenHistoryMetadata.ColumnNames.Notes, esSystemType.String);
			}
		}

		public esQueryItem Reason
		{
			get
			{
				return new esQueryItem(this, RegistrationCloseOpenHistoryMetadata.ColumnNames.Reason, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationCloseOpenHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationCloseOpenHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationCloseOpenHistoryCollection")]
	public partial class RegistrationCloseOpenHistoryCollection : esRegistrationCloseOpenHistoryCollection, IEnumerable<RegistrationCloseOpenHistory>
	{
		public RegistrationCloseOpenHistoryCollection()
		{

		}

		public static implicit operator List<RegistrationCloseOpenHistory>(RegistrationCloseOpenHistoryCollection coll)
		{
			List<RegistrationCloseOpenHistory> list = new List<RegistrationCloseOpenHistory>();

			foreach (RegistrationCloseOpenHistory emp in coll)
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
				return RegistrationCloseOpenHistoryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationCloseOpenHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationCloseOpenHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationCloseOpenHistory();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RegistrationCloseOpenHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationCloseOpenHistoryQuery();
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
		public bool Load(RegistrationCloseOpenHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationCloseOpenHistory AddNew()
		{
			RegistrationCloseOpenHistory entity = base.AddNewEntity() as RegistrationCloseOpenHistory;

			return entity;
		}
		public RegistrationCloseOpenHistory FindByPrimaryKey(Guid registrationCloseOpenId)
		{
			return base.FindByPrimaryKey(registrationCloseOpenId) as RegistrationCloseOpenHistory;
		}

		#region IEnumerable< RegistrationCloseOpenHistory> Members

		IEnumerator<RegistrationCloseOpenHistory> IEnumerable<RegistrationCloseOpenHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationCloseOpenHistory;
			}
		}

		#endregion

		private RegistrationCloseOpenHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationCloseOpenHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationCloseOpenHistory ({RegistrationCloseOpenId})")]
	[Serializable]
	public partial class RegistrationCloseOpenHistory : esRegistrationCloseOpenHistory
	{
		public RegistrationCloseOpenHistory()
		{
		}

		public RegistrationCloseOpenHistory(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationCloseOpenHistoryMetadata.Meta();
			}
		}

		override protected esRegistrationCloseOpenHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationCloseOpenHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RegistrationCloseOpenHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationCloseOpenHistoryQuery();
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
		public bool Load(RegistrationCloseOpenHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RegistrationCloseOpenHistoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationCloseOpenHistoryQuery : esRegistrationCloseOpenHistoryQuery
	{
		public RegistrationCloseOpenHistoryQuery()
		{

		}

		public RegistrationCloseOpenHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RegistrationCloseOpenHistoryQuery";
		}
	}

	[Serializable]
	public partial class RegistrationCloseOpenHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationCloseOpenHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationCloseOpenHistoryMetadata.ColumnNames.RegistrationCloseOpenId, 0, typeof(System.Guid), esSystemType.Guid);
			c.PropertyName = RegistrationCloseOpenHistoryMetadata.PropertyNames.RegistrationCloseOpenId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 0;
			c.HasDefault = true;
			c.Default = @"(newid())";
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationCloseOpenHistoryMetadata.ColumnNames.RegistrationNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCloseOpenHistoryMetadata.PropertyNames.RegistrationNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationCloseOpenHistoryMetadata.ColumnNames.StatusId, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCloseOpenHistoryMetadata.PropertyNames.StatusId;
			c.CharacterMaxLength = 1;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationCloseOpenHistoryMetadata.ColumnNames.IsTrue, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = RegistrationCloseOpenHistoryMetadata.PropertyNames.IsTrue;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationCloseOpenHistoryMetadata.ColumnNames.Notes, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCloseOpenHistoryMetadata.PropertyNames.Notes;
			c.CharacterMaxLength = 100;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationCloseOpenHistoryMetadata.ColumnNames.Reason, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCloseOpenHistoryMetadata.PropertyNames.Reason;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationCloseOpenHistoryMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationCloseOpenHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationCloseOpenHistoryMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationCloseOpenHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RegistrationCloseOpenHistoryMetadata Meta()
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
			public const string RegistrationCloseOpenId = "RegistrationCloseOpenId";
			public const string RegistrationNo = "RegistrationNo";
			public const string StatusId = "StatusId";
			public const string IsTrue = "IsTrue";
			public const string Notes = "Notes";
			public const string Reason = "Reason";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationCloseOpenId = "RegistrationCloseOpenId";
			public const string RegistrationNo = "RegistrationNo";
			public const string StatusId = "StatusId";
			public const string IsTrue = "IsTrue";
			public const string Notes = "Notes";
			public const string Reason = "Reason";
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
			lock (typeof(RegistrationCloseOpenHistoryMetadata))
			{
				if (RegistrationCloseOpenHistoryMetadata.mapDelegates == null)
				{
					RegistrationCloseOpenHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RegistrationCloseOpenHistoryMetadata.meta == null)
				{
					RegistrationCloseOpenHistoryMetadata.meta = new RegistrationCloseOpenHistoryMetadata();
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

				meta.AddTypeMap("RegistrationCloseOpenId", new esTypeMap("uniqueidentifier", "System.Guid"));
				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("StatusId", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsTrue", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Notes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Reason", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "RegistrationCloseOpenHistory";
				meta.Destination = "RegistrationCloseOpenHistory";
				meta.spInsert = "proc_RegistrationCloseOpenHistoryInsert";
				meta.spUpdate = "proc_RegistrationCloseOpenHistoryUpdate";
				meta.spDelete = "proc_RegistrationCloseOpenHistoryDelete";
				meta.spLoadAll = "proc_RegistrationCloseOpenHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationCloseOpenHistoryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationCloseOpenHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
