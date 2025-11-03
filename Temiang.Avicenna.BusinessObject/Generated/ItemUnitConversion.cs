/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:19 PM
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
	abstract public class esItemUnitConversionCollection : esEntityCollectionWAuditLog
	{
		public esItemUnitConversionCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemUnitConversionCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemUnitConversionQuery query)
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
			this.InitQuery(query as esItemUnitConversionQuery);
		}
		#endregion
		
		virtual public ItemUnitConversion DetachEntity(ItemUnitConversion entity)
		{
			return base.DetachEntity(entity) as ItemUnitConversion;
		}
		
		virtual public ItemUnitConversion AttachEntity(ItemUnitConversion entity)
		{
			return base.AttachEntity(entity) as ItemUnitConversion;
		}
		
		virtual public void Combine(ItemUnitConversionCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemUnitConversion this[int index]
		{
			get
			{
				return base[index] as ItemUnitConversion;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemUnitConversion);
		}
	}



	[Serializable]
	abstract public class esItemUnitConversion : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemUnitConversionQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemUnitConversion()
		{

		}

		public esItemUnitConversion(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String itemID, System.String sRAlternateItemUnit)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, sRAlternateItemUnit);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, sRAlternateItemUnit);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID, System.String sRAlternateItemUnit)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, sRAlternateItemUnit);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, sRAlternateItemUnit);
		}

		private bool LoadByPrimaryKeyDynamic(System.String itemID, System.String sRAlternateItemUnit)
		{
			esItemUnitConversionQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID, query.SRAlternateItemUnit == sRAlternateItemUnit);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String itemID, System.String sRAlternateItemUnit)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID",itemID);			parms.Add("SRAlternateItemUnit",sRAlternateItemUnit);
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
						case "SRAlternateItemUnit": this.str.SRAlternateItemUnit = (string)value; break;							
						case "ConversionFactor": this.str.ConversionFactor = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ConversionFactor":
						
							if (value == null || value is System.Decimal)
								this.ConversionFactor = (System.Decimal?)value;
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
		/// Maps to ItemUnitConversion.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemUnitConversionMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemUnitConversionMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemUnitConversion.SRAlternateItemUnit
		/// </summary>
		virtual public System.String SRAlternateItemUnit
		{
			get
			{
				return base.GetSystemString(ItemUnitConversionMetadata.ColumnNames.SRAlternateItemUnit);
			}
			
			set
			{
				base.SetSystemString(ItemUnitConversionMetadata.ColumnNames.SRAlternateItemUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemUnitConversion.ConversionFactor
		/// </summary>
		virtual public System.Decimal? ConversionFactor
		{
			get
			{
				return base.GetSystemDecimal(ItemUnitConversionMetadata.ColumnNames.ConversionFactor);
			}
			
			set
			{
				base.SetSystemDecimal(ItemUnitConversionMetadata.ColumnNames.ConversionFactor, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemUnitConversion.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemUnitConversionMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemUnitConversionMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemUnitConversion.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemUnitConversionMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemUnitConversionMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemUnitConversion entity)
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
				
			public System.String SRAlternateItemUnit
			{
				get
				{
					System.String data = entity.SRAlternateItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRAlternateItemUnit = null;
					else entity.SRAlternateItemUnit = Convert.ToString(value);
				}
			}
				
			public System.String ConversionFactor
			{
				get
				{
					System.Decimal? data = entity.ConversionFactor;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ConversionFactor = null;
					else entity.ConversionFactor = Convert.ToDecimal(value);
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
			

			private esItemUnitConversion entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemUnitConversionQuery query)
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
				throw new Exception("esItemUnitConversion can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemUnitConversion : esItemUnitConversion
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
	abstract public class esItemUnitConversionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemUnitConversionMetadata.Meta();
			}
		}	
		

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemUnitConversionMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem SRAlternateItemUnit
		{
			get
			{
				return new esQueryItem(this, ItemUnitConversionMetadata.ColumnNames.SRAlternateItemUnit, esSystemType.String);
			}
		} 
		
		public esQueryItem ConversionFactor
		{
			get
			{
				return new esQueryItem(this, ItemUnitConversionMetadata.ColumnNames.ConversionFactor, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemUnitConversionMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemUnitConversionMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemUnitConversionCollection")]
	public partial class ItemUnitConversionCollection : esItemUnitConversionCollection, IEnumerable<ItemUnitConversion>
	{
		public ItemUnitConversionCollection()
		{

		}
		
		public static implicit operator List<ItemUnitConversion>(ItemUnitConversionCollection coll)
		{
			List<ItemUnitConversion> list = new List<ItemUnitConversion>();
			
			foreach (ItemUnitConversion emp in coll)
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
				return  ItemUnitConversionMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemUnitConversionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemUnitConversion(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemUnitConversion();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemUnitConversionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemUnitConversionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemUnitConversionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemUnitConversion AddNew()
		{
			ItemUnitConversion entity = base.AddNewEntity() as ItemUnitConversion;
			
			return entity;
		}

		public ItemUnitConversion FindByPrimaryKey(System.String itemID, System.String sRAlternateItemUnit)
		{
			return base.FindByPrimaryKey(itemID, sRAlternateItemUnit) as ItemUnitConversion;
		}


		#region IEnumerable<ItemUnitConversion> Members

		IEnumerator<ItemUnitConversion> IEnumerable<ItemUnitConversion>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemUnitConversion;
			}
		}

		#endregion
		
		private ItemUnitConversionQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemUnitConversion' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemUnitConversion ({ItemID},{SRAlternateItemUnit})")]
	[Serializable]
	public partial class ItemUnitConversion : esItemUnitConversion
	{
		public ItemUnitConversion()
		{

		}
	
		public ItemUnitConversion(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemUnitConversionMetadata.Meta();
			}
		}
		
		
		
		override protected esItemUnitConversionQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemUnitConversionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemUnitConversionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemUnitConversionQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemUnitConversionQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemUnitConversionQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemUnitConversionQuery : esItemUnitConversionQuery
	{
		public ItemUnitConversionQuery()
		{

		}		
		
		public ItemUnitConversionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemUnitConversionQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemUnitConversionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemUnitConversionMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemUnitConversionMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemUnitConversionMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemUnitConversionMetadata.ColumnNames.SRAlternateItemUnit, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemUnitConversionMetadata.PropertyNames.SRAlternateItemUnit;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemUnitConversionMetadata.ColumnNames.ConversionFactor, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemUnitConversionMetadata.PropertyNames.ConversionFactor;
			c.NumericPrecision = 6;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemUnitConversionMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemUnitConversionMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemUnitConversionMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemUnitConversionMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemUnitConversionMetadata Meta()
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
			 public const string SRAlternateItemUnit = "SRAlternateItemUnit";
			 public const string ConversionFactor = "ConversionFactor";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ItemID = "ItemID";
			 public const string SRAlternateItemUnit = "SRAlternateItemUnit";
			 public const string ConversionFactor = "ConversionFactor";
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
			lock (typeof(ItemUnitConversionMetadata))
			{
				if(ItemUnitConversionMetadata.mapDelegates == null)
				{
					ItemUnitConversionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemUnitConversionMetadata.meta == null)
				{
					ItemUnitConversionMetadata.meta = new ItemUnitConversionMetadata();
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
				meta.AddTypeMap("SRAlternateItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConversionFactor", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemUnitConversion";
				meta.Destination = "ItemUnitConversion";
				
				meta.spInsert = "proc_ItemUnitConversionInsert";				
				meta.spUpdate = "proc_ItemUnitConversionUpdate";		
				meta.spDelete = "proc_ItemUnitConversionDelete";
				meta.spLoadAll = "proc_ItemUnitConversionLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemUnitConversionLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemUnitConversionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
