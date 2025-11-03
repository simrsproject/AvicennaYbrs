/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 7/5/2014 1:12:49 PM
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
	abstract public class esItemProductDeductionDetailCollection : esEntityCollectionWAuditLog
	{
		public esItemProductDeductionDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemProductDeductionDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemProductDeductionDetailQuery query)
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
			this.InitQuery(query as esItemProductDeductionDetailQuery);
		}
		#endregion
		
		virtual public ItemProductDeductionDetail DetachEntity(ItemProductDeductionDetail entity)
		{
			return base.DetachEntity(entity) as ItemProductDeductionDetail;
		}
		
		virtual public ItemProductDeductionDetail AttachEntity(ItemProductDeductionDetail entity)
		{
			return base.AttachEntity(entity) as ItemProductDeductionDetail;
		}
		
		virtual public void Combine(ItemProductDeductionDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemProductDeductionDetail this[int index]
		{
			get
			{
				return base[index] as ItemProductDeductionDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemProductDeductionDetail);
		}
	}



	[Serializable]
	abstract public class esItemProductDeductionDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemProductDeductionDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemProductDeductionDetail()
		{

		}

		public esItemProductDeductionDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String deductionID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(deductionID);
			else
				return LoadByPrimaryKeyStoredProcedure(deductionID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String deductionID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(deductionID);
			else
				return LoadByPrimaryKeyStoredProcedure(deductionID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String deductionID)
		{
			esItemProductDeductionDetailQuery query = this.GetDynamicQuery();
			query.Where(query.DeductionID == deductionID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String deductionID)
		{
			esParameters parms = new esParameters();
			parms.Add("DeductionID",deductionID);
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
						case "DeductionID": this.str.DeductionID = (string)value; break;							
						case "MinAmount": this.str.MinAmount = (string)value; break;							
						case "MaxAmount": this.str.MaxAmount = (string)value; break;							
						case "DeductionAmount": this.str.DeductionAmount = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "MinAmount":
						
							if (value == null || value is System.Decimal)
								this.MinAmount = (System.Decimal?)value;
							break;
						
						case "MaxAmount":
						
							if (value == null || value is System.Decimal)
								this.MaxAmount = (System.Decimal?)value;
							break;
						
						case "DeductionAmount":
						
							if (value == null || value is System.Decimal)
								this.DeductionAmount = (System.Decimal?)value;
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
		/// Maps to ItemProductDeductionDetail.DeductionID
		/// </summary>
		virtual public System.String DeductionID
		{
			get
			{
				return base.GetSystemString(ItemProductDeductionDetailMetadata.ColumnNames.DeductionID);
			}
			
			set
			{
				base.SetSystemString(ItemProductDeductionDetailMetadata.ColumnNames.DeductionID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductDeductionDetail.MinAmount
		/// </summary>
		virtual public System.Decimal? MinAmount
		{
			get
			{
				return base.GetSystemDecimal(ItemProductDeductionDetailMetadata.ColumnNames.MinAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ItemProductDeductionDetailMetadata.ColumnNames.MinAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductDeductionDetail.MaxAmount
		/// </summary>
		virtual public System.Decimal? MaxAmount
		{
			get
			{
				return base.GetSystemDecimal(ItemProductDeductionDetailMetadata.ColumnNames.MaxAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ItemProductDeductionDetailMetadata.ColumnNames.MaxAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductDeductionDetail.DeductionAmount
		/// </summary>
		virtual public System.Decimal? DeductionAmount
		{
			get
			{
				return base.GetSystemDecimal(ItemProductDeductionDetailMetadata.ColumnNames.DeductionAmount);
			}
			
			set
			{
				base.SetSystemDecimal(ItemProductDeductionDetailMetadata.ColumnNames.DeductionAmount, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductDeductionDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemProductDeductionDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemProductDeductionDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemProductDeductionDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemProductDeductionDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemProductDeductionDetailMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemProductDeductionDetail entity)
			{
				this.entity = entity;
			}
			
	
			public System.String DeductionID
			{
				get
				{
					System.String data = entity.DeductionID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionID = null;
					else entity.DeductionID = Convert.ToString(value);
				}
			}
				
			public System.String MinAmount
			{
				get
				{
					System.Decimal? data = entity.MinAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinAmount = null;
					else entity.MinAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String MaxAmount
			{
				get
				{
					System.Decimal? data = entity.MaxAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaxAmount = null;
					else entity.MaxAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String DeductionAmount
			{
				get
				{
					System.Decimal? data = entity.DeductionAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeductionAmount = null;
					else entity.DeductionAmount = Convert.ToDecimal(value);
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
			

			private esItemProductDeductionDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemProductDeductionDetailQuery query)
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
				throw new Exception("esItemProductDeductionDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}



	[Serializable]
	abstract public class esItemProductDeductionDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductDeductionDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem DeductionID
		{
			get
			{
				return new esQueryItem(this, ItemProductDeductionDetailMetadata.ColumnNames.DeductionID, esSystemType.String);
			}
		} 
		
		public esQueryItem MinAmount
		{
			get
			{
				return new esQueryItem(this, ItemProductDeductionDetailMetadata.ColumnNames.MinAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem MaxAmount
		{
			get
			{
				return new esQueryItem(this, ItemProductDeductionDetailMetadata.ColumnNames.MaxAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DeductionAmount
		{
			get
			{
				return new esQueryItem(this, ItemProductDeductionDetailMetadata.ColumnNames.DeductionAmount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemProductDeductionDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemProductDeductionDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemProductDeductionDetailCollection")]
	public partial class ItemProductDeductionDetailCollection : esItemProductDeductionDetailCollection, IEnumerable<ItemProductDeductionDetail>
	{
		public ItemProductDeductionDetailCollection()
		{

		}
		
		public static implicit operator List<ItemProductDeductionDetail>(ItemProductDeductionDetailCollection coll)
		{
			List<ItemProductDeductionDetail> list = new List<ItemProductDeductionDetail>();
			
			foreach (ItemProductDeductionDetail emp in coll)
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
				return  ItemProductDeductionDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductDeductionDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemProductDeductionDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemProductDeductionDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemProductDeductionDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductDeductionDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemProductDeductionDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemProductDeductionDetail AddNew()
		{
			ItemProductDeductionDetail entity = base.AddNewEntity() as ItemProductDeductionDetail;
			
			return entity;
		}

		public ItemProductDeductionDetail FindByPrimaryKey(System.String deductionID)
		{
			return base.FindByPrimaryKey(deductionID) as ItemProductDeductionDetail;
		}


		#region IEnumerable<ItemProductDeductionDetail> Members

		IEnumerator<ItemProductDeductionDetail> IEnumerable<ItemProductDeductionDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemProductDeductionDetail;
			}
		}

		#endregion
		
		private ItemProductDeductionDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemProductDeductionDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemProductDeductionDetail ({DeductionID})")]
	[Serializable]
	public partial class ItemProductDeductionDetail : esItemProductDeductionDetail
	{
		public ItemProductDeductionDetail()
		{

		}
	
		public ItemProductDeductionDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemProductDeductionDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esItemProductDeductionDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemProductDeductionDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemProductDeductionDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemProductDeductionDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemProductDeductionDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemProductDeductionDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemProductDeductionDetailQuery : esItemProductDeductionDetailQuery
	{
		public ItemProductDeductionDetailQuery()
		{

		}		
		
		public ItemProductDeductionDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemProductDeductionDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemProductDeductionDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemProductDeductionDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemProductDeductionDetailMetadata.ColumnNames.DeductionID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductDeductionDetailMetadata.PropertyNames.DeductionID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductDeductionDetailMetadata.ColumnNames.MinAmount, 1, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductDeductionDetailMetadata.PropertyNames.MinAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductDeductionDetailMetadata.ColumnNames.MaxAmount, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductDeductionDetailMetadata.PropertyNames.MaxAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductDeductionDetailMetadata.ColumnNames.DeductionAmount, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemProductDeductionDetailMetadata.PropertyNames.DeductionAmount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductDeductionDetailMetadata.ColumnNames.LastUpdateDateTime, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemProductDeductionDetailMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemProductDeductionDetailMetadata.ColumnNames.LastUpdateByUserID, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemProductDeductionDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemProductDeductionDetailMetadata Meta()
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
			 public const string DeductionID = "DeductionID";
			 public const string MinAmount = "MinAmount";
			 public const string MaxAmount = "MaxAmount";
			 public const string DeductionAmount = "DeductionAmount";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string DeductionID = "DeductionID";
			 public const string MinAmount = "MinAmount";
			 public const string MaxAmount = "MaxAmount";
			 public const string DeductionAmount = "DeductionAmount";
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
			lock (typeof(ItemProductDeductionDetailMetadata))
			{
				if(ItemProductDeductionDetailMetadata.mapDelegates == null)
				{
					ItemProductDeductionDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemProductDeductionDetailMetadata.meta == null)
				{
					ItemProductDeductionDetailMetadata.meta = new ItemProductDeductionDetailMetadata();
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
				

				meta.AddTypeMap("DeductionID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MinAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("MaxAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DeductionAmount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemProductDeductionDetail";
				meta.Destination = "ItemProductDeductionDetail";
				
				meta.spInsert = "proc_ItemProductDeductionDetailInsert";				
				meta.spUpdate = "proc_ItemProductDeductionDetailUpdate";		
				meta.spDelete = "proc_ItemProductDeductionDetailDelete";
				meta.spLoadAll = "proc_ItemProductDeductionDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemProductDeductionDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemProductDeductionDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
