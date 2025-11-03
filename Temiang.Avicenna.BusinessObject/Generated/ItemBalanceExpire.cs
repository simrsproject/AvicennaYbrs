/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/22/2012 4:29:17 PM
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
	abstract public class esItemBalanceExpireCollection : esEntityCollectionWAuditLog
	{
		public esItemBalanceExpireCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemBalanceExpireCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemBalanceExpireQuery query)
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
			this.InitQuery(query as esItemBalanceExpireQuery);
		}
		#endregion
		
		virtual public ItemBalanceExpire DetachEntity(ItemBalanceExpire entity)
		{
			return base.DetachEntity(entity) as ItemBalanceExpire;
		}
		
		virtual public ItemBalanceExpire AttachEntity(ItemBalanceExpire entity)
		{
			return base.AttachEntity(entity) as ItemBalanceExpire;
		}
		
		virtual public void Combine(ItemBalanceExpireCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemBalanceExpire this[int index]
		{
			get
			{
				return base[index] as ItemBalanceExpire;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemBalanceExpire);
		}
	}



	[Serializable]
	abstract public class esItemBalanceExpire : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemBalanceExpireQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemBalanceExpire()
		{

		}

		public esItemBalanceExpire(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String locationID, System.String itemID, System.DateTime expiredDate)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(locationID, itemID, expiredDate);
			else
				return LoadByPrimaryKeyStoredProcedure(locationID, itemID, expiredDate);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String locationID, System.String itemID, System.DateTime expiredDate)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(locationID, itemID, expiredDate);
			else
				return LoadByPrimaryKeyStoredProcedure(locationID, itemID, expiredDate);
		}

		private bool LoadByPrimaryKeyDynamic(System.String locationID, System.String itemID, System.DateTime expiredDate)
		{
			esItemBalanceExpireQuery query = this.GetDynamicQuery();
			query.Where(query.LocationID == locationID, query.ItemID == itemID, query.ExpiredDate == expiredDate);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String locationID, System.String itemID, System.DateTime expiredDate)
		{
			esParameters parms = new esParameters();
			parms.Add("LocationID",locationID);			parms.Add("ItemID",itemID);			parms.Add("ExpiredDate",expiredDate);
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
						case "LocationID": this.str.LocationID = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "ExpiredDate": this.str.ExpiredDate = (string)value; break;							
						case "QuantityIn": this.str.QuantityIn = (string)value; break;							
						case "QuantityOut": this.str.QuantityOut = (string)value; break;							
						case "IsActive": this.str.IsActive = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ExpiredDate":
						
							if (value == null || value is System.DateTime)
								this.ExpiredDate = (System.DateTime?)value;
							break;
						
						case "QuantityIn":
						
							if (value == null || value is System.Decimal)
								this.QuantityIn = (System.Decimal?)value;
							break;
						
						case "QuantityOut":
						
							if (value == null || value is System.Decimal)
								this.QuantityOut = (System.Decimal?)value;
							break;
						
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
		/// Maps to ItemBalanceExpire.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(ItemBalanceExpireMetadata.ColumnNames.LocationID);
			}
			
			set
			{
				base.SetSystemString(ItemBalanceExpireMetadata.ColumnNames.LocationID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceExpire.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemBalanceExpireMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				if(base.SetSystemString(ItemBalanceExpireMetadata.ColumnNames.ItemID, value))
				{
					this._UpToItemByItemID = null;
				}
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceExpire.ExpiredDate
		/// </summary>
		virtual public System.DateTime? ExpiredDate
		{
			get
			{
				return base.GetSystemDateTime(ItemBalanceExpireMetadata.ColumnNames.ExpiredDate);
			}
			
			set
			{
				base.SetSystemDateTime(ItemBalanceExpireMetadata.ColumnNames.ExpiredDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceExpire.QuantityIn
		/// </summary>
		virtual public System.Decimal? QuantityIn
		{
			get
			{
				return base.GetSystemDecimal(ItemBalanceExpireMetadata.ColumnNames.QuantityIn);
			}
			
			set
			{
				base.SetSystemDecimal(ItemBalanceExpireMetadata.ColumnNames.QuantityIn, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceExpire.QuantityOut
		/// </summary>
		virtual public System.Decimal? QuantityOut
		{
			get
			{
				return base.GetSystemDecimal(ItemBalanceExpireMetadata.ColumnNames.QuantityOut);
			}
			
			set
			{
				base.SetSystemDecimal(ItemBalanceExpireMetadata.ColumnNames.QuantityOut, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceExpire.IsActive
		/// </summary>
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ItemBalanceExpireMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				base.SetSystemBoolean(ItemBalanceExpireMetadata.ColumnNames.IsActive, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceExpire.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemBalanceExpireMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemBalanceExpireMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceExpire.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemBalanceExpireMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemBalanceExpireMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		[CLSCompliant(false)]
		internal protected Item _UpToItemByItemID;
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
			public esStrings(esItemBalanceExpire entity)
			{
				this.entity = entity;
			}
			
	
			public System.String LocationID
			{
				get
				{
					System.String data = entity.LocationID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LocationID = null;
					else entity.LocationID = Convert.ToString(value);
				}
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
				
			public System.String ExpiredDate
			{
				get
				{
					System.DateTime? data = entity.ExpiredDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ExpiredDate = null;
					else entity.ExpiredDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String QuantityIn
			{
				get
				{
					System.Decimal? data = entity.QuantityIn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuantityIn = null;
					else entity.QuantityIn = Convert.ToDecimal(value);
				}
			}
				
			public System.String QuantityOut
			{
				get
				{
					System.Decimal? data = entity.QuantityOut;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuantityOut = null;
					else entity.QuantityOut = Convert.ToDecimal(value);
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
			

			private esItemBalanceExpire entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemBalanceExpireQuery query)
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
				throw new Exception("esItemBalanceExpire can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemBalanceExpire : esItemBalanceExpire
	{

				
		#region UpToItemByItemID - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - RefItemBalanceDtToItem
		/// </summary>

		[XmlIgnore]
		public Item UpToItemByItemID
		{
			get
			{
				if(this._UpToItemByItemID == null
					&& ItemID != null					)
				{
					this._UpToItemByItemID = new Item();
					this._UpToItemByItemID.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToItemByItemID", this._UpToItemByItemID);
					this._UpToItemByItemID.Query.Where(this._UpToItemByItemID.Query.ItemID == this.ItemID);
					this._UpToItemByItemID.Query.Load();
				}

				return this._UpToItemByItemID;
			}
			
			set
			{
				this.RemovePreSave("UpToItemByItemID");
				

				if(value == null)
				{
					this.ItemID = null;
					this._UpToItemByItemID = null;
				}
				else
				{
					this.ItemID = value.ItemID;
					this._UpToItemByItemID = value;
					this.SetPreSave("UpToItemByItemID", this._UpToItemByItemID);
				}
				
			}
		}
		#endregion
		

		
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
	abstract public class esItemBalanceExpireQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemBalanceExpireMetadata.Meta();
			}
		}	
		

		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceExpireMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceExpireMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ExpiredDate
		{
			get
			{
				return new esQueryItem(this, ItemBalanceExpireMetadata.ColumnNames.ExpiredDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem QuantityIn
		{
			get
			{
				return new esQueryItem(this, ItemBalanceExpireMetadata.ColumnNames.QuantityIn, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem QuantityOut
		{
			get
			{
				return new esQueryItem(this, ItemBalanceExpireMetadata.ColumnNames.QuantityOut, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsActive
		{
			get
			{
				return new esQueryItem(this, ItemBalanceExpireMetadata.ColumnNames.IsActive, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemBalanceExpireMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceExpireMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemBalanceExpireCollection")]
	public partial class ItemBalanceExpireCollection : esItemBalanceExpireCollection, IEnumerable<ItemBalanceExpire>
	{
		public ItemBalanceExpireCollection()
		{

		}
		
		public static implicit operator List<ItemBalanceExpire>(ItemBalanceExpireCollection coll)
		{
			List<ItemBalanceExpire> list = new List<ItemBalanceExpire>();
			
			foreach (ItemBalanceExpire emp in coll)
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
				return  ItemBalanceExpireMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemBalanceExpireQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemBalanceExpire(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemBalanceExpire();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemBalanceExpireQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemBalanceExpireQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemBalanceExpireQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemBalanceExpire AddNew()
		{
			ItemBalanceExpire entity = base.AddNewEntity() as ItemBalanceExpire;
			
			return entity;
		}

		public ItemBalanceExpire FindByPrimaryKey(System.String locationID, System.String itemID, System.DateTime expiredDate)
		{
			return base.FindByPrimaryKey(locationID, itemID, expiredDate) as ItemBalanceExpire;
		}


		#region IEnumerable<ItemBalanceExpire> Members

		IEnumerator<ItemBalanceExpire> IEnumerable<ItemBalanceExpire>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemBalanceExpire;
			}
		}

		#endregion
		
		private ItemBalanceExpireQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemBalanceExpire' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemBalanceExpire ({LocationID},{ItemID},{ExpiredDate})")]
	[Serializable]
	public partial class ItemBalanceExpire : esItemBalanceExpire
	{
		public ItemBalanceExpire()
		{

		}
	
		public ItemBalanceExpire(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemBalanceExpireMetadata.Meta();
			}
		}
		
		
		
		override protected esItemBalanceExpireQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemBalanceExpireQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemBalanceExpireQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemBalanceExpireQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemBalanceExpireQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemBalanceExpireQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemBalanceExpireQuery : esItemBalanceExpireQuery
	{
		public ItemBalanceExpireQuery()
		{

		}		
		
		public ItemBalanceExpireQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemBalanceExpireQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemBalanceExpireMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemBalanceExpireMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemBalanceExpireMetadata.ColumnNames.LocationID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceExpireMetadata.PropertyNames.LocationID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceExpireMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceExpireMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceExpireMetadata.ColumnNames.ExpiredDate, 2, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemBalanceExpireMetadata.PropertyNames.ExpiredDate;
			c.IsInPrimaryKey = true;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceExpireMetadata.ColumnNames.QuantityIn, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemBalanceExpireMetadata.PropertyNames.QuantityIn;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceExpireMetadata.ColumnNames.QuantityOut, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemBalanceExpireMetadata.PropertyNames.QuantityOut;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceExpireMetadata.ColumnNames.IsActive, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemBalanceExpireMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceExpireMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemBalanceExpireMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceExpireMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceExpireMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemBalanceExpireMetadata Meta()
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
			 public const string LocationID = "LocationID";
			 public const string ItemID = "ItemID";
			 public const string ExpiredDate = "ExpiredDate";
			 public const string QuantityIn = "QuantityIn";
			 public const string QuantityOut = "QuantityOut";
			 public const string IsActive = "IsActive";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string LocationID = "LocationID";
			 public const string ItemID = "ItemID";
			 public const string ExpiredDate = "ExpiredDate";
			 public const string QuantityIn = "QuantityIn";
			 public const string QuantityOut = "QuantityOut";
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
			lock (typeof(ItemBalanceExpireMetadata))
			{
				if(ItemBalanceExpireMetadata.mapDelegates == null)
				{
					ItemBalanceExpireMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemBalanceExpireMetadata.meta == null)
				{
					ItemBalanceExpireMetadata.meta = new ItemBalanceExpireMetadata();
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
				

				meta.AddTypeMap("LocationID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExpiredDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("QuantityIn", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QuantityOut", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemBalanceExpire";
				meta.Destination = "ItemBalanceExpire";
				
				meta.spInsert = "proc_ItemBalanceExpireInsert";				
				meta.spUpdate = "proc_ItemBalanceExpireUpdate";		
				meta.spDelete = "proc_ItemBalanceExpireDelete";
				meta.spLoadAll = "proc_ItemBalanceExpireLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemBalanceExpireLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemBalanceExpireMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
