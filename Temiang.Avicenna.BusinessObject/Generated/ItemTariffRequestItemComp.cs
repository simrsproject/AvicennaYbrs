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
	abstract public class esItemTariffRequestItemCompCollection : esEntityCollectionWAuditLog
	{
		public esItemTariffRequestItemCompCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemTariffRequestItemCompCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemTariffRequestItemCompQuery query)
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
			this.InitQuery(query as esItemTariffRequestItemCompQuery);
		}
		#endregion
		
		virtual public ItemTariffRequestItemComp DetachEntity(ItemTariffRequestItemComp entity)
		{
			return base.DetachEntity(entity) as ItemTariffRequestItemComp;
		}
		
		virtual public ItemTariffRequestItemComp AttachEntity(ItemTariffRequestItemComp entity)
		{
			return base.AttachEntity(entity) as ItemTariffRequestItemComp;
		}
		
		virtual public void Combine(ItemTariffRequestItemCompCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemTariffRequestItemComp this[int index]
		{
			get
			{
				return base[index] as ItemTariffRequestItemComp;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemTariffRequestItemComp);
		}
	}



	[Serializable]
	abstract public class esItemTariffRequestItemComp : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemTariffRequestItemCompQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemTariffRequestItemComp()
		{

		}

		public esItemTariffRequestItemComp(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String tariffRequestNo, System.String itemID, System.String tariffComponentID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(tariffRequestNo, itemID, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(tariffRequestNo, itemID, tariffComponentID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String tariffRequestNo, System.String itemID, System.String tariffComponentID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(tariffRequestNo, itemID, tariffComponentID);
			else
				return LoadByPrimaryKeyStoredProcedure(tariffRequestNo, itemID, tariffComponentID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String tariffRequestNo, System.String itemID, System.String tariffComponentID)
		{
			esItemTariffRequestItemCompQuery query = this.GetDynamicQuery();
			query.Where(query.TariffRequestNo == tariffRequestNo, query.ItemID == itemID, query.TariffComponentID == tariffComponentID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String tariffRequestNo, System.String itemID, System.String tariffComponentID)
		{
			esParameters parms = new esParameters();
			parms.Add("TariffRequestNo",tariffRequestNo);			parms.Add("ItemID",itemID);			parms.Add("TariffComponentID",tariffComponentID);
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
						case "TariffRequestNo": this.str.TariffRequestNo = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "TariffComponentID": this.str.TariffComponentID = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "IsAllowDiscount": this.str.IsAllowDiscount = (string)value; break;							
						case "IsAllowVariable": this.str.IsAllowVariable = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Price":
						
							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						
						case "IsAllowDiscount":
						
							if (value == null || value is System.Boolean)
								this.IsAllowDiscount = (System.Boolean?)value;
							break;
						
						case "IsAllowVariable":
						
							if (value == null || value is System.Boolean)
								this.IsAllowVariable = (System.Boolean?)value;
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
		/// Maps to ItemTariffRequestItemComp.TariffRequestNo
		/// </summary>
		virtual public System.String TariffRequestNo
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestItemCompMetadata.ColumnNames.TariffRequestNo);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequestItemCompMetadata.ColumnNames.TariffRequestNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequestItemComp.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestItemCompMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequestItemCompMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequestItemComp.TariffComponentID
		/// </summary>
		virtual public System.String TariffComponentID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestItemCompMetadata.ColumnNames.TariffComponentID);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequestItemCompMetadata.ColumnNames.TariffComponentID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequestItemComp.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(ItemTariffRequestItemCompMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTariffRequestItemCompMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Bisa di set True bila di master itemnya IsAllowDiscount=1
		/// </summary>
		virtual public System.Boolean? IsAllowDiscount
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffRequestItemCompMetadata.ColumnNames.IsAllowDiscount);
			}
			
			set
			{
				base.SetSystemBoolean(ItemTariffRequestItemCompMetadata.ColumnNames.IsAllowDiscount, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequestItemComp.IsAllowVariable
		/// </summary>
		virtual public System.Boolean? IsAllowVariable
		{
			get
			{
				return base.GetSystemBoolean(ItemTariffRequestItemCompMetadata.ColumnNames.IsAllowVariable);
			}
			
			set
			{
				base.SetSystemBoolean(ItemTariffRequestItemCompMetadata.ColumnNames.IsAllowVariable, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequestItemComp.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTariffRequestItemCompMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemTariffRequestItemCompMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTariffRequestItemComp.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTariffRequestItemCompMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemTariffRequestItemCompMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemTariffRequestItemComp entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TariffRequestNo
			{
				get
				{
					System.String data = entity.TariffRequestNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffRequestNo = null;
					else entity.TariffRequestNo = Convert.ToString(value);
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
				
			public System.String TariffComponentID
			{
				get
				{
					System.String data = entity.TariffComponentID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TariffComponentID = null;
					else entity.TariffComponentID = Convert.ToString(value);
				}
			}
				
			public System.String Price
			{
				get
				{
					System.Decimal? data = entity.Price;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Price = null;
					else entity.Price = Convert.ToDecimal(value);
				}
			}
				
			public System.String IsAllowDiscount
			{
				get
				{
					System.Boolean? data = entity.IsAllowDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowDiscount = null;
					else entity.IsAllowDiscount = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsAllowVariable
			{
				get
				{
					System.Boolean? data = entity.IsAllowVariable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAllowVariable = null;
					else entity.IsAllowVariable = Convert.ToBoolean(value);
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
			

			private esItemTariffRequestItemComp entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemTariffRequestItemCompQuery query)
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
				throw new Exception("esItemTariffRequestItemComp can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemTariffRequestItemComp : esItemTariffRequestItemComp
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
	abstract public class esItemTariffRequestItemCompQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffRequestItemCompMetadata.Meta();
			}
		}	
		

		public esQueryItem TariffRequestNo
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemCompMetadata.ColumnNames.TariffRequestNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemCompMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem TariffComponentID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemCompMetadata.ColumnNames.TariffComponentID, esSystemType.String);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemCompMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsAllowDiscount
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemCompMetadata.ColumnNames.IsAllowDiscount, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsAllowVariable
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemCompMetadata.ColumnNames.IsAllowVariable, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemCompMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTariffRequestItemCompMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemTariffRequestItemCompCollection")]
	public partial class ItemTariffRequestItemCompCollection : esItemTariffRequestItemCompCollection, IEnumerable<ItemTariffRequestItemComp>
	{
		public ItemTariffRequestItemCompCollection()
		{

		}
		
		public static implicit operator List<ItemTariffRequestItemComp>(ItemTariffRequestItemCompCollection coll)
		{
			List<ItemTariffRequestItemComp> list = new List<ItemTariffRequestItemComp>();
			
			foreach (ItemTariffRequestItemComp emp in coll)
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
				return  ItemTariffRequestItemCompMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffRequestItemCompQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemTariffRequestItemComp(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemTariffRequestItemComp();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemTariffRequestItemCompQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffRequestItemCompQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemTariffRequestItemCompQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemTariffRequestItemComp AddNew()
		{
			ItemTariffRequestItemComp entity = base.AddNewEntity() as ItemTariffRequestItemComp;
			
			return entity;
		}

		public ItemTariffRequestItemComp FindByPrimaryKey(System.String tariffRequestNo, System.String itemID, System.String tariffComponentID)
		{
			return base.FindByPrimaryKey(tariffRequestNo, itemID, tariffComponentID) as ItemTariffRequestItemComp;
		}


		#region IEnumerable<ItemTariffRequestItemComp> Members

		IEnumerator<ItemTariffRequestItemComp> IEnumerable<ItemTariffRequestItemComp>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemTariffRequestItemComp;
			}
		}

		#endregion
		
		private ItemTariffRequestItemCompQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemTariffRequestItemComp' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemTariffRequestItemComp ({TariffRequestNo},{ItemID},{TariffComponentID})")]
	[Serializable]
	public partial class ItemTariffRequestItemComp : esItemTariffRequestItemComp
	{
		public ItemTariffRequestItemComp()
		{

		}
	
		public ItemTariffRequestItemComp(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemTariffRequestItemCompMetadata.Meta();
			}
		}
		
		
		
		override protected esItemTariffRequestItemCompQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTariffRequestItemCompQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemTariffRequestItemCompQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTariffRequestItemCompQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemTariffRequestItemCompQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemTariffRequestItemCompQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemTariffRequestItemCompQuery : esItemTariffRequestItemCompQuery
	{
		public ItemTariffRequestItemCompQuery()
		{

		}		
		
		public ItemTariffRequestItemCompQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemTariffRequestItemCompQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemTariffRequestItemCompMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemTariffRequestItemCompMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemTariffRequestItemCompMetadata.ColumnNames.TariffRequestNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestItemCompMetadata.PropertyNames.TariffRequestNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestItemCompMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestItemCompMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestItemCompMetadata.ColumnNames.TariffComponentID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestItemCompMetadata.PropertyNames.TariffComponentID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestItemCompMetadata.ColumnNames.Price, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTariffRequestItemCompMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestItemCompMetadata.ColumnNames.IsAllowDiscount, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffRequestItemCompMetadata.PropertyNames.IsAllowDiscount;
			c.Description = "Bisa di set True bila di master itemnya IsAllowDiscount=1";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestItemCompMetadata.ColumnNames.IsAllowVariable, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTariffRequestItemCompMetadata.PropertyNames.IsAllowVariable;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestItemCompMetadata.ColumnNames.LastUpdateDateTime, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTariffRequestItemCompMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTariffRequestItemCompMetadata.ColumnNames.LastUpdateByUserID, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTariffRequestItemCompMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemTariffRequestItemCompMetadata Meta()
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
			 public const string TariffRequestNo = "TariffRequestNo";
			 public const string ItemID = "ItemID";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string Price = "Price";
			 public const string IsAllowDiscount = "IsAllowDiscount";
			 public const string IsAllowVariable = "IsAllowVariable";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TariffRequestNo = "TariffRequestNo";
			 public const string ItemID = "ItemID";
			 public const string TariffComponentID = "TariffComponentID";
			 public const string Price = "Price";
			 public const string IsAllowDiscount = "IsAllowDiscount";
			 public const string IsAllowVariable = "IsAllowVariable";
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
			lock (typeof(ItemTariffRequestItemCompMetadata))
			{
				if(ItemTariffRequestItemCompMetadata.mapDelegates == null)
				{
					ItemTariffRequestItemCompMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemTariffRequestItemCompMetadata.meta == null)
				{
					ItemTariffRequestItemCompMetadata.meta = new ItemTariffRequestItemCompMetadata();
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
				

				meta.AddTypeMap("TariffRequestNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TariffComponentID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsAllowDiscount", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAllowVariable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemTariffRequestItemComp";
				meta.Destination = "ItemTariffRequestItemComp";
				
				meta.spInsert = "proc_ItemTariffRequestItemCompInsert";				
				meta.spUpdate = "proc_ItemTariffRequestItemCompUpdate";		
				meta.spDelete = "proc_ItemTariffRequestItemCompDelete";
				meta.spLoadAll = "proc_ItemTariffRequestItemCompLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemTariffRequestItemCompLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemTariffRequestItemCompMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
