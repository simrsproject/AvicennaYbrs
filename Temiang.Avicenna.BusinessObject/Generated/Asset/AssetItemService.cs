/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:11 PM
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
	abstract public class esAssetItemServiceCollection : esEntityCollectionWAuditLog
	{
		public esAssetItemServiceCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "AssetItemServiceCollection";
		}

		#region Query Logic
		protected void InitQuery(esAssetItemServiceQuery query)
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
			this.InitQuery(query as esAssetItemServiceQuery);
		}
		#endregion
		
		virtual public AssetItemService DetachEntity(AssetItemService entity)
		{
			return base.DetachEntity(entity) as AssetItemService;
		}
		
		virtual public AssetItemService AttachEntity(AssetItemService entity)
		{
			return base.AttachEntity(entity) as AssetItemService;
		}
		
		virtual public void Combine(AssetItemServiceCollection collection)
		{
			base.Combine(collection);
		}
		
		new public AssetItemService this[int index]
		{
			get
			{
				return base[index] as AssetItemService;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(AssetItemService);
		}
	}



	[Serializable]
	abstract public class esAssetItemService : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esAssetItemServiceQuery GetDynamicQuery()
		{
			return null;
		}

		public esAssetItemService()
		{

		}

		public esAssetItemService(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String itemID, System.String assetID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, assetID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, assetID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID, System.String assetID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, assetID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, assetID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String itemID, System.String assetID)
		{
			esAssetItemServiceQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID, query.AssetID == assetID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String itemID, System.String assetID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID",itemID);			parms.Add("AssetID",assetID);
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
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "AssetID": this.str.AssetID = (string)value; break;							
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
		/// Maps to AssetItemService.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(AssetItemServiceMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(AssetItemServiceMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetItemService.AssetID
		/// </summary>
		virtual public System.String AssetID
		{
			get
			{
				return base.GetSystemString(AssetItemServiceMetadata.ColumnNames.AssetID);
			}
			
			set
			{
				base.SetSystemString(AssetItemServiceMetadata.ColumnNames.AssetID, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetItemService.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(AssetItemServiceMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(AssetItemServiceMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to AssetItemService.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(AssetItemServiceMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(AssetItemServiceMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esAssetItemService entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ItemID
			{
				get
				{
					System.String data = entity.ItemID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemID = null;
					else entity.ItemID = Convert.ToString(value);
				}
			}
				
			public System.String AssetID
			{
				get
				{
					System.String data = entity.AssetID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AssetID = null;
					else entity.AssetID = Convert.ToString(value);
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
			

			private esAssetItemService entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esAssetItemServiceQuery query)
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
				throw new Exception("esAssetItemService can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class AssetItemService : esAssetItemService
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
	abstract public class esAssetItemServiceQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return AssetItemServiceMetadata.Meta();
			}
		}	
		

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, AssetItemServiceMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem AssetID
		{
			get
			{
				return new esQueryItem(this, AssetItemServiceMetadata.ColumnNames.AssetID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, AssetItemServiceMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, AssetItemServiceMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("AssetItemServiceCollection")]
	public partial class AssetItemServiceCollection : esAssetItemServiceCollection, IEnumerable<AssetItemService>
	{
		public AssetItemServiceCollection()
		{

		}
		
		public static implicit operator List<AssetItemService>(AssetItemServiceCollection coll)
		{
			List<AssetItemService> list = new List<AssetItemService>();
			
			foreach (AssetItemService emp in coll)
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
				return  AssetItemServiceMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetItemServiceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new AssetItemService(row);
		}

		override protected esEntity CreateEntity()
		{
			return new AssetItemService();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public AssetItemServiceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetItemServiceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(AssetItemServiceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public AssetItemService AddNew()
		{
			AssetItemService entity = base.AddNewEntity() as AssetItemService;
			
			return entity;
		}

		public AssetItemService FindByPrimaryKey(System.String itemID, System.String assetID)
		{
			return base.FindByPrimaryKey(itemID, assetID) as AssetItemService;
		}


		#region IEnumerable<AssetItemService> Members

		IEnumerator<AssetItemService> IEnumerable<AssetItemService>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as AssetItemService;
			}
		}

		#endregion
		
		private AssetItemServiceQuery query;
	}


	/// <summary>
	/// Encapsulates the 'AssetItemService' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("AssetItemService ({ItemID},{AssetID})")]
	[Serializable]
	public partial class AssetItemService : esAssetItemService
	{
		public AssetItemService()
		{

		}
	
		public AssetItemService(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return AssetItemServiceMetadata.Meta();
			}
		}
		
		
		
		override protected esAssetItemServiceQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new AssetItemServiceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public AssetItemServiceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new AssetItemServiceQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(AssetItemServiceQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private AssetItemServiceQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class AssetItemServiceQuery : esAssetItemServiceQuery
	{
		public AssetItemServiceQuery()
		{

		}		
		
		public AssetItemServiceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "AssetItemServiceQuery";
        }
		
			
	}


	[Serializable]
	public partial class AssetItemServiceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected AssetItemServiceMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(AssetItemServiceMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetItemServiceMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetItemServiceMetadata.ColumnNames.AssetID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetItemServiceMetadata.PropertyNames.AssetID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 30;
			c.HasDefault = true;
			c.Default = @"('00:00')";
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetItemServiceMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = AssetItemServiceMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(AssetItemServiceMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = AssetItemServiceMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public AssetItemServiceMetadata Meta()
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
			 public const string ItemID = "ItemID";
			 public const string AssetID = "AssetID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ItemID = "ItemID";
			 public const string AssetID = "AssetID";
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
			lock (typeof(AssetItemServiceMetadata))
			{
				if(AssetItemServiceMetadata.mapDelegates == null)
				{
					AssetItemServiceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (AssetItemServiceMetadata.meta == null)
				{
					AssetItemServiceMetadata.meta = new AssetItemServiceMetadata();
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
				

				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AssetID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "AssetItemService";
				meta.Destination = "AssetItemService";
				
				meta.spInsert = "proc_AssetItemServiceInsert";				
				meta.spUpdate = "proc_AssetItemServiceUpdate";		
				meta.spDelete = "proc_AssetItemServiceDelete";
				meta.spLoadAll = "proc_AssetItemServiceLoadAll";
				meta.spLoadByPrimaryKey = "proc_AssetItemServiceLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private AssetItemServiceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
