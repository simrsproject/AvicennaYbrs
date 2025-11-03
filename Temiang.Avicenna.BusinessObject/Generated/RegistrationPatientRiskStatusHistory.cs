/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 1/5/2023 1:15:13 PM
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
	abstract public class esRegistrationPatientRiskStatusHistoryCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationPatientRiskStatusHistoryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RegistrationPatientRiskStatusHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationPatientRiskStatusHistoryQuery query)
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
			this.InitQuery(query as esRegistrationPatientRiskStatusHistoryQuery);
		}
		#endregion

		virtual public RegistrationPatientRiskStatusHistory DetachEntity(RegistrationPatientRiskStatusHistory entity)
		{
			return base.DetachEntity(entity) as RegistrationPatientRiskStatusHistory;
		}

		virtual public RegistrationPatientRiskStatusHistory AttachEntity(RegistrationPatientRiskStatusHistory entity)
		{
			return base.AttachEntity(entity) as RegistrationPatientRiskStatusHistory;
		}

		virtual public void Combine(RegistrationPatientRiskStatusHistoryCollection collection)
		{
			base.Combine(collection);
		}

		new public RegistrationPatientRiskStatusHistory this[int index]
		{
			get
			{
				return base[index] as RegistrationPatientRiskStatusHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationPatientRiskStatusHistory);
		}
	}

	[Serializable]
	abstract public class esRegistrationPatientRiskStatusHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationPatientRiskStatusHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationPatientRiskStatusHistory()
		{
		}

		public esRegistrationPatientRiskStatusHistory(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, String fromSRPatientRiskStatus, String toSRPatientRiskStatus, DateTime lastUpdateDateTime)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, fromSRPatientRiskStatus, toSRPatientRiskStatus, lastUpdateDateTime);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, fromSRPatientRiskStatus, toSRPatientRiskStatus, lastUpdateDateTime);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, String fromSRPatientRiskStatus, String toSRPatientRiskStatus, DateTime lastUpdateDateTime)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, fromSRPatientRiskStatus, toSRPatientRiskStatus, lastUpdateDateTime);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, fromSRPatientRiskStatus, toSRPatientRiskStatus, lastUpdateDateTime);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, String fromSRPatientRiskStatus, String toSRPatientRiskStatus, DateTime lastUpdateDateTime)
		{
			esRegistrationPatientRiskStatusHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.FromSRPatientRiskStatus == fromSRPatientRiskStatus, query.ToSRPatientRiskStatus == toSRPatientRiskStatus, query.LastUpdateDateTime == lastUpdateDateTime);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, String fromSRPatientRiskStatus, String toSRPatientRiskStatus, DateTime lastUpdateDateTime)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("FromSRPatientRiskStatus", fromSRPatientRiskStatus);
			parms.Add("ToSRPatientRiskStatus", toSRPatientRiskStatus);
			parms.Add("LastUpdateDateTime", lastUpdateDateTime);
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
						case "RegistrationNo": this.str.RegistrationNo = (string)value; break;
						case "FromSRPatientRiskStatus": this.str.FromSRPatientRiskStatus = (string)value; break;
						case "ToSRPatientRiskStatus": this.str.ToSRPatientRiskStatus = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
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
		/// Maps to RegistrationPatientRiskStatusHistory.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPatientRiskStatusHistory.FromSRPatientRiskStatus
		/// </summary>
		virtual public System.String FromSRPatientRiskStatus
		{
			get
			{
				return base.GetSystemString(RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.FromSRPatientRiskStatus);
			}

			set
			{
				base.SetSystemString(RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.FromSRPatientRiskStatus, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPatientRiskStatusHistory.ToSRPatientRiskStatus
		/// </summary>
		virtual public System.String ToSRPatientRiskStatus
		{
			get
			{
				return base.GetSystemString(RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.ToSRPatientRiskStatus);
			}

			set
			{
				base.SetSystemString(RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.ToSRPatientRiskStatus, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPatientRiskStatusHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationPatientRiskStatusHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esRegistrationPatientRiskStatusHistory entity)
			{
				this.entity = entity;
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
			public System.String FromSRPatientRiskStatus
			{
				get
				{
					System.String data = entity.FromSRPatientRiskStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromSRPatientRiskStatus = null;
					else entity.FromSRPatientRiskStatus = Convert.ToString(value);
				}
			}
			public System.String ToSRPatientRiskStatus
			{
				get
				{
					System.String data = entity.ToSRPatientRiskStatus;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToSRPatientRiskStatus = null;
					else entity.ToSRPatientRiskStatus = Convert.ToString(value);
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
			private esRegistrationPatientRiskStatusHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationPatientRiskStatusHistoryQuery query)
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
				throw new Exception("esRegistrationPatientRiskStatusHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationPatientRiskStatusHistory : esRegistrationPatientRiskStatusHistory
	{
	}

	[Serializable]
	abstract public class esRegistrationPatientRiskStatusHistoryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RegistrationPatientRiskStatusHistoryMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem FromSRPatientRiskStatus
		{
			get
			{
				return new esQueryItem(this, RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.FromSRPatientRiskStatus, esSystemType.String);
			}
		}

		public esQueryItem ToSRPatientRiskStatus
		{
			get
			{
				return new esQueryItem(this, RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.ToSRPatientRiskStatus, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationPatientRiskStatusHistoryCollection")]
	public partial class RegistrationPatientRiskStatusHistoryCollection : esRegistrationPatientRiskStatusHistoryCollection, IEnumerable<RegistrationPatientRiskStatusHistory>
	{
		public RegistrationPatientRiskStatusHistoryCollection()
		{

		}

		public static implicit operator List<RegistrationPatientRiskStatusHistory>(RegistrationPatientRiskStatusHistoryCollection coll)
		{
			List<RegistrationPatientRiskStatusHistory> list = new List<RegistrationPatientRiskStatusHistory>();

			foreach (RegistrationPatientRiskStatusHistory emp in coll)
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
				return RegistrationPatientRiskStatusHistoryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationPatientRiskStatusHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationPatientRiskStatusHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationPatientRiskStatusHistory();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RegistrationPatientRiskStatusHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationPatientRiskStatusHistoryQuery();
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
		public bool Load(RegistrationPatientRiskStatusHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationPatientRiskStatusHistory AddNew()
		{
			RegistrationPatientRiskStatusHistory entity = base.AddNewEntity() as RegistrationPatientRiskStatusHistory;

			return entity;
		}
		public RegistrationPatientRiskStatusHistory FindByPrimaryKey(String registrationNo, String fromSRPatientRiskStatus, String toSRPatientRiskStatus, DateTime lastUpdateDateTime)
		{
			return base.FindByPrimaryKey(registrationNo, fromSRPatientRiskStatus, toSRPatientRiskStatus, lastUpdateDateTime) as RegistrationPatientRiskStatusHistory;
		}

		#region IEnumerable< RegistrationPatientRiskStatusHistory> Members

		IEnumerator<RegistrationPatientRiskStatusHistory> IEnumerable<RegistrationPatientRiskStatusHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationPatientRiskStatusHistory;
			}
		}

		#endregion

		private RegistrationPatientRiskStatusHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationPatientRiskStatusHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationPatientRiskStatusHistory ({RegistrationNo, FromSRPatientRiskStatus, ToSRPatientRiskStatus, LastUpdateDateTime})")]
	[Serializable]
	public partial class RegistrationPatientRiskStatusHistory : esRegistrationPatientRiskStatusHistory
	{
		public RegistrationPatientRiskStatusHistory()
		{
		}

		public RegistrationPatientRiskStatusHistory(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationPatientRiskStatusHistoryMetadata.Meta();
			}
		}

		override protected esRegistrationPatientRiskStatusHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationPatientRiskStatusHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RegistrationPatientRiskStatusHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationPatientRiskStatusHistoryQuery();
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
		public bool Load(RegistrationPatientRiskStatusHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RegistrationPatientRiskStatusHistoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationPatientRiskStatusHistoryQuery : esRegistrationPatientRiskStatusHistoryQuery
	{
		public RegistrationPatientRiskStatusHistoryQuery()
		{

		}

		public RegistrationPatientRiskStatusHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RegistrationPatientRiskStatusHistoryQuery";
		}
	}

	[Serializable]
	public partial class RegistrationPatientRiskStatusHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationPatientRiskStatusHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPatientRiskStatusHistoryMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.FromSRPatientRiskStatus, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPatientRiskStatusHistoryMetadata.PropertyNames.FromSRPatientRiskStatus;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.ToSRPatientRiskStatus, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPatientRiskStatusHistoryMetadata.PropertyNames.ToSRPatientRiskStatus;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationPatientRiskStatusHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationPatientRiskStatusHistoryMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationPatientRiskStatusHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RegistrationPatientRiskStatusHistoryMetadata Meta()
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
			public const string RegistrationNo = "RegistrationNo";
			public const string FromSRPatientRiskStatus = "FromSRPatientRiskStatus";
			public const string ToSRPatientRiskStatus = "ToSRPatientRiskStatus";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string FromSRPatientRiskStatus = "FromSRPatientRiskStatus";
			public const string ToSRPatientRiskStatus = "ToSRPatientRiskStatus";
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
			lock (typeof(RegistrationPatientRiskStatusHistoryMetadata))
			{
				if (RegistrationPatientRiskStatusHistoryMetadata.mapDelegates == null)
				{
					RegistrationPatientRiskStatusHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RegistrationPatientRiskStatusHistoryMetadata.meta == null)
				{
					RegistrationPatientRiskStatusHistoryMetadata.meta = new RegistrationPatientRiskStatusHistoryMetadata();
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

				meta.AddTypeMap("RegistrationNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromSRPatientRiskStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToSRPatientRiskStatus", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "RegistrationPatientRiskStatusHistory";
				meta.Destination = "RegistrationPatientRiskStatusHistory";
				meta.spInsert = "proc_RegistrationPatientRiskStatusHistoryInsert";
				meta.spUpdate = "proc_RegistrationPatientRiskStatusHistoryUpdate";
				meta.spDelete = "proc_RegistrationPatientRiskStatusHistoryDelete";
				meta.spLoadAll = "proc_RegistrationPatientRiskStatusHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationPatientRiskStatusHistoryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationPatientRiskStatusHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
