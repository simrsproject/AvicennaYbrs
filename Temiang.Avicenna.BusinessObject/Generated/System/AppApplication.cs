/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 8/6/2022 7:03:11 PM
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
	abstract public class esAppApplicationCollection : esEntityCollectionWAuditLog
	{
		public esAppApplicationCollection()
		{

		}


		protected override string GetCollectionName()
		{
			return "AppApplicationCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppApplicationQuery query)
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
			this.InitQuery(query as esAppApplicationQuery);
		}
		#endregion

		virtual public AppApplication DetachEntity(AppApplication entity)
		{
			return base.DetachEntity(entity) as AppApplication;
		}

		virtual public AppApplication AttachEntity(AppApplication entity)
		{
			return base.AttachEntity(entity) as AppApplication;
		}

		virtual public void Combine(AppApplicationCollection collection)
		{
			base.Combine(collection);
		}

		new public AppApplication this[int index]
		{
			get
			{
				return base[index] as AppApplication;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppApplication);
		}
	}

	[Serializable]
	abstract public class esAppApplication : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppApplicationQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppApplication()
		{
		}

		public esAppApplication(DataRow row)
			: base(row)
		{
		}


		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String applicationID)
		{
			if (this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicationID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicationID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String applicationID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(applicationID);
			else
				return LoadByPrimaryKeyStoredProcedure(applicationID);
		}

		private bool LoadByPrimaryKeyDynamic(String applicationID)
		{
			esAppApplicationQuery query = this.GetDynamicQuery();
			query.Where(query.ApplicationID == applicationID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(String applicationID)
		{
			esParameters parms = new esParameters();
			parms.Add("ApplicationID", applicationID);
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
						case "ApplicationID": this.str.ApplicationID = (string)value; break;
						case "BaseUrl": this.str.BaseUrl = (string)value; break;
						case "BinFolderLocation": this.str.BinFolderLocation = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{

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
		/// Maps to AppApplication.ApplicationID
		/// </summary>
		virtual public System.String ApplicationID
		{
			get
			{
				return base.GetSystemString(AppApplicationMetadata.ColumnNames.ApplicationID);
			}

			set
			{
				base.SetSystemString(AppApplicationMetadata.ColumnNames.ApplicationID, value);
			}
		}
		/// <summary>
		/// Maps to AppApplication.BaseUrl
		/// </summary>
		virtual public System.String BaseUrl
		{
			get
			{
				return base.GetSystemString(AppApplicationMetadata.ColumnNames.BaseUrl);
			}

			set
			{
				base.SetSystemString(AppApplicationMetadata.ColumnNames.BaseUrl, value);
			}
		}
		/// <summary>
		/// Maps to AppApplication.BinFolderLocation
		/// </summary>
		virtual public System.String BinFolderLocation
		{
			get
			{
				return base.GetSystemString(AppApplicationMetadata.ColumnNames.BinFolderLocation);
			}

			set
			{
				base.SetSystemString(AppApplicationMetadata.ColumnNames.BinFolderLocation, value);
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
			public esStrings(esAppApplication entity)
			{
				this.entity = entity;
			}
			public System.String ApplicationID
			{
				get
				{
					System.String data = entity.ApplicationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ApplicationID = null;
					else entity.ApplicationID = Convert.ToString(value);
				}
			}
			public System.String BaseUrl
			{
				get
				{
					System.String data = entity.BaseUrl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BaseUrl = null;
					else entity.BaseUrl = Convert.ToString(value);
				}
			}
			public System.String BinFolderLocation
			{
				get
				{
					System.String data = entity.BinFolderLocation;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BinFolderLocation = null;
					else entity.BinFolderLocation = Convert.ToString(value);
				}
			}
			private esAppApplication entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppApplicationQuery query)
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
				throw new Exception("esAppApplication can only hold one record of data");
			}

			return dataFound;
		}
		#endregion

		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppApplication : esAppApplication
	{
	}

	[Serializable]
	abstract public class esAppApplicationQuery : esDynamicQuery
	{

		override protected IMetadata Meta
		{
			get
			{
				return AppApplicationMetadata.Meta();
			}
		}

		public esQueryItem ApplicationID
		{
			get
			{
				return new esQueryItem(this, AppApplicationMetadata.ColumnNames.ApplicationID, esSystemType.String);
			}
		}

		public esQueryItem BaseUrl
		{
			get
			{
				return new esQueryItem(this, AppApplicationMetadata.ColumnNames.BaseUrl, esSystemType.String);
			}
		}

		public esQueryItem BinFolderLocation
		{
			get
			{
				return new esQueryItem(this, AppApplicationMetadata.ColumnNames.BinFolderLocation, esSystemType.String);
			}
		}

	}

	[System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppApplicationCollection")]
	public partial class AppApplicationCollection : esAppApplicationCollection, IEnumerable<AppApplication>
	{
		public AppApplicationCollection()
		{

		}

		public static implicit operator List<AppApplication>(AppApplicationCollection coll)
		{
			List<AppApplication> list = new List<AppApplication>();

			foreach (AppApplication emp in coll)
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
				return AppApplicationMetadata.Meta();
			}
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppApplicationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppApplication(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppApplication();
		}

		#endregion

		[BrowsableAttribute(false)]
		public AppApplicationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppApplicationQuery();
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
		public bool Load(AppApplicationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppApplication AddNew()
		{
			AppApplication entity = base.AddNewEntity() as AppApplication;

			return entity;
		}
		public AppApplication FindByPrimaryKey(String applicationID)
		{
			return base.FindByPrimaryKey(applicationID) as AppApplication;
		}

		#region IEnumerable< AppApplication> Members

		IEnumerator<AppApplication> IEnumerable<AppApplication>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while (iterator.MoveNext())
			{
				yield return iterator.Current as AppApplication;
			}
		}

		#endregion

		private AppApplicationQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppApplication' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppApplication ({ApplicationID})")]
	[Serializable]
	public partial class AppApplication : esAppApplication
	{
		public AppApplication()
		{
		}

		public AppApplication(DataRow row)
			: base(row)
		{
		}

		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppApplicationMetadata.Meta();
			}
		}

		override protected esAppApplicationQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppApplicationQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion

		[BrowsableAttribute(false)]
		public AppApplicationQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppApplicationQuery();
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
		public bool Load(AppApplicationQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}

		private AppApplicationQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppApplicationQuery : esAppApplicationQuery
	{
		public AppApplicationQuery()
		{

		}

		public AppApplicationQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}

		override protected string GetQueryName()
		{
			return "AppApplicationQuery";
		}
	}

	[Serializable]
	public partial class AppApplicationMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppApplicationMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppApplicationMetadata.ColumnNames.ApplicationID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppApplicationMetadata.PropertyNames.ApplicationID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);

			c = new esColumnMetadata(AppApplicationMetadata.ColumnNames.BaseUrl, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppApplicationMetadata.PropertyNames.BaseUrl;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);

			c = new esColumnMetadata(AppApplicationMetadata.ColumnNames.BinFolderLocation, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = AppApplicationMetadata.PropertyNames.BinFolderLocation;
			c.CharacterMaxLength = 200;
			c.IsNullable = true;
			_columns.Add(c);


		}
		#endregion

		static public AppApplicationMetadata Meta()
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
			public const string ApplicationID = "ApplicationID";
			public const string BaseUrl = "BaseUrl";
			public const string BinFolderLocation = "BinFolderLocation";
		}
		#endregion

		#region PropertyNames
		public class PropertyNames
		{
			public const string ApplicationID = "ApplicationID";
			public const string BaseUrl = "BaseUrl";
			public const string BinFolderLocation = "BinFolderLocation";
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
			lock (typeof(AppApplicationMetadata))
			{
				if (AppApplicationMetadata.mapDelegates == null)
				{
					AppApplicationMetadata.mapDelegates = new Dictionary<string, MapToMeta>();
				}

				if (AppApplicationMetadata.meta == null)
				{
					AppApplicationMetadata.meta = new AppApplicationMetadata();
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

				meta.AddTypeMap("ApplicationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BaseUrl", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BinFolderLocation", new esTypeMap("varchar", "System.String"));


				meta.Source = "AppApplication";
				meta.Destination = "AppApplication";
				meta.spInsert = "proc_AppApplicationInsert";
				meta.spUpdate = "proc_AppApplicationUpdate";
				meta.spDelete = "proc_AppApplicationDelete";
				meta.spLoadAll = "proc_AppApplicationLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppApplicationLoadByPrimaryKey";

				this._providerMetadataMaps["esDefault"] = meta;
			}

			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppApplicationMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}
