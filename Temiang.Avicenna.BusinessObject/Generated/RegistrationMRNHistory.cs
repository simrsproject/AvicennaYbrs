/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/3/2021 2:51:55 PM
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
	abstract public class esRegistrationMRNHistoryCollection : esEntityCollectionWAuditLog
	{
		public esRegistrationMRNHistoryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "RegistrationMRNHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esRegistrationMRNHistoryQuery query)
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
			this.InitQuery(query as esRegistrationMRNHistoryQuery);
		}
		#endregion

		virtual public RegistrationMRNHistory DetachEntity(RegistrationMRNHistory entity)
		{
			return base.DetachEntity(entity) as RegistrationMRNHistory;
		}

		virtual public RegistrationMRNHistory AttachEntity(RegistrationMRNHistory entity)
		{
			return base.AttachEntity(entity) as RegistrationMRNHistory;
		}

		virtual public void Combine(RegistrationMRNHistoryCollection collection)
		{
			base.Combine(collection);
		}

		new public RegistrationMRNHistory this[int index]
		{
			get
			{
				return base[index] as RegistrationMRNHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(RegistrationMRNHistory);
		}
	}

	[Serializable]
	abstract public class esRegistrationMRNHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esRegistrationMRNHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esRegistrationMRNHistory()
		{
		}

		public esRegistrationMRNHistory(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String registrationNo, DateTime updateDateTime)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, updateDateTime);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, updateDateTime);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String registrationNo, DateTime updateDateTime)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(registrationNo, updateDateTime);
			else
				return LoadByPrimaryKeyStoredProcedure(registrationNo, updateDateTime);
		}

		private bool LoadByPrimaryKeyDynamic(String registrationNo, DateTime updateDateTime)
		{
			esRegistrationMRNHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.RegistrationNo == registrationNo, query.UpdateDateTime == updateDateTime);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String registrationNo, DateTime updateDateTime)
		{
			esParameters parms = new esParameters();
			parms.Add("RegistrationNo", registrationNo);
			parms.Add("UpdateDateTime", updateDateTime);
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
						case "UpdateDateTime": this.str.UpdateDateTime = (string)value; break;
						case "UpdateByUserID": this.str.UpdateByUserID = (string)value; break;
						case "FromPatientID": this.str.FromPatientID = (string)value; break;
						case "ToPatientID": this.str.ToPatientID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "UpdateDateTime":

							if (value == null || value is System.DateTime)
								this.UpdateDateTime = (System.DateTime?)value;
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
		/// Maps to RegistrationMRNHistory.RegistrationNo
		/// </summary>
		virtual public System.String RegistrationNo
		{
			get
			{
				return base.GetSystemString(RegistrationMRNHistoryMetadata.ColumnNames.RegistrationNo);
			}

			set
			{
				base.SetSystemString(RegistrationMRNHistoryMetadata.ColumnNames.RegistrationNo, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationMRNHistory.UpdateDateTime
		/// </summary>
		virtual public System.DateTime? UpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(RegistrationMRNHistoryMetadata.ColumnNames.UpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(RegistrationMRNHistoryMetadata.ColumnNames.UpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationMRNHistory.UpdateByUserID
		/// </summary>
		virtual public System.String UpdateByUserID
		{
			get
			{
				return base.GetSystemString(RegistrationMRNHistoryMetadata.ColumnNames.UpdateByUserID);
			}

			set
			{
				base.SetSystemString(RegistrationMRNHistoryMetadata.ColumnNames.UpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationMRNHistory.FromPatientID
		/// </summary>
		virtual public System.String FromPatientID
		{
			get
			{
				return base.GetSystemString(RegistrationMRNHistoryMetadata.ColumnNames.FromPatientID);
			}

			set
			{
				base.SetSystemString(RegistrationMRNHistoryMetadata.ColumnNames.FromPatientID, value);
			}
		}
		/// <summary>
		/// Maps to RegistrationMRNHistory.ToPatientID
		/// </summary>
		virtual public System.String ToPatientID
		{
			get
			{
				return base.GetSystemString(RegistrationMRNHistoryMetadata.ColumnNames.ToPatientID);
			}

			set
			{
				base.SetSystemString(RegistrationMRNHistoryMetadata.ColumnNames.ToPatientID, value);
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
			public esStrings(esRegistrationMRNHistory entity)
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
			public System.String UpdateDateTime
			{
				get
				{
					System.DateTime? data = entity.UpdateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UpdateDateTime = null;
					else entity.UpdateDateTime = Convert.ToDateTime(value);
				}
			}
			public System.String UpdateByUserID
			{
				get
				{
					System.String data = entity.UpdateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UpdateByUserID = null;
					else entity.UpdateByUserID = Convert.ToString(value);
				}
			}
			public System.String FromPatientID
			{
				get
				{
					System.String data = entity.FromPatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.FromPatientID = null;
					else entity.FromPatientID = Convert.ToString(value);
				}
			}
			public System.String ToPatientID
			{
				get
				{
					System.String data = entity.ToPatientID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ToPatientID = null;
					else entity.ToPatientID = Convert.ToString(value);
				}
			}
			private esRegistrationMRNHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esRegistrationMRNHistoryQuery query)
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
				throw new Exception("esRegistrationMRNHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class RegistrationMRNHistory : esRegistrationMRNHistory
	{
	}

	[Serializable]
	abstract public class esRegistrationMRNHistoryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return RegistrationMRNHistoryMetadata.Meta();
			}
		}

		public esQueryItem RegistrationNo
		{
			get
			{
				return new esQueryItem(this, RegistrationMRNHistoryMetadata.ColumnNames.RegistrationNo, esSystemType.String);
			}
		}

		public esQueryItem UpdateDateTime
		{
			get
			{
				return new esQueryItem(this, RegistrationMRNHistoryMetadata.ColumnNames.UpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem UpdateByUserID
		{
			get
			{
				return new esQueryItem(this, RegistrationMRNHistoryMetadata.ColumnNames.UpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem FromPatientID
		{
			get
			{
				return new esQueryItem(this, RegistrationMRNHistoryMetadata.ColumnNames.FromPatientID, esSystemType.String);
			}
		}

		public esQueryItem ToPatientID
		{
			get
			{
				return new esQueryItem(this, RegistrationMRNHistoryMetadata.ColumnNames.ToPatientID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("RegistrationMRNHistoryCollection")]
	public partial class RegistrationMRNHistoryCollection : esRegistrationMRNHistoryCollection, IEnumerable<RegistrationMRNHistory>
	{
		public RegistrationMRNHistoryCollection()
		{

		}

		public static implicit operator List<RegistrationMRNHistory>(RegistrationMRNHistoryCollection coll)
		{
			List<RegistrationMRNHistory> list = new List<RegistrationMRNHistory>();

			foreach (RegistrationMRNHistory emp in coll)
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
				return RegistrationMRNHistoryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationMRNHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new RegistrationMRNHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new RegistrationMRNHistory();
		}

		#endregion

		[BrowsableAttribute(false)]
		public RegistrationMRNHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationMRNHistoryQuery();
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
		public bool Load(RegistrationMRNHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public RegistrationMRNHistory AddNew()
		{
			RegistrationMRNHistory entity = base.AddNewEntity() as RegistrationMRNHistory;

			return entity;
		}
		public RegistrationMRNHistory FindByPrimaryKey(String registrationNo, DateTime updateDateTime)
		{
			return base.FindByPrimaryKey(registrationNo, updateDateTime) as RegistrationMRNHistory;
		}

		#region IEnumerable< RegistrationMRNHistory> Members

		IEnumerator<RegistrationMRNHistory> IEnumerable<RegistrationMRNHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as RegistrationMRNHistory;
			}
		}

		#endregion

		private RegistrationMRNHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'RegistrationMRNHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("RegistrationMRNHistory ({RegistrationNo, UpdateDateTime})")]
	[Serializable]
	public partial class RegistrationMRNHistory : esRegistrationMRNHistory
	{
		public RegistrationMRNHistory()
		{
		}

		public RegistrationMRNHistory(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RegistrationMRNHistoryMetadata.Meta();
			}
		}

		override protected esRegistrationMRNHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RegistrationMRNHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public RegistrationMRNHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RegistrationMRNHistoryQuery();
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
		public bool Load(RegistrationMRNHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private RegistrationMRNHistoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class RegistrationMRNHistoryQuery : esRegistrationMRNHistoryQuery
	{
		public RegistrationMRNHistoryQuery()
		{

		}

		public RegistrationMRNHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "RegistrationMRNHistoryQuery";
		}
	}

	[Serializable]
	public partial class RegistrationMRNHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RegistrationMRNHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RegistrationMRNHistoryMetadata.ColumnNames.RegistrationNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMRNHistoryMetadata.PropertyNames.RegistrationNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMRNHistoryMetadata.ColumnNames.UpdateDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = RegistrationMRNHistoryMetadata.PropertyNames.UpdateDateTime;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMRNHistoryMetadata.ColumnNames.UpdateByUserID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMRNHistoryMetadata.PropertyNames.UpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMRNHistoryMetadata.ColumnNames.FromPatientID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMRNHistoryMetadata.PropertyNames.FromPatientID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(RegistrationMRNHistoryMetadata.ColumnNames.ToPatientID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = RegistrationMRNHistoryMetadata.PropertyNames.ToPatientID;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public RegistrationMRNHistoryMetadata Meta()
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
			public const string UpdateDateTime = "UpdateDateTime";
			public const string UpdateByUserID = "UpdateByUserID";
			public const string FromPatientID = "FromPatientID";
			public const string ToPatientID = "ToPatientID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string RegistrationNo = "RegistrationNo";
			public const string UpdateDateTime = "UpdateDateTime";
			public const string UpdateByUserID = "UpdateByUserID";
			public const string FromPatientID = "FromPatientID";
			public const string ToPatientID = "ToPatientID";
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
			lock (typeof(RegistrationMRNHistoryMetadata))
			{
				if (RegistrationMRNHistoryMetadata.mapDelegates == null)
				{
					RegistrationMRNHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (RegistrationMRNHistoryMetadata.meta == null)
				{
					RegistrationMRNHistoryMetadata.meta = new RegistrationMRNHistoryMetadata();
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
				meta.AddTypeMap("UpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("UpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("FromPatientID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ToPatientID", new esTypeMap("varchar", "System.String"));


				meta.Source = "RegistrationMRNHistory";
				meta.Destination = "RegistrationMRNHistory";
				meta.spInsert = "proc_RegistrationMRNHistoryInsert";
				meta.spUpdate = "proc_RegistrationMRNHistoryUpdate";
				meta.spDelete = "proc_RegistrationMRNHistoryDelete";
				meta.spLoadAll = "proc_RegistrationMRNHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_RegistrationMRNHistoryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RegistrationMRNHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
