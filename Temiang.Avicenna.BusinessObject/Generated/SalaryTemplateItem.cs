/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 10/14/2020 2:57:38 PM
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
	abstract public class esSalaryTemplateItemCollection : esEntityCollectionWAuditLog
	{
		public esSalaryTemplateItemCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "SalaryTemplateItemCollection";
		}

		#region Query Logic
		protected void InitQuery(esSalaryTemplateItemQuery query)
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
			this.InitQuery(query as esSalaryTemplateItemQuery);
		}
		#endregion

		virtual public SalaryTemplateItem DetachEntity(SalaryTemplateItem entity)
		{
			return base.DetachEntity(entity) as SalaryTemplateItem;
		}

		virtual public SalaryTemplateItem AttachEntity(SalaryTemplateItem entity)
		{
			return base.AttachEntity(entity) as SalaryTemplateItem;
		}

		virtual public void Combine(SalaryTemplateItemCollection collection)
		{
			base.Combine(collection);
		}

		new public SalaryTemplateItem this[int index]
		{
			get
			{
				return base[index] as SalaryTemplateItem;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(SalaryTemplateItem);
		}
	}

	[Serializable]
	abstract public class esSalaryTemplateItem : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esSalaryTemplateItemQuery GetDynamicQuery()
		{
			return null;
		}

		public esSalaryTemplateItem()
		{
		}

		public esSalaryTemplateItem(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String salaryTemplateID, Int32 salaryComponentID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(salaryTemplateID, salaryComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(salaryTemplateID, salaryComponentID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String salaryTemplateID, Int32 salaryComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(salaryTemplateID, salaryComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(salaryTemplateID, salaryComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(String salaryTemplateID, Int32 salaryComponentID)
		{
			esSalaryTemplateItemQuery query = this.GetDynamicQuery();
			query.Where(query.SalaryTemplateID == salaryTemplateID, query.SalaryComponentID == salaryComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String salaryTemplateID, Int32 salaryComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("SalaryTemplateID", salaryTemplateID);
			parms.Add("SalaryComponentID", salaryComponentID);
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
						case "SalaryTemplateID": this.str.SalaryTemplateID = (string)value; break;
						case "SalaryComponentID": this.str.SalaryComponentID = (string)value; break;
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{
						case "SalaryComponentID":

							if (value == null || value is System.Int32)
								this.SalaryComponentID = (System.Int32?)value;
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
		/// Maps to SalaryTemplateItem.SalaryTemplateID
		/// </summary>
		virtual public System.String SalaryTemplateID
		{
			get
			{
				return base.GetSystemString(SalaryTemplateItemMetadata.ColumnNames.SalaryTemplateID);
			}

			set
			{
				base.SetSystemString(SalaryTemplateItemMetadata.ColumnNames.SalaryTemplateID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryTemplateItem.SalaryComponentID
		/// </summary>
		virtual public System.Int32? SalaryComponentID
		{
			get
			{
				return base.GetSystemInt32(SalaryTemplateItemMetadata.ColumnNames.SalaryComponentID);
			}

			set
			{
				base.SetSystemInt32(SalaryTemplateItemMetadata.ColumnNames.SalaryComponentID, value);
			}
		}
		/// <summary>
		/// Maps to SalaryTemplateItem.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(SalaryTemplateItemMetadata.ColumnNames.LastUpdateDateTime);
			}

			set
			{
				base.SetSystemDateTime(SalaryTemplateItemMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		/// <summary>
		/// Maps to SalaryTemplateItem.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(SalaryTemplateItemMetadata.ColumnNames.LastUpdateByUserID);
			}

			set
			{
				base.SetSystemString(SalaryTemplateItemMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esSalaryTemplateItem entity)
			{
				this.entity = entity;
			}
			public System.String SalaryTemplateID
			{
				get
				{
					System.String data = entity.SalaryTemplateID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryTemplateID = null;
					else entity.SalaryTemplateID = Convert.ToString(value);
				}
			}
			public System.String SalaryComponentID
			{
				get
				{
					System.Int32? data = entity.SalaryComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SalaryComponentID = null;
					else entity.SalaryComponentID = Convert.ToInt32(value);
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
			private esSalaryTemplateItem entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esSalaryTemplateItemQuery query)
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
				throw new Exception("esSalaryTemplateItem can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class SalaryTemplateItem : esSalaryTemplateItem
	{
	}

	[Serializable]
	abstract public class esSalaryTemplateItemQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return SalaryTemplateItemMetadata.Meta();
			}
		}

		public esQueryItem SalaryTemplateID
		{
			get
			{
				return new esQueryItem(this, SalaryTemplateItemMetadata.ColumnNames.SalaryTemplateID, esSystemType.String);
			}
		}

		public esQueryItem SalaryComponentID
		{
			get
			{
				return new esQueryItem(this, SalaryTemplateItemMetadata.ColumnNames.SalaryComponentID, esSystemType.Int32);
			}
		}

		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, SalaryTemplateItemMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		}

		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, SalaryTemplateItemMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("SalaryTemplateItemCollection")]
	public partial class SalaryTemplateItemCollection : esSalaryTemplateItemCollection, IEnumerable<SalaryTemplateItem>
	{
		public SalaryTemplateItemCollection()
		{

		}

		public static implicit operator List<SalaryTemplateItem>(SalaryTemplateItemCollection coll)
		{
			List<SalaryTemplateItem> list = new List<SalaryTemplateItem>();

			foreach (SalaryTemplateItem emp in coll)
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
				return SalaryTemplateItemMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SalaryTemplateItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new SalaryTemplateItem(row);
		}

		override protected esEntity CreateEntity()
		{
			return new SalaryTemplateItem();
		}

		#endregion

		[BrowsableAttribute(false)]
		public SalaryTemplateItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SalaryTemplateItemQuery();
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
		public bool Load(SalaryTemplateItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public SalaryTemplateItem AddNew()
		{
			SalaryTemplateItem entity = base.AddNewEntity() as SalaryTemplateItem;

			return entity;
		}
		public SalaryTemplateItem FindByPrimaryKey(String salaryTemplateID, Int32 salaryComponentID)
		{
			return base.FindByPrimaryKey(salaryTemplateID, salaryComponentID) as SalaryTemplateItem;
		}

		#region IEnumerable< SalaryTemplateItem> Members

		IEnumerator<SalaryTemplateItem> IEnumerable<SalaryTemplateItem>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as SalaryTemplateItem;
			}
		}

		#endregion

		private SalaryTemplateItemQuery query;
	}


	/// <summary>
	/// Encapsulates the 'SalaryTemplateItem' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("SalaryTemplateItem ({SalaryTemplateID, SalaryComponentID})")]
	[Serializable]
	public partial class SalaryTemplateItem : esSalaryTemplateItem
	{
		public SalaryTemplateItem()
		{
		}

		public SalaryTemplateItem(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return SalaryTemplateItemMetadata.Meta();
			}
		}

		override protected esSalaryTemplateItemQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new SalaryTemplateItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public SalaryTemplateItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new SalaryTemplateItemQuery();
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
		public bool Load(SalaryTemplateItemQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private SalaryTemplateItemQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class SalaryTemplateItemQuery : esSalaryTemplateItemQuery
	{
		public SalaryTemplateItemQuery()
		{

		}

		public SalaryTemplateItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "SalaryTemplateItemQuery";
		}
	}

	[Serializable]
	public partial class SalaryTemplateItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected SalaryTemplateItemMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(SalaryTemplateItemMetadata.ColumnNames.SalaryTemplateID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryTemplateItemMetadata.PropertyNames.SalaryTemplateID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryTemplateItemMetadata.ColumnNames.SalaryComponentID, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = SalaryTemplateItemMetadata.PropertyNames.SalaryComponentID;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryTemplateItemMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = SalaryTemplateItemMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(SalaryTemplateItemMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = SalaryTemplateItemMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public SalaryTemplateItemMetadata Meta()
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
			public const string SalaryTemplateID = "SalaryTemplateID";
			public const string SalaryComponentID = "SalaryComponentID";
			public const string LastUpdateDateTime = "LastUpdateDateTime";
			public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string SalaryTemplateID = "SalaryTemplateID";
			public const string SalaryComponentID = "SalaryComponentID";
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
			lock (typeof(SalaryTemplateItemMetadata))
			{
				if (SalaryTemplateItemMetadata.mapDelegates == null)
				{
					SalaryTemplateItemMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (SalaryTemplateItemMetadata.meta == null)
				{
					SalaryTemplateItemMetadata.meta = new SalaryTemplateItemMetadata();
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

				meta.AddTypeMap("SalaryTemplateID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SalaryComponentID", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));


				meta.Source = "SalaryTemplateItem";
				meta.Destination = "SalaryTemplateItem";
				meta.spInsert = "proc_SalaryTemplateItemInsert";
				meta.spUpdate = "proc_SalaryTemplateItemUpdate";
				meta.spDelete = "proc_SalaryTemplateItemDelete";
				meta.spLoadAll = "proc_SalaryTemplateItemLoadAll";
				meta.spLoadByPrimaryKey = "proc_SalaryTemplateItemLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private SalaryTemplateItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
