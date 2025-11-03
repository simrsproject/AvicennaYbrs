/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 4/6/2021 4:02:45 PM
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
	abstract public class esSanitationWasteItemBalanceCollection : esEntityCollectionWAuditLog
	{
		public esSanitationWasteItemBalanceCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SanitationWasteItemBalanceCollection";
		}

		#region Query Logic
		protected void InitQuery(esSanitationWasteItemBalanceQuery query)
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
			this.InitQuery(query as esSanitationWasteItemBalanceQuery);
		}
		#endregion

		virtual public SanitationWasteItemBalance DetachEntity(SanitationWasteItemBalance entity)
		{
			return base.DetachEntity(entity) as SanitationWasteItemBalance;
		}

		virtual public SanitationWasteItemBalance AttachEntity(SanitationWasteItemBalance entity)
		{
			return base.AttachEntity(entity) as SanitationWasteItemBalance;
		}

		virtual public void Combine(SanitationWasteItemBalanceCollection collection)
		{
			base.Combine(collection);
		}

		new public SanitationWasteItemBalance this[int index]
		{
			get
			{
				return base[index] as SanitationWasteItemBalance;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SanitationWasteItemBalance);
		}
	}

	[Serializable]
	abstract public class esSanitationWasteItemBalance : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSanitationWasteItemBalanceQuery GetDynamicQuery()
		{
			return null;
		}

		public esSanitationWasteItemBalance()
		{
		}

		public esSanitationWasteItemBalance(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String sRWasteType)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRWasteType);
			else
				return LoadByPrimaryKeyStoredProcedure(sRWasteType);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String sRWasteType)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(sRWasteType);
			else
				return LoadByPrimaryKeyStoredProcedure(sRWasteType);
		}

		private bool LoadByPrimaryKeyDynamic(String sRWasteType)
		{
			esSanitationWasteItemBalanceQuery query = this.GetDynamicQuery();
			query.Where(query.SRWasteType == sRWasteType);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String sRWasteType)
		{
			esParameters parms = new esParameters();
			parms.Add("SRWasteType", sRWasteType);
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
						case "SRWasteType": this.str.SRWasteType = (string)value; break;
						case "Balance": this.str.Balance = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "Balance":

							if (value == null || value is System.Decimal)
								this.Balance = (System.Decimal?)value;
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
		/// Maps to SanitationWasteItemBalance.SRWasteType
		/// </summary>
		virtual public System.String SRWasteType
		{
			get
			{
				return base.GetSystemString(SanitationWasteItemBalanceMetadata.ColumnNames.SRWasteType);
			}

			set
			{
				base.SetSystemString(SanitationWasteItemBalanceMetadata.ColumnNames.SRWasteType, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteItemBalance.Balance
		/// </summary>
		virtual public System.Decimal? Balance
		{
			get
			{
				return base.GetSystemDecimal(SanitationWasteItemBalanceMetadata.ColumnNames.Balance);
			}

			set
			{
				base.SetSystemDecimal(SanitationWasteItemBalanceMetadata.ColumnNames.Balance, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteItemBalance.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SanitationWasteItemBalanceMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SanitationWasteItemBalanceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SanitationWasteItemBalance.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SanitationWasteItemBalanceMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SanitationWasteItemBalanceMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSanitationWasteItemBalance entity)
			{
				this.entity = entity;
			}
			public System.String SRWasteType
			{
				get
				{
					System.String data = entity.SRWasteType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRWasteType = null;
					else entity.SRWasteType = Convert.ToString(value);
				}
			}
			public System.String Balance
			{
				get
				{
					System.Decimal? data = entity.Balance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Balance = null;
					else entity.Balance = Convert.ToDecimal(value);
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
			private esSanitationWasteItemBalance entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSanitationWasteItemBalanceQuery query)
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
				throw new Exception("esSanitationWasteItemBalance can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SanitationWasteItemBalance : esSanitationWasteItemBalance
	{
	}

	[Serializable]
	abstract public class esSanitationWasteItemBalanceQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SanitationWasteItemBalanceMetadata.Meta();
			}
		}

		public esQueryItem SRWasteType
		{
			get
			{
				return new esQueryItem(this, SanitationWasteItemBalanceMetadata.ColumnNames.SRWasteType, esSystemType.String);
			}
		}

		public esQueryItem Balance
		{
			get
			{
				return new esQueryItem(this, SanitationWasteItemBalanceMetadata.ColumnNames.Balance, esSystemType.Decimal);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SanitationWasteItemBalanceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SanitationWasteItemBalanceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SanitationWasteItemBalanceCollection")]
	public partial class SanitationWasteItemBalanceCollection : esSanitationWasteItemBalanceCollection, IEnumerable<SanitationWasteItemBalance>
	{
		public SanitationWasteItemBalanceCollection()
		{

		}

		public static implicit operator List<SanitationWasteItemBalance>(SanitationWasteItemBalanceCollection coll)
		{
			List<SanitationWasteItemBalance> list = new List<SanitationWasteItemBalance>();

			foreach (SanitationWasteItemBalance emp in coll)
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
				return SanitationWasteItemBalanceMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationWasteItemBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SanitationWasteItemBalance(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SanitationWasteItemBalance();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SanitationWasteItemBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationWasteItemBalanceQuery();
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
		public bool Load(SanitationWasteItemBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SanitationWasteItemBalance AddNew()
		{
			SanitationWasteItemBalance entity = base.AddNewEntity() as SanitationWasteItemBalance;

			return entity;
		}
		public SanitationWasteItemBalance FindByPrimaryKey(String sRWasteType)
		{
			return base.FindByPrimaryKey(sRWasteType) as SanitationWasteItemBalance;
		}

		#region IEnumerable< SanitationWasteItemBalance> Members

		IEnumerator<SanitationWasteItemBalance> IEnumerable<SanitationWasteItemBalance>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SanitationWasteItemBalance;
			}
		}

		#endregion

		private SanitationWasteItemBalanceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SanitationWasteItemBalance' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SanitationWasteItemBalance ({SRWasteType})")]
	[Serializable]
	public partial class SanitationWasteItemBalance : esSanitationWasteItemBalance
	{
		public SanitationWasteItemBalance()
		{
		}

		public SanitationWasteItemBalance(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SanitationWasteItemBalanceMetadata.Meta();
			}
		}

		override protected esSanitationWasteItemBalanceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SanitationWasteItemBalanceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SanitationWasteItemBalanceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SanitationWasteItemBalanceQuery();
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
		public bool Load(SanitationWasteItemBalanceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SanitationWasteItemBalanceQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SanitationWasteItemBalanceQuery : esSanitationWasteItemBalanceQuery
	{
		public SanitationWasteItemBalanceQuery()
		{

		}

		public SanitationWasteItemBalanceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SanitationWasteItemBalanceQuery";
		}
	}

	[Serializable]
	public partial class SanitationWasteItemBalanceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SanitationWasteItemBalanceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SanitationWasteItemBalanceMetadata.ColumnNames.SRWasteType, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationWasteItemBalanceMetadata.PropertyNames.SRWasteType;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteItemBalanceMetadata.ColumnNames.Balance, 1, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = SanitationWasteItemBalanceMetadata.PropertyNames.Balance;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteItemBalanceMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SanitationWasteItemBalanceMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SanitationWasteItemBalanceMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SanitationWasteItemBalanceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SanitationWasteItemBalanceMetadata Meta()
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
			public const string SRWasteType = "SRWasteType";
			public const string Balance = "Balance";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SRWasteType = "SRWasteType";
			public const string Balance = "Balance";
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
			lock (typeof(SanitationWasteItemBalanceMetadata))
			{
				if (SanitationWasteItemBalanceMetadata.mapDelegates == null)
				{
					SanitationWasteItemBalanceMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SanitationWasteItemBalanceMetadata.meta == null)
				{
					SanitationWasteItemBalanceMetadata.meta = new SanitationWasteItemBalanceMetadata();
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

				meta.AddTypeMap("SRWasteType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Balance", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "SanitationWasteItemBalance";
				meta.Destination = "SanitationWasteItemBalance";
				meta.spInsert = "proc_SanitationWasteItemBalanceInsert";
				meta.spUpdate = "proc_SanitationWasteItemBalanceUpdate";
				meta.spDelete = "proc_SanitationWasteItemBalanceDelete";
				meta.spLoadAll = "proc_SanitationWasteItemBalanceLoadAll";
				meta.spLoadByPrimaryKey = "proc_SanitationWasteItemBalanceLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SanitationWasteItemBalanceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
