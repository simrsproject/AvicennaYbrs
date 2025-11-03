/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 5/28/2014 3:07:40 PM
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
	abstract public class esItemProductDosageDetailCollection : esEntityCollectionWAuditLog
	{
		public esItemProductDosageDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemProductDosageDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemProductDosageDetailQuery query)
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
			this.InitQuery(query as esItemProductDosageDetailQuery);
		}
		#endregion
		
		virtual public ItemProductDosageDetail DetachEntity(ItemProductDosageDetail entity)
		{
			return base.DetachEntity(entity) as ItemProductDosageDetail;
		}
		
		virtual public ItemProductDosageDetail AttachEntity(ItemProductDosageDetail entity)
		{
			return base.AttachEntity(entity) as ItemProductDosageDetail;
		}
		
		virtual public void Combine(ItemProductDosageDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemProductDosageDetail this[int index]
		{
			get
			{
				return base[index] as ItemProductDosageDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemProductDosageDetail);
		}
	}



	[Serializable]
	abstract public class esItemProductDosageDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemProductDosageDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemProductDosageDetail()
		{

		}

		public esItemProductDosageDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String itemID, System.String sRDosageUnit)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, sRDosageUnit);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, sRDosageUnit);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID, System.String sRDosageUnit)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, sRDosageUnit);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, sRDosageUnit);
		}

		private bool LoadByPrimaryKeyDynamic(System.String itemID, System.String sRDosageUnit)
		{
			esItemProductDosageDetailQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID, query.SRDosageUnit == sRDosageUnit);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String itemID, System.String sRDosageUnit)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID",itemID);			parms.Add("SRDosageUnit",sRDosageUnit);
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
						case "Dosage": this.str.Dosage = (string)value; break;							
						case "SRDosageUnit": this.str.SRDosageUnit = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Dosage":
						
							if (value == null || value is System.Decimal)
								this.Dosage = (System.Decimal?)value;
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
		/// Maps to ItemProductDosageDetail.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemProductDosageDetailMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemProductDosageDetailMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductDosageDetail.Dosage
		/// </summary>
		virtual public System.Decimal? Dosage
		{
			get
			{
				return base.GetSystemDecimal(ItemProductDosageDetailMetadata.ColumnNames.Dosage);
			}
			
			set
			{
				base.SetSystemDecimal(ItemProductDosageDetailMetadata.ColumnNames.Dosage, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductDosageDetail.SRDosageUnit
		/// </summary>
		virtual public System.String SRDosageUnit
		{
			get
			{
				return base.GetSystemString(ItemProductDosageDetailMetadata.ColumnNames.SRDosageUnit);
			}
			
			set
			{
				base.SetSystemString(ItemProductDosageDetailMetadata.ColumnNames.SRDosageUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductDosageDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemProductDosageDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemProductDosageDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductDosageDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemProductDosageDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemProductDosageDetailMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemProductDosageDetail entity)
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
				
			public System.String Dosage
			{
				get
				{
					System.Decimal? data = entity.Dosage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Dosage = null;
					else entity.Dosage = Convert.ToDecimal(value);
				}
			}
				
			public System.String SRDosageUnit
			{
				get
				{
					System.String data = entity.SRDosageUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRDosageUnit = null;
					else entity.SRDosageUnit = Convert.ToString(value);
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
			

			private esItemProductDosageDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemProductDosageDetailQuery query)
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
				throw new Exception("esItemProductDosageDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esItemProductDosageDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductDosageDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemProductDosageDetailMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem Dosage
		{
			get
			{
				return new esQueryItem(this, ItemProductDosageDetailMetadata.ColumnNames.Dosage, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SRDosageUnit
		{
			get
			{
				return new esQueryItem(this, ItemProductDosageDetailMetadata.ColumnNames.SRDosageUnit, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemProductDosageDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemProductDosageDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemProductDosageDetailCollection")]
	public partial class ItemProductDosageDetailCollection : esItemProductDosageDetailCollection, IEnumerable<ItemProductDosageDetail>
	{
		public ItemProductDosageDetailCollection()
		{

		}
		
		public static implicit operator List<ItemProductDosageDetail>(ItemProductDosageDetailCollection coll)
		{
			List<ItemProductDosageDetail> list = new List<ItemProductDosageDetail>();
			
			foreach (ItemProductDosageDetail emp in coll)
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
				return  ItemProductDosageDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductDosageDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemProductDosageDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemProductDosageDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemProductDosageDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductDosageDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemProductDosageDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemProductDosageDetail AddNew()
		{
			ItemProductDosageDetail entity = base.AddNewEntity() as ItemProductDosageDetail;
			
			return entity;
		}

		public ItemProductDosageDetail FindByPrimaryKey(System.String itemID, System.String sRDosageUnit)
		{
			return base.FindByPrimaryKey(itemID, sRDosageUnit) as ItemProductDosageDetail;
		}


		#region IEnumerable<ItemProductDosageDetail> Members

		IEnumerator<ItemProductDosageDetail> IEnumerable<ItemProductDosageDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemProductDosageDetail;
			}
		}

		#endregion
		
		private ItemProductDosageDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemProductDosageDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemProductDosageDetail ({ItemID},{SRDosageUnit})")]
	[Serializable]
	public partial class ItemProductDosageDetail : esItemProductDosageDetail
	{
		public ItemProductDosageDetail()
		{

		}
	
		public ItemProductDosageDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductDosageDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esItemProductDosageDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductDosageDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemProductDosageDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductDosageDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemProductDosageDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemProductDosageDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemProductDosageDetailQuery : esItemProductDosageDetailQuery
	{
		public ItemProductDosageDetailQuery()
		{

		}		
		
		public ItemProductDosageDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemProductDosageDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemProductDosageDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemProductDosageDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemProductDosageDetailMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductDosageDetailMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductDosageDetailMetadata.ColumnNames.Dosage, 1, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductDosageDetailMetadata.PropertyNames.Dosage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductDosageDetailMetadata.ColumnNames.SRDosageUnit, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductDosageDetailMetadata.PropertyNames.SRDosageUnit;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductDosageDetailMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemProductDosageDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductDosageDetailMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductDosageDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemProductDosageDetailMetadata Meta()
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
			 public const string Dosage = "Dosage";
			 public const string SRDosageUnit = "SRDosageUnit";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ItemID = "ItemID";
			 public const string Dosage = "Dosage";
			 public const string SRDosageUnit = "SRDosageUnit";
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
			lock (typeof(ItemProductDosageDetailMetadata))
			{
				if(ItemProductDosageDetailMetadata.mapDelegates == null)
				{
					ItemProductDosageDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemProductDosageDetailMetadata.meta == null)
				{
					ItemProductDosageDetailMetadata.meta = new ItemProductDosageDetailMetadata();
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
				meta.AddTypeMap("Dosage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRDosageUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemProductDosageDetail";
				meta.Destination = "ItemProductDosageDetail";
				
				meta.spInsert = "proc_ItemProductDosageDetailInsert";				
				meta.spUpdate = "proc_ItemProductDosageDetailUpdate";		
				meta.spDelete = "proc_ItemProductDosageDetailDelete";
				meta.spLoadAll = "proc_ItemProductDosageDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemProductDosageDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemProductDosageDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
