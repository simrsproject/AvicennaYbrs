/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:10 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;


using Temiang.Dal.Core;
using Temiang.Dal.Interfaces;
using Temiang.Dal.DynamicQuery;



namespace Temiang.Avicenna.BusinessObject
{

	[Serializable]
	abstract public class esAppUserServiceUnitCollection : esEntityCollectionWAuditLog
	{
		public esAppUserServiceUnitCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AppUserServiceUnitCollection";
		}

		#region Query Logic
		protected void InitQuery(esAppUserServiceUnitQuery query)
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
			this.InitQuery(query as esAppUserServiceUnitQuery);
		}
		#endregion
		
		virtual public AppUserServiceUnit DetachEntity(AppUserServiceUnit entity)
		{
			return base.DetachEntity(entity) as AppUserServiceUnit;
		}
		
		virtual public AppUserServiceUnit AttachEntity(AppUserServiceUnit entity)
		{
			return base.AttachEntity(entity) as AppUserServiceUnit;
		}
		
		virtual public void Combine(AppUserServiceUnitCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AppUserServiceUnit this[int index]
		{
			get
			{
				return base[index] as AppUserServiceUnit;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AppUserServiceUnit);
		}
	}



	[Serializable]
	abstract public class esAppUserServiceUnit : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAppUserServiceUnitQuery GetDynamicQuery()
		{
			return null;
		}

		public esAppUserServiceUnit()
		{

		}

		public esAppUserServiceUnit(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String userID, System.String serviceUnitID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(userID, serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(userID, serviceUnitID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String userID, System.String serviceUnitID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(userID, serviceUnitID);
			else
				return LoadByPrimaryKeyStoredProcedure(userID, serviceUnitID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String userID, System.String serviceUnitID)
		{
			esAppUserServiceUnitQuery query = this.GetDynamicQuery();
			query.Where(query.UserID == userID, query.ServiceUnitID == serviceUnitID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String userID, System.String serviceUnitID)
		{
			esParameters parms = new esParameters();
			parms.Add("UserID",userID);			parms.Add("ServiceUnitID",serviceUnitID);
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
						case "ServiceUnitID": this.str.ServiceUnitID = (string)value; break;							
						case "IsDiscontinue": this.str.IsDiscontinue = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsDiscontinue":
						
							if (value == null || value is System.Boolean)
								this.IsDiscontinue = (System.Boolean?)value;
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
		/// Maps to AppUserServiceUnit.UserID
		/// </summary>
		virtual public System.String UserID
		{
			get
			{
				return base.GetSystemString(AppUserServiceUnitMetadata.ColumnNames.UserID);
			}
			
			set
			{
				base.SetSystemString(AppUserServiceUnitMetadata.ColumnNames.UserID, value);
			}
		}
		
		/// <summary>
		/// Maps to AppUserServiceUnit.ServiceUnitID
		/// </summary>
		virtual public System.String ServiceUnitID
		{
			get
			{
				return base.GetSystemString(AppUserServiceUnitMetadata.ColumnNames.ServiceUnitID);
			}
			
			set
			{
				base.SetSystemString(AppUserServiceUnitMetadata.ColumnNames.ServiceUnitID, value);
			}
		}
		
		/// <summary>
		/// Maps to AppUserServiceUnit.IsDiscontinue
		/// </summary>
		virtual public System.Boolean? IsDiscontinue
		{
			get
			{
				return base.GetSystemBoolean(AppUserServiceUnitMetadata.ColumnNames.IsDiscontinue);
			}
			
			set
			{
				base.SetSystemBoolean(AppUserServiceUnitMetadata.ColumnNames.IsDiscontinue, value);
			}
		}
		
		/// <summary>
		/// Maps to AppUserServiceUnit.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AppUserServiceUnitMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AppUserServiceUnitMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AppUserServiceUnit.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AppUserServiceUnitMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AppUserServiceUnitMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		#endregion	

		#region String Properties


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
			public esStrings(esAppUserServiceUnit entity)
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
				
			public System.String IsDiscontinue
			{
				get
				{
					System.Boolean? data = entity.IsDiscontinue;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDiscontinue = null;
					else entity.IsDiscontinue = Convert.ToBoolean(value);
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
			

			private esAppUserServiceUnit entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAppUserServiceUnitQuery query)
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
				throw new Exception("esAppUserServiceUnit can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AppUserServiceUnit : esAppUserServiceUnit
	{

		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
		
			return props;
		}	
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostOneToOneSave.
		/// </summary>
		protected override void ApplyPostOneSaveKeys()
		{
		}
		
	}



	[Serializable]
	abstract public class esAppUserServiceUnitQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AppUserServiceUnitMetadata.Meta();
			}
		}	
		

		public esQueryItem UserID
		{
			get
			{
				return new esQueryItem(this, AppUserServiceUnitMetadata.ColumnNames.UserID, esSystemType.String);
			}
		} 
		
		public esQueryItem ServiceUnitID
		{
			get
			{
				return new esQueryItem(this, AppUserServiceUnitMetadata.ColumnNames.ServiceUnitID, esSystemType.String);
			}
		} 
		
		public esQueryItem IsDiscontinue
		{
			get
			{
				return new esQueryItem(this, AppUserServiceUnitMetadata.ColumnNames.IsDiscontinue, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AppUserServiceUnitMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AppUserServiceUnitMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AppUserServiceUnitCollection")]
	public partial class AppUserServiceUnitCollection : esAppUserServiceUnitCollection, IEnumerable<AppUserServiceUnit>
	{
		public AppUserServiceUnitCollection()
		{

		}
		
		public static implicit operator List<AppUserServiceUnit>(AppUserServiceUnitCollection coll)
		{
			List<AppUserServiceUnit> list = new List<AppUserServiceUnit>();
			
			foreach (AppUserServiceUnit emp in coll)
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
				return  AppUserServiceUnitMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppUserServiceUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AppUserServiceUnit(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AppUserServiceUnit();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AppUserServiceUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppUserServiceUnitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AppUserServiceUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AppUserServiceUnit AddNew()
		{
			AppUserServiceUnit entity = base.AddNewEntity() as AppUserServiceUnit;
			
			return entity;
		}

		public AppUserServiceUnit FindByPrimaryKey(System.String userID, System.String serviceUnitID)
		{
			return base.FindByPrimaryKey(userID, serviceUnitID) as AppUserServiceUnit;
		}


		#region IEnumerable<AppUserServiceUnit> Members

		IEnumerator<AppUserServiceUnit> IEnumerable<AppUserServiceUnit>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AppUserServiceUnit;
			}
		}

		#endregion
		
		private AppUserServiceUnitQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AppUserServiceUnit' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AppUserServiceUnit ({UserID},{ServiceUnitID})")]
	[Serializable]
	public partial class AppUserServiceUnit : esAppUserServiceUnit
	{
		public AppUserServiceUnit()
		{

		}
	
		public AppUserServiceUnit(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AppUserServiceUnitMetadata.Meta();
			}
		}
		
		
		
		override protected esAppUserServiceUnitQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AppUserServiceUnitQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AppUserServiceUnitQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AppUserServiceUnitQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AppUserServiceUnitQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AppUserServiceUnitQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AppUserServiceUnitQuery : esAppUserServiceUnitQuery
	{
		public AppUserServiceUnitQuery()
		{

		}		
		
		public AppUserServiceUnitQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AppUserServiceUnitQuery";
        }
		
			
	}


	[Serializable]
	public partial class AppUserServiceUnitMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AppUserServiceUnitMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AppUserServiceUnitMetadata.ColumnNames.UserID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserServiceUnitMetadata.PropertyNames.UserID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 15;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppUserServiceUnitMetadata.ColumnNames.ServiceUnitID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserServiceUnitMetadata.PropertyNames.ServiceUnitID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppUserServiceUnitMetadata.ColumnNames.IsDiscontinue, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = AppUserServiceUnitMetadata.PropertyNames.IsDiscontinue;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppUserServiceUnitMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AppUserServiceUnitMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(AppUserServiceUnitMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = AppUserServiceUnitMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AppUserServiceUnitMetadata Meta()
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
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string IsDiscontinue = "IsDiscontinue";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string UserID = "UserID";
			 public const string ServiceUnitID = "ServiceUnitID";
			 public const string IsDiscontinue = "IsDiscontinue";
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
			lock (typeof(AppUserServiceUnitMetadata))
			{
				if(AppUserServiceUnitMetadata.mapDelegates == null)
				{
					AppUserServiceUnitMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AppUserServiceUnitMetadata.meta == null)
				{
					AppUserServiceUnitMetadata.meta = new AppUserServiceUnitMetadata();
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
				meta.AddTypeMap("ServiceUnitID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsDiscontinue", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AppUserServiceUnit";
				meta.Destination = "AppUserServiceUnit";
				
				meta.spInsert = "proc_AppUserServiceUnitInsert";				
				meta.spUpdate = "proc_AppUserServiceUnitUpdate";		
				meta.spDelete = "proc_AppUserServiceUnitDelete";
				meta.spLoadAll = "proc_AppUserServiceUnitLoadAll";
				meta.spLoadByPrimaryKey = "proc_AppUserServiceUnitLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AppUserServiceUnitMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
