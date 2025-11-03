/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:18 PM
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
	abstract public class esItemServiceSubSpecialtyCollection : esEntityCollectionWAuditLog
	{
		public esItemServiceSubSpecialtyCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemServiceSubSpecialtyCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemServiceSubSpecialtyQuery query)
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
			this.InitQuery(query as esItemServiceSubSpecialtyQuery);
		}
		#endregion
		
		virtual public ItemServiceSubSpecialty DetachEntity(ItemServiceSubSpecialty entity)
		{
			return base.DetachEntity(entity) as ItemServiceSubSpecialty;
		}
		
		virtual public ItemServiceSubSpecialty AttachEntity(ItemServiceSubSpecialty entity)
		{
			return base.AttachEntity(entity) as ItemServiceSubSpecialty;
		}
		
		virtual public void Combine(ItemServiceSubSpecialtyCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemServiceSubSpecialty this[int index]
		{
			get
			{
				return base[index] as ItemServiceSubSpecialty;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemServiceSubSpecialty);
		}
	}



	[Serializable]
	abstract public class esItemServiceSubSpecialty : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemServiceSubSpecialtyQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemServiceSubSpecialty()
		{

		}

		public esItemServiceSubSpecialty(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String itemID, System.String subSpecialtyID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, subSpecialtyID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, subSpecialtyID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID, System.String subSpecialtyID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, subSpecialtyID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, subSpecialtyID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String itemID, System.String subSpecialtyID)
		{
			esItemServiceSubSpecialtyQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID, query.SubSpecialtyID == subSpecialtyID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String itemID, System.String subSpecialtyID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID",itemID);			parms.Add("SubSpecialtyID",subSpecialtyID);
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
						case "SubSpecialtyID": this.str.SubSpecialtyID = (string)value; break;							
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
		/// Maps to ItemServiceSubSpecialty.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemServiceSubSpecialtyMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemServiceSubSpecialtyMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemServiceSubSpecialty.SubSpecialtyID
		/// </summary>
		virtual public System.String SubSpecialtyID
		{
			get
			{
				return base.GetSystemString(ItemServiceSubSpecialtyMetadata.ColumnNames.SubSpecialtyID);
			}
			
			set
			{
				base.SetSystemString(ItemServiceSubSpecialtyMetadata.ColumnNames.SubSpecialtyID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemServiceSubSpecialty.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemServiceSubSpecialtyMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemServiceSubSpecialtyMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemServiceSubSpecialty.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemServiceSubSpecialtyMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemServiceSubSpecialtyMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemServiceSubSpecialty entity)
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
				
			public System.String SubSpecialtyID
			{
				get
				{
					System.String data = entity.SubSpecialtyID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubSpecialtyID = null;
					else entity.SubSpecialtyID = Convert.ToString(value);
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
			

			private esItemServiceSubSpecialty entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemServiceSubSpecialtyQuery query)
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
				throw new Exception("esItemServiceSubSpecialty can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemServiceSubSpecialty : esItemServiceSubSpecialty
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
	abstract public class esItemServiceSubSpecialtyQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemServiceSubSpecialtyMetadata.Meta();
			}
		}	
		

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemServiceSubSpecialtyMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem SubSpecialtyID
		{
			get
			{
				return new esQueryItem(this, ItemServiceSubSpecialtyMetadata.ColumnNames.SubSpecialtyID, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemServiceSubSpecialtyMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemServiceSubSpecialtyMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemServiceSubSpecialtyCollection")]
	public partial class ItemServiceSubSpecialtyCollection : esItemServiceSubSpecialtyCollection, IEnumerable<ItemServiceSubSpecialty>
	{
		public ItemServiceSubSpecialtyCollection()
		{

		}
		
		public static implicit operator List<ItemServiceSubSpecialty>(ItemServiceSubSpecialtyCollection coll)
		{
			List<ItemServiceSubSpecialty> list = new List<ItemServiceSubSpecialty>();
			
			foreach (ItemServiceSubSpecialty emp in coll)
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
				return  ItemServiceSubSpecialtyMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemServiceSubSpecialtyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemServiceSubSpecialty(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemServiceSubSpecialty();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemServiceSubSpecialtyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemServiceSubSpecialtyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemServiceSubSpecialtyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemServiceSubSpecialty AddNew()
		{
			ItemServiceSubSpecialty entity = base.AddNewEntity() as ItemServiceSubSpecialty;
			
			return entity;
		}

		public ItemServiceSubSpecialty FindByPrimaryKey(System.String itemID, System.String subSpecialtyID)
		{
			return base.FindByPrimaryKey(itemID, subSpecialtyID) as ItemServiceSubSpecialty;
		}


		#region IEnumerable<ItemServiceSubSpecialty> Members

		IEnumerator<ItemServiceSubSpecialty> IEnumerable<ItemServiceSubSpecialty>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemServiceSubSpecialty;
			}
		}

		#endregion
		
		private ItemServiceSubSpecialtyQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemServiceSubSpecialty' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemServiceSubSpecialty ({ItemID},{SubSpecialtyID})")]
	[Serializable]
	public partial class ItemServiceSubSpecialty : esItemServiceSubSpecialty
	{
		public ItemServiceSubSpecialty()
		{

		}
	
		public ItemServiceSubSpecialty(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemServiceSubSpecialtyMetadata.Meta();
			}
		}
		
		
		
		override protected esItemServiceSubSpecialtyQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemServiceSubSpecialtyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemServiceSubSpecialtyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemServiceSubSpecialtyQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemServiceSubSpecialtyQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemServiceSubSpecialtyQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemServiceSubSpecialtyQuery : esItemServiceSubSpecialtyQuery
	{
		public ItemServiceSubSpecialtyQuery()
		{

		}		
		
		public ItemServiceSubSpecialtyQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemServiceSubSpecialtyQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemServiceSubSpecialtyMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemServiceSubSpecialtyMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemServiceSubSpecialtyMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemServiceSubSpecialtyMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemServiceSubSpecialtyMetadata.ColumnNames.SubSpecialtyID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemServiceSubSpecialtyMetadata.PropertyNames.SubSpecialtyID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemServiceSubSpecialtyMetadata.ColumnNames.LastUpdateDateTime, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemServiceSubSpecialtyMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemServiceSubSpecialtyMetadata.ColumnNames.LastUpdateByUserID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemServiceSubSpecialtyMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemServiceSubSpecialtyMetadata Meta()
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
			 public const string SubSpecialtyID = "SubSpecialtyID";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ItemID = "ItemID";
			 public const string SubSpecialtyID = "SubSpecialtyID";
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
			lock (typeof(ItemServiceSubSpecialtyMetadata))
			{
				if(ItemServiceSubSpecialtyMetadata.mapDelegates == null)
				{
					ItemServiceSubSpecialtyMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemServiceSubSpecialtyMetadata.meta == null)
				{
					ItemServiceSubSpecialtyMetadata.meta = new ItemServiceSubSpecialtyMetadata();
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
				meta.AddTypeMap("SubSpecialtyID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemServiceSubSpecialty";
				meta.Destination = "ItemServiceSubSpecialty";
				
				meta.spInsert = "proc_ItemServiceSubSpecialtyInsert";				
				meta.spUpdate = "proc_ItemServiceSubSpecialtyUpdate";		
				meta.spDelete = "proc_ItemServiceSubSpecialtyDelete";
				meta.spLoadAll = "proc_ItemServiceSubSpecialtyLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemServiceSubSpecialtyLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemServiceSubSpecialtyMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
