/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 11/28/2013 11:23:18 AM
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
	abstract public class esItemBalanceDetailCollection : esEntityCollectionWAuditLog
	{
		public esItemBalanceDetailCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemBalanceDetailCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemBalanceDetailQuery query)
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
			this.InitQuery(query as esItemBalanceDetailQuery);
		}
		#endregion
		
		virtual public ItemBalanceDetail DetachEntity(ItemBalanceDetail entity)
		{
			return base.DetachEntity(entity) as ItemBalanceDetail;
		}
		
		virtual public ItemBalanceDetail AttachEntity(ItemBalanceDetail entity)
		{
			return base.AttachEntity(entity) as ItemBalanceDetail;
		}
		
		virtual public void Combine(ItemBalanceDetailCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemBalanceDetail this[int index]
		{
			get
			{
				return base[index] as ItemBalanceDetail;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemBalanceDetail);
		}
	}



	[Serializable]
	abstract public class esItemBalanceDetail : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemBalanceDetailQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemBalanceDetail()
		{

		}

		public esItemBalanceDetail(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.DateTime balanceDate, System.String itemID, System.String locationID, System.String referenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(balanceDate, itemID, locationID, referenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(balanceDate, itemID, locationID, referenceNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.DateTime balanceDate, System.String itemID, System.String locationID, System.String referenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(balanceDate, itemID, locationID, referenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(balanceDate, itemID, locationID, referenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.DateTime balanceDate, System.String itemID, System.String locationID, System.String referenceNo)
		{
			esItemBalanceDetailQuery query = this.GetDynamicQuery();
			query.Where(query.BalanceDate == balanceDate, query.ItemID == itemID, query.LocationID == locationID, query.ReferenceNo == referenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.DateTime balanceDate, System.String itemID, System.String locationID, System.String referenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("BalanceDate",balanceDate);			parms.Add("ItemID",itemID);			parms.Add("LocationID",locationID);			parms.Add("ReferenceNo",referenceNo);
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
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;							
						case "TransactionCode": this.str.TransactionCode = (string)value; break;							
						case "BalanceDate": this.str.BalanceDate = (string)value; break;							
						case "Balance": this.str.Balance = (string)value; break;							
						case "Booking": this.str.Booking = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "PurchaseReceiveNo": this.str.PurchaseReceiveNo = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "BalanceDate":
						
							if (value == null || value is System.DateTime)
								this.BalanceDate = (System.DateTime?)value;
							break;
						
						case "Balance":
						
							if (value == null || value is System.Decimal)
								this.Balance = (System.Decimal?)value;
							break;
						
						case "Booking":
						
							if (value == null || value is System.Decimal)
								this.Booking = (System.Decimal?)value;
							break;
						
						case "Price":
						
							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
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
		/// Maps to ItemBalanceDetail.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(ItemBalanceDetailMetadata.ColumnNames.LocationID);
			}
			
			set
			{
				base.SetSystemString(ItemBalanceDetailMetadata.ColumnNames.LocationID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceDetail.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemBalanceDetailMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemBalanceDetailMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceDetail.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(ItemBalanceDetailMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(ItemBalanceDetailMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceDetail.TransactionCode
		/// </summary>
		virtual public System.String TransactionCode
		{
			get
			{
				return base.GetSystemString(ItemBalanceDetailMetadata.ColumnNames.TransactionCode);
			}
			
			set
			{
				base.SetSystemString(ItemBalanceDetailMetadata.ColumnNames.TransactionCode, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceDetail.BalanceDate
		/// </summary>
		virtual public System.DateTime? BalanceDate
		{
			get
			{
				return base.GetSystemDateTime(ItemBalanceDetailMetadata.ColumnNames.BalanceDate);
			}
			
			set
			{
				base.SetSystemDateTime(ItemBalanceDetailMetadata.ColumnNames.BalanceDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceDetail.Balance
		/// </summary>
		virtual public System.Decimal? Balance
		{
			get
			{
				return base.GetSystemDecimal(ItemBalanceDetailMetadata.ColumnNames.Balance);
			}
			
			set
			{
				base.SetSystemDecimal(ItemBalanceDetailMetadata.ColumnNames.Balance, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceDetail.Booking
		/// </summary>
		virtual public System.Decimal? Booking
		{
			get
			{
				return base.GetSystemDecimal(ItemBalanceDetailMetadata.ColumnNames.Booking);
			}
			
			set
			{
				base.SetSystemDecimal(ItemBalanceDetailMetadata.ColumnNames.Booking, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceDetail.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(ItemBalanceDetailMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(ItemBalanceDetailMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceDetail.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemBalanceDetailMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemBalanceDetailMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceDetail.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemBalanceDetailMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemBalanceDetailMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceDetail.PurchaseReceiveNo
		/// </summary>
		virtual public System.String PurchaseReceiveNo
		{
			get
			{
				return base.GetSystemString(ItemBalanceDetailMetadata.ColumnNames.PurchaseReceiveNo);
			}
			
			set
			{
				base.SetSystemString(ItemBalanceDetailMetadata.ColumnNames.PurchaseReceiveNo, value);
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
			public esStrings(esItemBalanceDetail entity)
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
				
			public System.String ReferenceNo
			{
				get
				{
					System.String data = entity.ReferenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceNo = null;
					else entity.ReferenceNo = Convert.ToString(value);
				}
			}
				
			public System.String TransactionCode
			{
				get
				{
					System.String data = entity.TransactionCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionCode = null;
					else entity.TransactionCode = Convert.ToString(value);
				}
			}
				
			public System.String BalanceDate
			{
				get
				{
					System.DateTime? data = entity.BalanceDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BalanceDate = null;
					else entity.BalanceDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String Balance
			{
				get
				{
					System.Decimal? data = entity.Balance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Balance = null;
					else entity.Balance = Convert.ToDecimal(value);
				}
			}
				
			public System.String Booking
			{
				get
				{
					System.Decimal? data = entity.Booking;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Booking = null;
					else entity.Booking = Convert.ToDecimal(value);
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
				
			public System.String PurchaseReceiveNo
			{
				get
				{
					System.String data = entity.PurchaseReceiveNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PurchaseReceiveNo = null;
					else entity.PurchaseReceiveNo = Convert.ToString(value);
				}
			}
			

			private esItemBalanceDetail entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemBalanceDetailQuery query)
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
				throw new Exception("esItemBalanceDetail can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemBalanceDetail : esItemBalanceDetail
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
	abstract public class esItemBalanceDetailQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemBalanceDetailMetadata.Meta();
			}
		}	
		

		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem TransactionCode
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailMetadata.ColumnNames.TransactionCode, esSystemType.String);
			}
		} 
		
		public esQueryItem BalanceDate
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailMetadata.ColumnNames.BalanceDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem Balance
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailMetadata.ColumnNames.Balance, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Booking
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailMetadata.ColumnNames.Booking, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem PurchaseReceiveNo
		{
			get
			{
				return new esQueryItem(this, ItemBalanceDetailMetadata.ColumnNames.PurchaseReceiveNo, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemBalanceDetailCollection")]
	public partial class ItemBalanceDetailCollection : esItemBalanceDetailCollection, IEnumerable<ItemBalanceDetail>
	{
		public ItemBalanceDetailCollection()
		{

		}
		
		public static implicit operator List<ItemBalanceDetail>(ItemBalanceDetailCollection coll)
		{
			List<ItemBalanceDetail> list = new List<ItemBalanceDetail>();
			
			foreach (ItemBalanceDetail emp in coll)
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
				return  ItemBalanceDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemBalanceDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemBalanceDetail(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemBalanceDetail();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemBalanceDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemBalanceDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemBalanceDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemBalanceDetail AddNew()
		{
			ItemBalanceDetail entity = base.AddNewEntity() as ItemBalanceDetail;
			
			return entity;
		}

		public ItemBalanceDetail FindByPrimaryKey(System.DateTime balanceDate, System.String itemID, System.String locationID, System.String referenceNo)
		{
			return base.FindByPrimaryKey(balanceDate, itemID, locationID, referenceNo) as ItemBalanceDetail;
		}


		#region IEnumerable<ItemBalanceDetail> Members

		IEnumerator<ItemBalanceDetail> IEnumerable<ItemBalanceDetail>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemBalanceDetail;
			}
		}

		#endregion
		
		private ItemBalanceDetailQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemBalanceDetail' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemBalanceDetail ({LocationID},{ItemID},{ReferenceNo},{BalanceDate})")]
	[Serializable]
	public partial class ItemBalanceDetail : esItemBalanceDetail
	{
		public ItemBalanceDetail()
		{

		}
	
		public ItemBalanceDetail(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemBalanceDetailMetadata.Meta();
			}
		}
		
		
		
		override protected esItemBalanceDetailQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemBalanceDetailQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemBalanceDetailQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemBalanceDetailQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemBalanceDetailQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemBalanceDetailQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemBalanceDetailQuery : esItemBalanceDetailQuery
	{
		public ItemBalanceDetailQuery()
		{

		}		
		
		public ItemBalanceDetailQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemBalanceDetailQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemBalanceDetailMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemBalanceDetailMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemBalanceDetailMetadata.ColumnNames.LocationID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceDetailMetadata.PropertyNames.LocationID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceDetailMetadata.ColumnNames.ItemID, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceDetailMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceDetailMetadata.ColumnNames.ReferenceNo, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceDetailMetadata.PropertyNames.ReferenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceDetailMetadata.ColumnNames.TransactionCode, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceDetailMetadata.PropertyNames.TransactionCode;
			c.CharacterMaxLength = 30;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceDetailMetadata.ColumnNames.BalanceDate, 4, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemBalanceDetailMetadata.PropertyNames.BalanceDate;
			c.IsInPrimaryKey = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceDetailMetadata.ColumnNames.Balance, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemBalanceDetailMetadata.PropertyNames.Balance;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceDetailMetadata.ColumnNames.Booking, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemBalanceDetailMetadata.PropertyNames.Booking;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceDetailMetadata.ColumnNames.Price, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemBalanceDetailMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceDetailMetadata.ColumnNames.LastUpdateDateTime, 8, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemBalanceDetailMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceDetailMetadata.ColumnNames.LastUpdateByUserID, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceDetailMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceDetailMetadata.ColumnNames.PurchaseReceiveNo, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceDetailMetadata.PropertyNames.PurchaseReceiveNo;
			c.CharacterMaxLength = 20;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemBalanceDetailMetadata Meta()
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
			 public const string ReferenceNo = "ReferenceNo";
			 public const string TransactionCode = "TransactionCode";
			 public const string BalanceDate = "BalanceDate";
			 public const string Balance = "Balance";
			 public const string Booking = "Booking";
			 public const string Price = "Price";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string PurchaseReceiveNo = "PurchaseReceiveNo";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string LocationID = "LocationID";
			 public const string ItemID = "ItemID";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string TransactionCode = "TransactionCode";
			 public const string BalanceDate = "BalanceDate";
			 public const string Balance = "Balance";
			 public const string Booking = "Booking";
			 public const string Price = "Price";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string PurchaseReceiveNo = "PurchaseReceiveNo";
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
			lock (typeof(ItemBalanceDetailMetadata))
			{
				if(ItemBalanceDetailMetadata.mapDelegates == null)
				{
					ItemBalanceDetailMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemBalanceDetailMetadata.meta == null)
				{
					ItemBalanceDetailMetadata.meta = new ItemBalanceDetailMetadata();
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
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("TransactionCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BalanceDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("Balance", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Booking", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PurchaseReceiveNo", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemBalanceDetail";
				meta.Destination = "ItemBalanceDetail";
				
				meta.spInsert = "proc_ItemBalanceDetailInsert";				
				meta.spUpdate = "proc_ItemBalanceDetailUpdate";		
				meta.spDelete = "proc_ItemBalanceDetailDelete";
				meta.spLoadAll = "proc_ItemBalanceDetailLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemBalanceDetailLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemBalanceDetailMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
