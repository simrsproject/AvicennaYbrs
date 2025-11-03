/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 6/2/2020 12:17:35 PM
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
	abstract public class esMedicalRecordFileBorrowedHistoryCollection : esEntityCollectionWAuditLog
	{
		public esMedicalRecordFileBorrowedHistoryCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "MedicalRecordFileBorrowedHistoryCollection";
		}

		#region Query Logic
		protected void InitQuery(esMedicalRecordFileBorrowedHistoryQuery query)
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
			this.InitQuery(query as esMedicalRecordFileBorrowedHistoryQuery);
		}
		#endregion

		virtual public MedicalRecordFileBorrowedHistory DetachEntity(MedicalRecordFileBorrowedHistory entity)
		{
			return base.DetachEntity(entity) as MedicalRecordFileBorrowedHistory;
		}

		virtual public MedicalRecordFileBorrowedHistory AttachEntity(MedicalRecordFileBorrowedHistory entity)
		{
			return base.AttachEntity(entity) as MedicalRecordFileBorrowedHistory;
		}

		virtual public void Combine(MedicalRecordFileBorrowedHistoryCollection collection)
		{
			base.Combine(collection);
		}

		new public MedicalRecordFileBorrowedHistory this[int index]
		{
			get
			{
				return base[index] as MedicalRecordFileBorrowedHistory;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(MedicalRecordFileBorrowedHistory);
		}
	}

	[Serializable]
	abstract public class esMedicalRecordFileBorrowedHistory : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esMedicalRecordFileBorrowedHistoryQuery GetDynamicQuery()
		{
			return null;
		}

		public esMedicalRecordFileBorrowedHistory()
		{
		}

		public esMedicalRecordFileBorrowedHistory(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String transactionNo, DateTime lastUpdateDateTime)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, lastUpdateDateTime);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, lastUpdateDateTime);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String transactionNo, DateTime lastUpdateDateTime)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, lastUpdateDateTime);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, lastUpdateDateTime);
		}

		private bool LoadByPrimaryKeyDynamic(String transactionNo, DateTime lastUpdateDateTime)
		{
			esMedicalRecordFileBorrowedHistoryQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.LastUpdateDateTime == lastUpdateDateTime);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String transactionNo, DateTime lastUpdateDateTime)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo", transactionNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
						case "Duration": this.str.Duration = (string)value; break;
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
						case "Duration":

							if (value == null || value is System.Int16)
								this.Duration = (System.Int16?)value;
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
		/// Maps to MedicalRecordFileBorrowedHistory.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.TransactionNo);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.TransactionNo, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileBorrowedHistory.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileBorrowedHistory.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		/// <summary>
		/// Maps to MedicalRecordFileBorrowedHistory.Duration
		/// </summary>
		virtual public System.Int16? Duration
		{
			get
			{
				return base.GetSystemInt16(MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.Duration);
			}

			set
			{
				base.SetSystemInt16(MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.Duration, value);
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
			public esStrings(esMedicalRecordFileBorrowedHistory entity)
			{
				this.entity = entity;
			}
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
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
			public System.String Duration
			{
				get
				{
					System.Int16? data = entity.Duration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Duration = null;
					else entity.Duration = Convert.ToInt16(value);
				}
			}
			private esMedicalRecordFileBorrowedHistory entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esMedicalRecordFileBorrowedHistoryQuery query)
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
				throw new Exception("esMedicalRecordFileBorrowedHistory can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class MedicalRecordFileBorrowedHistory : esMedicalRecordFileBorrowedHistory
	{
	}

	[Serializable]
	abstract public class esMedicalRecordFileBorrowedHistoryQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return MedicalRecordFileBorrowedHistoryMetadata.Meta();
			}
		}

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

		public esQueryItem Duration
		{
			get
			{
				return new esQueryItem(this, MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.Duration, esSystemType.Int16);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("MedicalRecordFileBorrowedHistoryCollection")]
	public partial class MedicalRecordFileBorrowedHistoryCollection : esMedicalRecordFileBorrowedHistoryCollection, IEnumerable<MedicalRecordFileBorrowedHistory>
	{
		public MedicalRecordFileBorrowedHistoryCollection()
		{

		}

		public static implicit operator List<MedicalRecordFileBorrowedHistory>(MedicalRecordFileBorrowedHistoryCollection coll)
		{
			List<MedicalRecordFileBorrowedHistory> list = new List<MedicalRecordFileBorrowedHistory>();

			foreach (MedicalRecordFileBorrowedHistory emp in coll)
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
				return MedicalRecordFileBorrowedHistoryMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalRecordFileBorrowedHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new MedicalRecordFileBorrowedHistory(row);
		}

		override protected esEntity CreateEntity()
		{
			return new MedicalRecordFileBorrowedHistory();
		}

		#endregion

		[BrowsableAttribute(false)]
		public MedicalRecordFileBorrowedHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalRecordFileBorrowedHistoryQuery();
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
		public bool Load(MedicalRecordFileBorrowedHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public MedicalRecordFileBorrowedHistory AddNew()
		{
			MedicalRecordFileBorrowedHistory entity = base.AddNewEntity() as MedicalRecordFileBorrowedHistory;

			return entity;
		}
		public MedicalRecordFileBorrowedHistory FindByPrimaryKey(String transactionNo, DateTime lastUpdateDateTime)
		{
			return base.FindByPrimaryKey(transactionNo, lastUpdateDateTime) as MedicalRecordFileBorrowedHistory;
		}

		#region IEnumerable< MedicalRecordFileBorrowedHistory> Members

		IEnumerator<MedicalRecordFileBorrowedHistory> IEnumerable<MedicalRecordFileBorrowedHistory>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as MedicalRecordFileBorrowedHistory;
			}
		}

		#endregion

		private MedicalRecordFileBorrowedHistoryQuery query;
	}


	/// <summary>
	/// Encapsulates the 'MedicalRecordFileBorrowedHistory' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("MedicalRecordFileBorrowedHistory ({TransactionNo, LastUpdateDateTime})")]
	[Serializable]
	public partial class MedicalRecordFileBorrowedHistory : esMedicalRecordFileBorrowedHistory
	{
		public MedicalRecordFileBorrowedHistory()
		{
		}

		public MedicalRecordFileBorrowedHistory(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return MedicalRecordFileBorrowedHistoryMetadata.Meta();
			}
		}

		override protected esMedicalRecordFileBorrowedHistoryQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new MedicalRecordFileBorrowedHistoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public MedicalRecordFileBorrowedHistoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new MedicalRecordFileBorrowedHistoryQuery();
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
		public bool Load(MedicalRecordFileBorrowedHistoryQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private MedicalRecordFileBorrowedHistoryQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class MedicalRecordFileBorrowedHistoryQuery : esMedicalRecordFileBorrowedHistoryQuery
	{
		public MedicalRecordFileBorrowedHistoryQuery()
		{

		}

		public MedicalRecordFileBorrowedHistoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "MedicalRecordFileBorrowedHistoryQuery";
		}
	}

	[Serializable]
	public partial class MedicalRecordFileBorrowedHistoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected MedicalRecordFileBorrowedHistoryMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileBorrowedHistoryMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.LastUpdateDateTime, 1, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = MedicalRecordFileBorrowedHistoryMetadata.PropertyNames.LastUpdateDateTime;
			c.IsInPrimaryKey = true;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.LastUpdateByUserID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = MedicalRecordFileBorrowedHistoryMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			_columns.Add(c);

			c = new esColumnMetadata(MedicalRecordFileBorrowedHistoryMetadata.ColumnNames.Duration, 3, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = MedicalRecordFileBorrowedHistoryMetadata.PropertyNames.Duration;
			c.NumericPrecision = 5;
			_columns.Add(c);


		}
		#endregion

		static public MedicalRecordFileBorrowedHistoryMetadata Meta()
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
			public const string TransactionNo = "TransactionNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Duration = "Duration";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string TransactionNo = "TransactionNo";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
			public const string Duration = "Duration";
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
			lock (typeof(MedicalRecordFileBorrowedHistoryMetadata))
			{
				if (MedicalRecordFileBorrowedHistoryMetadata.mapDelegates == null)
				{
					MedicalRecordFileBorrowedHistoryMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (MedicalRecordFileBorrowedHistoryMetadata.meta == null)
				{
					MedicalRecordFileBorrowedHistoryMetadata.meta = new MedicalRecordFileBorrowedHistoryMetadata();
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

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Duration", new esTypeMap("smallint", "System.Int16"));


				meta.Source = "MedicalRecordFileBorrowedHistory";
				meta.Destination = "MedicalRecordFileBorrowedHistory";
				meta.spInsert = "proc_MedicalRecordFileBorrowedHistoryInsert";
				meta.spUpdate = "proc_MedicalRecordFileBorrowedHistoryUpdate";
				meta.spDelete = "proc_MedicalRecordFileBorrowedHistoryDelete";
				meta.spLoadAll = "proc_MedicalRecordFileBorrowedHistoryLoadAll";
				meta.spLoadByPrimaryKey = "proc_MedicalRecordFileBorrowedHistoryLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private MedicalRecordFileBorrowedHistoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
