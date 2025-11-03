/*
===============================================================================
                       Persistence Layer and Business Objects  
===============================================================================
                       Date Generated       : 9/22/2022 9:38:16 PM
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
	abstract public class esAppUserSettingsCollection : esEntityCollectionWAuditLog
	{
		public esAppUserSettingsCollection()
		{

		}
		
				
		protected override string GetCollectionName()
		{
			return "AppUserSettingsCollection";
		}		
		
		#region Query Logic
		protected void InitQuery(esAppUserSettingsQuery query)
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
			this.InitQuery(query as esAppUserSettingsQuery);
		}
		#endregion
			
		virtual public AppUserSettings DetachEntity(AppUserSettings entity)
		{
			return base.DetachEntity(entity) as AppUserSettings;
		}
		
		virtual public AppUserSettings AttachEntity(AppUserSettings entity)
		{
			return base.AttachEntity(entity) as AppUserSettings;
		}
		
		virtual public void Combine(AppUserSettingsCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AppUserSettings this[int index]
		{
			get
			{
				return base[index] as AppUserSettings;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppUserSettings);
		}
	}

	[Serializable]
	abstract public class esAppUserSettings : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppUserSettingsQuery GetDynamicQuery()
		{
			return null;
		}
		
		public esAppUserSettings()
		{
		}
	
		public esAppUserSettings(DataRow row)
			: base(row)
		{
		}
		
				
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(String userID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(userID);
			else
				return LoadByPrimaryKeyStoredProcedure(userID);
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
		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, String userID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(userID);
			else
				return LoadByPrimaryKeyStoredProcedure(userID);
		}
	
		private bool LoadByPrimaryKeyDynamic(String userID)
		{
			esAppUserSettingsQuery query = this.GetDynamicQuery();
			query.Where(query.UserID==userID);
			return query.Load();
		}
	
		private bool LoadByPrimaryKeyStoredProcedure(String userID)
		{
			esParameters parms = new esParameters();
			parms.Add("UserID",userID);
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
			if(this.Row == null) this.AddNew();
			
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{
						case "UserID": this.str.UserID = (string)value; break;
						case "QueueingCounterSetting": this.str.QueueingCounterSetting = (string)value; break;
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
			else if(this.Row.Table.Columns.Contains(name))
			{
				this.Row[name] = value;
			}
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}

		/// <summary>
		/// Maps to AppUserSettings.UserID
		/// </summary>
		virtual public System.String UserID
		{
			get
			{
				return base.GetSystemString(AppUserSettingsMetadata.ColumnNames.UserID);
			}
			
			set
			{
				base.SetSystemString(AppUserSettingsMetadata.ColumnNames.UserID, value);
			}
		}
		/// <summary>
		/// Maps to AppUserSettings.QueueingCounterSetting
		/// </summary>
		virtual public System.String QueueingCounterSetting
		{
			get
			{
				return base.GetSystemString(AppUserSettingsMetadata.ColumnNames.QueueingCounterSetting);
			}
			
			set
			{
				base.SetSystemString(AppUserSettingsMetadata.ColumnNames.QueueingCounterSetting, value);
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
		[BrowsableAttribute( false )]		
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
			public esStrings(esAppUserSettings entity)
			{
				this.entity = entity;
			}
			public System.String UserID
			{
				get
				{
					System.String data = entity.UserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UserID = null;
					else entity.UserID = Convert.ToString(value);
				}
			}
			public System.String QueueingCounterSetting
			{
				get
				{
					System.String data = entity.QueueingCounterSetting;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QueueingCounterSetting = null;
					else entity.QueueingCounterSetting = Convert.ToString(value);
				}
			}
			private esAppUserSettings entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppUserSettingsQuery query)
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
				throw new Exception("esAppUserSettings can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	public partial class AppUserSettings : esAppUserSettings
	{	
	}

	[Serializable]
	abstract public class esAppUserSettingsQuery : esDynamicQuery
	{
				
		override protected IMetadata Meta
		{
			get
			{
				return AppUserSettingsMetadata.Meta();
			}
		}	
			
		public esQueryItem UserID
		{
			get
			{
				return new esQueryItem(this, AppUserSettingsMetadata.ColumnNames.UserID, esSystemType.String);
			}
		} 
			
		public esQueryItem QueueingCounterSetting
		{
			get
			{
				return new esQueryItem(this, AppUserSettingsMetadata.ColumnNames.QueueingCounterSetting, esSystemType.String);
			}
		} 
	
	}

    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppUserSettingsCollection")]
	public partial class AppUserSettingsCollection : esAppUserSettingsCollection, IEnumerable< AppUserSettings>
	{
		public AppUserSettingsCollection()
		{

		}	
		
		public static implicit operator List< AppUserSettings>(AppUserSettingsCollection coll)
		{
			List< AppUserSettings> list = new List< AppUserSettings>();
			
			foreach (AppUserSettings emp in coll)
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
				return  AppUserSettingsMetadata.Meta();
			}
		}
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppUserSettingsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppUserSettings(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppUserSettings();
		}
		
		#endregion

		[BrowsableAttribute( false )]
		public AppUserSettingsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppUserSettingsQuery();
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
		public bool Load(AppUserSettingsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}		
		
		/// <summary>
		/// Adds a new entity to the collection.
		/// Always calls AddNew() on the entity, in case it is overridden.
		/// </summary>
		public AppUserSettings AddNew()
		{
			AppUserSettings entity = base.AddNewEntity() as AppUserSettings;
			
			return entity;		
		}
		public AppUserSettings FindByPrimaryKey(String userID)
		{
			return base.FindByPrimaryKey(userID) as AppUserSettings;
		}

		#region IEnumerable< AppUserSettings> Members

		IEnumerator< AppUserSettings> IEnumerable< AppUserSettings>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AppUserSettings;
			}
		}

		#endregion
		
		private AppUserSettingsQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppUserSettings' table
	/// </summary>
	[System.Diagnostics.DebuggerDisplay("AppUserSettings ({UserID})")]
	[Serializable]
	public partial class AppUserSettings : esAppUserSettings
	{
		public AppUserSettings()
		{
		}	
	
		public AppUserSettings(DataRow row)
			: base(row)
		{
		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppUserSettingsMetadata.Meta();
			}
		}	
	
		override protected esAppUserSettingsQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppUserSettingsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		
		[BrowsableAttribute( false )]
		public AppUserSettingsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppUserSettingsQuery();
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
		public bool Load(AppUserSettingsQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}			
		
		private AppUserSettingsQuery query;
	}

	[System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
	public partial class AppUserSettingsQuery : esAppUserSettingsQuery
	{
		public AppUserSettingsQuery()
		{

		}		
		
		public AppUserSettingsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	
		
		override protected string GetQueryName()
        {
            return "AppUserSettingsQuery";
        }
	}

	[Serializable]
	public partial class AppUserSettingsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppUserSettingsMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;
				
			c = new esColumnMetadata(AppUserSettingsMetadata.ColumnNames.UserID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserSettingsMetadata.PropertyNames.UserID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c); 
				
			c = new esColumnMetadata(AppUserSettingsMetadata.ColumnNames.QueueingCounterSetting, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserSettingsMetadata.PropertyNames.QueueingCounterSetting;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c); 
				

		}
		#endregion
	
		static public AppUserSettingsMetadata Meta()
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
			get	{ return base._columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			public const string UserID = "UserID";
			public const string QueueingCounterSetting = "QueueingCounterSetting";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			public const string UserID = "UserID";
			public const string QueueingCounterSetting = "QueueingCounterSetting";
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
			lock (typeof(AppUserSettingsMetadata))
			{
				if(AppUserSettingsMetadata.mapDelegates == null)
				{
					AppUserSettingsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppUserSettingsMetadata.meta == null)
				{
					AppUserSettingsMetadata.meta = new AppUserSettingsMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();
				
				meta.AddTypeMap("UserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QueueingCounterSetting", new esTypeMap("varchar", "System.String"));
		

				meta.Source = "AppUserSettings";
				meta.Destination = "AppUserSettings";
				meta.spInsert = "proc_AppUserSettingsInsert";				
				meta.spUpdate = "proc_AppUserSettingsUpdate";		
				meta.spDelete = "proc_AppUserSettingsDelete";
				meta.spLoadAll = "proc_AppUserSettingsLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppUserSettingsLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppUserSettingsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}

}		
