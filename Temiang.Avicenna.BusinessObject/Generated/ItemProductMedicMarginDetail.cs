/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 1/1/2013 4:05:58 PM
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
	abstract public class esItemProductMedicMarginDetailCollection : esEntityCollectionWAuditLog
	{
		public esItemProductMedicMarginDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemProductMedicMarginDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemProductMedicMarginDetailQuery query)
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
			this.InitQuery(query as esItemProductMedicMarginDetailQuery);
		}
		#endregion
		
		virtual public ItemProductMedicMarginDetail DetachEntity(ItemProductMedicMarginDetail entity)
		{
			return base.DetachEntity(entity) as ItemProductMedicMarginDetail;
		}
		
		virtual public ItemProductMedicMarginDetail AttachEntity(ItemProductMedicMarginDetail entity)
		{
			return base.AttachEntity(entity) as ItemProductMedicMarginDetail;
		}
		
		virtual public void Combine(ItemProductMedicMarginDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemProductMedicMarginDetail this[int index]
		{
			get
			{
				return base[index] as ItemProductMedicMarginDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemProductMedicMarginDetail);
		}
	}



	[Serializable]
	abstract public class esItemProductMedicMarginDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemProductMedicMarginDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemProductMedicMarginDetail()
		{

		}

		public esItemProductMedicMarginDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String itemID, System.String classID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, classID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, classID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String itemID, System.String classID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(itemID, classID);
			else
				return LoadByPrimaryKeyStoredProcedure(itemID, classID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String itemID, System.String classID)
		{
			esItemProductMedicMarginDetailQuery query = this.GetDynamicQuery();
			query.Where(query.ItemID == itemID, query.ClassID == classID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String itemID, System.String classID)
		{
			esParameters parms = new esParameters();
			parms.Add("ItemID",itemID);			parms.Add("ClassID",classID);
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
						case "ClassID": this.str.ClassID = (string)value; break;							
						case "AmountPercentage": this.str.AmountPercentage = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "AmountPercentage":
						
							if (value == null || value is System.Decimal)
								this.AmountPercentage = (System.Decimal?)value;
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
		/// Maps to ItemProductMedicMarginDetail.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMarginDetailMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemProductMedicMarginDetailMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMedicMarginDetail.ClassID
		/// </summary>
		virtual public System.String ClassID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMarginDetailMetadata.ColumnNames.ClassID);
			}
			
			set
			{
				base.SetSystemString(ItemProductMedicMarginDetailMetadata.ColumnNames.ClassID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMedicMarginDetail.AmountPercentage
		/// </summary>
		virtual public System.Decimal? AmountPercentage
		{
			get
			{
				return base.GetSystemDecimal(ItemProductMedicMarginDetailMetadata.ColumnNames.AmountPercentage);
			}
			
			set
			{
				base.SetSystemDecimal(ItemProductMedicMarginDetailMetadata.ColumnNames.AmountPercentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMedicMarginDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemProductMedicMarginDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemProductMedicMarginDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductMedicMarginDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemProductMedicMarginDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemProductMedicMarginDetailMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemProductMedicMarginDetail entity)
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
				
			public System.String ClassID
			{
				get
				{
					System.String data = entity.ClassID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ClassID = null;
					else entity.ClassID = Convert.ToString(value);
				}
			}
				
			public System.String AmountPercentage
			{
				get
				{
					System.Decimal? data = entity.AmountPercentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountPercentage = null;
					else entity.AmountPercentage = Convert.ToDecimal(value);
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
			

			private esItemProductMedicMarginDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemProductMedicMarginDetailQuery query)
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
				throw new Exception("esItemProductMedicMarginDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemProductMedicMarginDetail : esItemProductMedicMarginDetail
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
	abstract public class esItemProductMedicMarginDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductMedicMarginDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMarginDetailMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ClassID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMarginDetailMetadata.ColumnNames.ClassID, esSystemType.String);
			}
		} 
		
		public esQueryItem AmountPercentage
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMarginDetailMetadata.ColumnNames.AmountPercentage, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMarginDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemProductMedicMarginDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemProductMedicMarginDetailCollection")]
	public partial class ItemProductMedicMarginDetailCollection : esItemProductMedicMarginDetailCollection, IEnumerable<ItemProductMedicMarginDetail>
	{
		public ItemProductMedicMarginDetailCollection()
		{

		}
		
		public static implicit operator List<ItemProductMedicMarginDetail>(ItemProductMedicMarginDetailCollection coll)
		{
			List<ItemProductMedicMarginDetail> list = new List<ItemProductMedicMarginDetail>();
			
			foreach (ItemProductMedicMarginDetail emp in coll)
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
				return  ItemProductMedicMarginDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductMedicMarginDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemProductMedicMarginDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemProductMedicMarginDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemProductMedicMarginDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductMedicMarginDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemProductMedicMarginDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemProductMedicMarginDetail AddNew()
		{
			ItemProductMedicMarginDetail entity = base.AddNewEntity() as ItemProductMedicMarginDetail;
			
			return entity;
		}

		public ItemProductMedicMarginDetail FindByPrimaryKey(System.String itemID, System.String classID)
		{
			return base.FindByPrimaryKey(itemID, classID) as ItemProductMedicMarginDetail;
		}


		#region IEnumerable<ItemProductMedicMarginDetail> Members

		IEnumerator<ItemProductMedicMarginDetail> IEnumerable<ItemProductMedicMarginDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemProductMedicMarginDetail;
			}
		}

		#endregion
		
		private ItemProductMedicMarginDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemProductMedicMarginDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemProductMedicMarginDetail ({ItemID},{ClassID})")]
	[Serializable]
	public partial class ItemProductMedicMarginDetail : esItemProductMedicMarginDetail
	{
		public ItemProductMedicMarginDetail()
		{

		}
	
		public ItemProductMedicMarginDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductMedicMarginDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esItemProductMedicMarginDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductMedicMarginDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemProductMedicMarginDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductMedicMarginDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemProductMedicMarginDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemProductMedicMarginDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemProductMedicMarginDetailQuery : esItemProductMedicMarginDetailQuery
	{
		public ItemProductMedicMarginDetailQuery()
		{

		}		
		
		public ItemProductMedicMarginDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemProductMedicMarginDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemProductMedicMarginDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemProductMedicMarginDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemProductMedicMarginDetailMetadata.ColumnNames.ItemID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMarginDetailMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMedicMarginDetailMetadata.ColumnNames.ClassID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMarginDetailMetadata.PropertyNames.ClassID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMedicMarginDetailMetadata.ColumnNames.AmountPercentage, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductMedicMarginDetailMetadata.PropertyNames.AmountPercentage;
			c.NumericPrecision = 5;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMedicMarginDetailMetadata.ColumnNames.LastUpdateDateTime, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemProductMedicMarginDetailMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductMedicMarginDetailMetadata.ColumnNames.LastUpdateByUserID, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductMedicMarginDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemProductMedicMarginDetailMetadata Meta()
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
			 public const string ClassID = "ClassID";
			 public const string AmountPercentage = "AmountPercentage";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ItemID = "ItemID";
			 public const string ClassID = "ClassID";
			 public const string AmountPercentage = "AmountPercentage";
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
			lock (typeof(ItemProductMedicMarginDetailMetadata))
			{
				if(ItemProductMedicMarginDetailMetadata.mapDelegates == null)
				{
					ItemProductMedicMarginDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemProductMedicMarginDetailMetadata.meta == null)
				{
					ItemProductMedicMarginDetailMetadata.meta = new ItemProductMedicMarginDetailMetadata();
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
				meta.AddTypeMap("ClassID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AmountPercentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemProductMedicMarginDetail";
				meta.Destination = "ItemProductMedicMarginDetail";
				
				meta.spInsert = "proc_ItemProductMedicMarginDetailInsert";				
				meta.spUpdate = "proc_ItemProductMedicMarginDetailUpdate";		
				meta.spDelete = "proc_ItemProductMedicMarginDetailDelete";
				meta.spLoadAll = "proc_ItemProductMedicMarginDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemProductMedicMarginDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemProductMedicMarginDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
