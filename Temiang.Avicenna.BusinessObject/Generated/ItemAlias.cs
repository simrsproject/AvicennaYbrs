/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 10/30/2018 3:58:19 PM
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
	abstract public class esItemAliasCollection : esEntityCollectionWAuditLog
	{
		public esItemAliasCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemAliasCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemAliasQuery query)
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
			this.InitQuery(query as esItemAliasQuery);
		}
		#endregion
		
		virtual public ItemAlias DetachEntity(ItemAlias entity)
		{
			return base.DetachEntity(entity) as ItemAlias;
		}
		
		virtual public ItemAlias AttachEntity(ItemAlias entity)
		{
			return base.AttachEntity(entity) as ItemAlias;
		}
		
		virtual public void Combine(ItemAliasCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemAlias this[int index]
		{
			get
			{
				return base[index] as ItemAlias;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemAlias);
		}
	}



	[Serializable]
	abstract public class esItemAlias : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemAliasQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemAlias()
		{

		}

		public esItemAlias(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String itemAliasID, System.String itemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemAliasID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemAliasID, itemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemAliasID, System.String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemAliasID, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemAliasID, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String itemAliasID, System.String itemID)
		{
			esItemAliasQuery query = this.GetDynamicQuery();
			query.Where(query.ItemAliasID == itemAliasID, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String itemAliasID, System.String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemAliasID",itemAliasID);			parms.Add("ItemID",itemID);
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
						case "ItemAliasID": this.str.ItemAliasID = (string)value; break;							
						case "ItemAliasName": this.str.ItemAliasName = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
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
		/// Maps to ItemAlias.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemAliasMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemAliasMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemAlias.ItemAliasID
		/// </summary>
		virtual public System.String ItemAliasID
		{
			get
			{
				return base.GetSystemString(ItemAliasMetadata.ColumnNames.ItemAliasID);
			}
			
			set
			{
				base.SetSystemString(ItemAliasMetadata.ColumnNames.ItemAliasID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemAlias.ItemAliasName
		/// </summary>
		virtual public System.String ItemAliasName
		{
			get
			{
				return base.GetSystemString(ItemAliasMetadata.ColumnNames.ItemAliasName);
			}
			
			set
			{
				base.SetSystemString(ItemAliasMetadata.ColumnNames.ItemAliasName, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemAlias.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ItemAliasMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ItemAliasMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemAlias.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemAliasMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemAliasMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemAlias.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemAliasMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemAliasMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemAlias entity)
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
				
			public System.String ItemAliasID
			{
				get
				{
					System.String data = entity.ItemAliasID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemAliasID = null;
					else entity.ItemAliasID = Convert.ToString(value);
				}
			}
				
			public System.String ItemAliasName
			{
				get
				{
					System.String data = entity.ItemAliasName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ItemAliasName = null;
					else entity.ItemAliasName = Convert.ToString(value);
				}
			}
				
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
			

			private esItemAlias entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemAliasQuery query)
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
				throw new Exception("esItemAlias can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemAlias : esItemAlias
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
	abstract public class esItemAliasQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemAliasMetadata.Meta();
			}
		}	
		

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemAliasMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemAliasID
		{
			get
			{
				return new esQueryItem(this, ItemAliasMetadata.ColumnNames.ItemAliasID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemAliasName
		{
			get
			{
				return new esQueryItem(this, ItemAliasMetadata.ColumnNames.ItemAliasName, esSystemType.String);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ItemAliasMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemAliasMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemAliasMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemAliasCollection")]
	public partial class ItemAliasCollection : esItemAliasCollection, IEnumerable<ItemAlias>
	{
		public ItemAliasCollection()
		{

		}
		
		public static implicit operator List<ItemAlias>(ItemAliasCollection coll)
		{
			List<ItemAlias> list = new List<ItemAlias>();
			
			foreach (ItemAlias emp in coll)
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
				return  ItemAliasMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemAliasQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemAlias(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemAlias();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemAliasQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemAliasQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemAliasQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemAlias AddNew()
		{
			ItemAlias entity = base.AddNewEntity() as ItemAlias;
			
			return entity;
		}

		public ItemAlias FindByPrimaryKey(System.String itemAliasID, System.String itemID)
		{
			return base.FindByPrimaryKey(itemAliasID, itemID) as ItemAlias;
		}


		#region IEnumerable<ItemAlias> Members

		IEnumerator<ItemAlias> IEnumerable<ItemAlias>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemAlias;
			}
		}

		#endregion
		
		private ItemAliasQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemAlias' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemAlias ({ItemID},{ItemAliasID})")]
	[Serializable]
	public partial class ItemAlias : esItemAlias
	{
		public ItemAlias()
		{

		}
	
		public ItemAlias(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemAliasMetadata.Meta();
			}
		}
		
		
		
		override protected esItemAliasQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemAliasQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemAliasQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemAliasQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemAliasQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemAliasQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemAliasQuery : esItemAliasQuery
	{
		public ItemAliasQuery()
		{

		}		
		
		public ItemAliasQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemAliasQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemAliasMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemAliasMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemAliasMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemAliasMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemAliasMetadata.ColumnNames.ItemAliasID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemAliasMetadata.PropertyNames.ItemAliasID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemAliasMetadata.ColumnNames.ItemAliasName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemAliasMetadata.PropertyNames.ItemAliasName;
			c.CharacterMaxLength = 255;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemAliasMetadata.ColumnNames.IsActive, 3, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemAliasMetadata.PropertyNames.IsActive;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemAliasMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemAliasMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemAliasMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemAliasMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemAliasMetadata Meta()
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
			 public const string ItemAliasID = "ItemAliasID";
			 public const string ItemAliasName = "ItemAliasName";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ItemID = "ItemID";
			 public const string ItemAliasID = "ItemAliasID";
			 public const string ItemAliasName = "ItemAliasName";
			 public const string IsActive = "IsActive";
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
			lock (typeof(ItemAliasMetadata))
			{
				if(ItemAliasMetadata.mapDelegates == null)
				{
					ItemAliasMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemAliasMetadata.meta == null)
				{
					ItemAliasMetadata.meta = new ItemAliasMetadata();
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
				meta.AddTypeMap("ItemAliasID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemAliasName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemAlias";
				meta.Destination = "ItemAlias";
				
				meta.spInsert = "proc_ItemAliasInsert";				
				meta.spUpdate = "proc_ItemAliasUpdate";		
				meta.spDelete = "proc_ItemAliasDelete";
				meta.spLoadAll = "proc_ItemAliasLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemAliasLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemAliasMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
