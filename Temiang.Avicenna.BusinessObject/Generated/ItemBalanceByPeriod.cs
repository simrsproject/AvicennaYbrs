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
	abstract public class esItemBalanceByPeriodCollection : esEntityCollectionWAuditLog
	{
		public esItemBalanceByPeriodCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemBalanceByPeriodCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemBalanceByPeriodQuery query)
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
			this.InitQuery(query as esItemBalanceByPeriodQuery);
		}
		#endregion
		
		virtual public ItemBalanceByPeriod DetachEntity(ItemBalanceByPeriod entity)
		{
			return base.DetachEntity(entity) as ItemBalanceByPeriod;
		}
		
		virtual public ItemBalanceByPeriod AttachEntity(ItemBalanceByPeriod entity)
		{
			return base.AttachEntity(entity) as ItemBalanceByPeriod;
		}
		
		virtual public void Combine(ItemBalanceByPeriodCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemBalanceByPeriod this[int index]
		{
			get
			{
				return base[index] as ItemBalanceByPeriod;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemBalanceByPeriod);
		}
	}



	[Serializable]
	abstract public class esItemBalanceByPeriod : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemBalanceByPeriodQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemBalanceByPeriod()
		{

		}

		public esItemBalanceByPeriod(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String locationID, System.Int32 periodYear, System.Int32 periodMonth, System.String itemID)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(locationID, periodYear, periodMonth, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(locationID, periodYear, periodMonth, itemID);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String locationID, System.Int32 periodYear, System.Int32 periodMonth, System.String itemID)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(locationID, periodYear, periodMonth, itemID);
			else
				return LoadByPrimaryKeyStoredProcedure(locationID, periodYear, periodMonth, itemID);
		}

		private bool LoadByPrimaryKeyDynamic(System.String locationID, System.Int32 periodYear, System.Int32 periodMonth, System.String itemID)
		{
			esItemBalanceByPeriodQuery query = this.GetDynamicQuery();
			query.Where(query.LocationID == locationID, query.PeriodYear == periodYear, query.PeriodMonth == periodMonth, query.ItemID == itemID);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String locationID, System.Int32 periodYear, System.Int32 periodMonth, System.String itemID)
		{
			esParameters parms = new esParameters();
			parms.Add("LocationID",locationID);			parms.Add("PeriodYear",periodYear);			parms.Add("PeriodMonth",periodMonth);			parms.Add("ItemID",itemID);
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
						case "PeriodYear": this.str.PeriodYear = (string)value; break;							
						case "PeriodMonth": this.str.PeriodMonth = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "BeginningBalance": this.str.BeginningBalance = (string)value; break;							
						case "QuantityIn": this.str.QuantityIn = (string)value; break;							
						case "QuantityOut": this.str.QuantityOut = (string)value; break;							
						case "AdjustmentIn": this.str.AdjustmentIn = (string)value; break;							
						case "AdjustmentOut": this.str.AdjustmentOut = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "PeriodYear":
						
							if (value == null || value is System.Int32)
								this.PeriodYear = (System.Int32?)value;
							break;
						
						case "PeriodMonth":
						
							if (value == null || value is System.Int32)
								this.PeriodMonth = (System.Int32?)value;
							break;
						
						case "BeginningBalance":
						
							if (value == null || value is System.Decimal)
								this.BeginningBalance = (System.Decimal?)value;
							break;
						
						case "QuantityIn":
						
							if (value == null || value is System.Decimal)
								this.QuantityIn = (System.Decimal?)value;
							break;
						
						case "QuantityOut":
						
							if (value == null || value is System.Decimal)
								this.QuantityOut = (System.Decimal?)value;
							break;
						
						case "AdjustmentIn":
						
							if (value == null || value is System.Decimal)
								this.AdjustmentIn = (System.Decimal?)value;
							break;
						
						case "AdjustmentOut":
						
							if (value == null || value is System.Decimal)
								this.AdjustmentOut = (System.Decimal?)value;
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
		/// Maps to ItemBalanceByPeriod.LocationID
		/// </summary>
		virtual public System.String LocationID
		{
			get
			{
				return base.GetSystemString(ItemBalanceByPeriodMetadata.ColumnNames.LocationID);
			}
			
			set
			{
				base.SetSystemString(ItemBalanceByPeriodMetadata.ColumnNames.LocationID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceByPeriod.PeriodYear
		/// </summary>
		virtual public System.Int32? PeriodYear
		{
			get
			{
				return base.GetSystemInt32(ItemBalanceByPeriodMetadata.ColumnNames.PeriodYear);
			}
			
			set
			{
				base.SetSystemInt32(ItemBalanceByPeriodMetadata.ColumnNames.PeriodYear, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceByPeriod.PeriodMonth
		/// </summary>
		virtual public System.Int32? PeriodMonth
		{
			get
			{
				return base.GetSystemInt32(ItemBalanceByPeriodMetadata.ColumnNames.PeriodMonth);
			}
			
			set
			{
				base.SetSystemInt32(ItemBalanceByPeriodMetadata.ColumnNames.PeriodMonth, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceByPeriod.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemBalanceByPeriodMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemBalanceByPeriodMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceByPeriod.BeginningBalance
		/// </summary>
		virtual public System.Decimal? BeginningBalance
		{
			get
			{
				return base.GetSystemDecimal(ItemBalanceByPeriodMetadata.ColumnNames.BeginningBalance);
			}
			
			set
			{
				base.SetSystemDecimal(ItemBalanceByPeriodMetadata.ColumnNames.BeginningBalance, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceByPeriod.QuantityIn
		/// </summary>
		virtual public System.Decimal? QuantityIn
		{
			get
			{
				return base.GetSystemDecimal(ItemBalanceByPeriodMetadata.ColumnNames.QuantityIn);
			}
			
			set
			{
				base.SetSystemDecimal(ItemBalanceByPeriodMetadata.ColumnNames.QuantityIn, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceByPeriod.QuantityOut
		/// </summary>
		virtual public System.Decimal? QuantityOut
		{
			get
			{
				return base.GetSystemDecimal(ItemBalanceByPeriodMetadata.ColumnNames.QuantityOut);
			}
			
			set
			{
				base.SetSystemDecimal(ItemBalanceByPeriodMetadata.ColumnNames.QuantityOut, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceByPeriod.AdjustmentIn
		/// </summary>
		virtual public System.Decimal? AdjustmentIn
		{
			get
			{
				return base.GetSystemDecimal(ItemBalanceByPeriodMetadata.ColumnNames.AdjustmentIn);
			}
			
			set
			{
				base.SetSystemDecimal(ItemBalanceByPeriodMetadata.ColumnNames.AdjustmentIn, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceByPeriod.AdjustmentOut
		/// </summary>
		virtual public System.Decimal? AdjustmentOut
		{
			get
			{
				return base.GetSystemDecimal(ItemBalanceByPeriodMetadata.ColumnNames.AdjustmentOut);
			}
			
			set
			{
				base.SetSystemDecimal(ItemBalanceByPeriodMetadata.ColumnNames.AdjustmentOut, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceByPeriod.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemBalanceByPeriodMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemBalanceByPeriodMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemBalanceByPeriod.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemBalanceByPeriodMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemBalanceByPeriodMetadata.ColumnNames.LastUpdateByUserID, value);
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
			public esStrings(esItemBalanceByPeriod entity)
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
				
			public System.String PeriodYear
			{
				get
				{
					System.Int32? data = entity.PeriodYear;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodYear = null;
					else entity.PeriodYear = Convert.ToInt32(value);
				}
			}
				
			public System.String PeriodMonth
			{
				get
				{
					System.Int32? data = entity.PeriodMonth;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PeriodMonth = null;
					else entity.PeriodMonth = Convert.ToInt32(value);
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
				
			public System.String BeginningBalance
			{
				get
				{
					System.Decimal? data = entity.BeginningBalance;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BeginningBalance = null;
					else entity.BeginningBalance = Convert.ToDecimal(value);
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
				
			public System.String AdjustmentIn
			{
				get
				{
					System.Decimal? data = entity.AdjustmentIn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdjustmentIn = null;
					else entity.AdjustmentIn = Convert.ToDecimal(value);
				}
			}
				
			public System.String AdjustmentOut
			{
				get
				{
					System.Decimal? data = entity.AdjustmentOut;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AdjustmentOut = null;
					else entity.AdjustmentOut = Convert.ToDecimal(value);
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
			

			private esItemBalanceByPeriod entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemBalanceByPeriodQuery query)
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
				throw new Exception("esItemBalanceByPeriod can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemBalanceByPeriod : esItemBalanceByPeriod
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
	abstract public class esItemBalanceByPeriodQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemBalanceByPeriodMetadata.Meta();
			}
		}	
		

		public esQueryItem LocationID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceByPeriodMetadata.ColumnNames.LocationID, esSystemType.String);
			}
		} 
		
		public esQueryItem PeriodYear
		{
			get
			{
				return new esQueryItem(this, ItemBalanceByPeriodMetadata.ColumnNames.PeriodYear, esSystemType.Int32);
			}
		} 
		
		public esQueryItem PeriodMonth
		{
			get
			{
				return new esQueryItem(this, ItemBalanceByPeriodMetadata.ColumnNames.PeriodMonth, esSystemType.Int32);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceByPeriodMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem BeginningBalance
		{
			get
			{
				return new esQueryItem(this, ItemBalanceByPeriodMetadata.ColumnNames.BeginningBalance, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem QuantityIn
		{
			get
			{
				return new esQueryItem(this, ItemBalanceByPeriodMetadata.ColumnNames.QuantityIn, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem QuantityOut
		{
			get
			{
				return new esQueryItem(this, ItemBalanceByPeriodMetadata.ColumnNames.QuantityOut, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem AdjustmentIn
		{
			get
			{
				return new esQueryItem(this, ItemBalanceByPeriodMetadata.ColumnNames.AdjustmentIn, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem AdjustmentOut
		{
			get
			{
				return new esQueryItem(this, ItemBalanceByPeriodMetadata.ColumnNames.AdjustmentOut, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemBalanceByPeriodMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemBalanceByPeriodMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemBalanceByPeriodCollection")]
	public partial class ItemBalanceByPeriodCollection : esItemBalanceByPeriodCollection, IEnumerable<ItemBalanceByPeriod>
	{
		public ItemBalanceByPeriodCollection()
		{

		}
		
		public static implicit operator List<ItemBalanceByPeriod>(ItemBalanceByPeriodCollection coll)
		{
			List<ItemBalanceByPeriod> list = new List<ItemBalanceByPeriod>();
			
			foreach (ItemBalanceByPeriod emp in coll)
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
				return  ItemBalanceByPeriodMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemBalanceByPeriodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemBalanceByPeriod(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemBalanceByPeriod();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemBalanceByPeriodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemBalanceByPeriodQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemBalanceByPeriodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemBalanceByPeriod AddNew()
		{
			ItemBalanceByPeriod entity = base.AddNewEntity() as ItemBalanceByPeriod;
			
			return entity;
		}

		public ItemBalanceByPeriod FindByPrimaryKey(System.String locationID, System.Int32 periodYear, System.Int32 periodMonth, System.String itemID)
		{
			return base.FindByPrimaryKey(locationID, periodYear, periodMonth, itemID) as ItemBalanceByPeriod;
		}


		#region IEnumerable<ItemBalanceByPeriod> Members

		IEnumerator<ItemBalanceByPeriod> IEnumerable<ItemBalanceByPeriod>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemBalanceByPeriod;
			}
		}

		#endregion
		
		private ItemBalanceByPeriodQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemBalanceByPeriod' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemBalanceByPeriod ({LocationID},{PeriodYear},{PeriodMonth},{ItemID})")]
	[Serializable]
	public partial class ItemBalanceByPeriod : esItemBalanceByPeriod
	{
		public ItemBalanceByPeriod()
		{

		}
	
		public ItemBalanceByPeriod(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemBalanceByPeriodMetadata.Meta();
			}
		}
		
		
		
		override protected esItemBalanceByPeriodQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemBalanceByPeriodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemBalanceByPeriodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemBalanceByPeriodQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemBalanceByPeriodQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemBalanceByPeriodQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemBalanceByPeriodQuery : esItemBalanceByPeriodQuery
	{
		public ItemBalanceByPeriodQuery()
		{

		}		
		
		public ItemBalanceByPeriodQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemBalanceByPeriodQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemBalanceByPeriodMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemBalanceByPeriodMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemBalanceByPeriodMetadata.ColumnNames.LocationID, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceByPeriodMetadata.PropertyNames.LocationID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceByPeriodMetadata.ColumnNames.PeriodYear, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ItemBalanceByPeriodMetadata.PropertyNames.PeriodYear;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceByPeriodMetadata.ColumnNames.PeriodMonth, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ItemBalanceByPeriodMetadata.PropertyNames.PeriodMonth;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceByPeriodMetadata.ColumnNames.ItemID, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceByPeriodMetadata.PropertyNames.ItemID;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceByPeriodMetadata.ColumnNames.BeginningBalance, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemBalanceByPeriodMetadata.PropertyNames.BeginningBalance;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceByPeriodMetadata.ColumnNames.QuantityIn, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemBalanceByPeriodMetadata.PropertyNames.QuantityIn;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceByPeriodMetadata.ColumnNames.QuantityOut, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemBalanceByPeriodMetadata.PropertyNames.QuantityOut;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceByPeriodMetadata.ColumnNames.AdjustmentIn, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemBalanceByPeriodMetadata.PropertyNames.AdjustmentIn;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceByPeriodMetadata.ColumnNames.AdjustmentOut, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemBalanceByPeriodMetadata.PropertyNames.AdjustmentOut;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceByPeriodMetadata.ColumnNames.LastUpdateDateTime, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemBalanceByPeriodMetadata.PropertyNames.LastUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemBalanceByPeriodMetadata.ColumnNames.LastUpdateByUserID, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemBalanceByPeriodMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemBalanceByPeriodMetadata Meta()
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
			 public const string PeriodYear = "PeriodYear";
			 public const string PeriodMonth = "PeriodMonth";
			 public const string ItemID = "ItemID";
			 public const string BeginningBalance = "BeginningBalance";
			 public const string QuantityIn = "QuantityIn";
			 public const string QuantityOut = "QuantityOut";
			 public const string AdjustmentIn = "AdjustmentIn";
			 public const string AdjustmentOut = "AdjustmentOut";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string LocationID = "LocationID";
			 public const string PeriodYear = "PeriodYear";
			 public const string PeriodMonth = "PeriodMonth";
			 public const string ItemID = "ItemID";
			 public const string BeginningBalance = "BeginningBalance";
			 public const string QuantityIn = "QuantityIn";
			 public const string QuantityOut = "QuantityOut";
			 public const string AdjustmentIn = "AdjustmentIn";
			 public const string AdjustmentOut = "AdjustmentOut";
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
			lock (typeof(ItemBalanceByPeriodMetadata))
			{
				if(ItemBalanceByPeriodMetadata.mapDelegates == null)
				{
					ItemBalanceByPeriodMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemBalanceByPeriodMetadata.meta == null)
				{
					ItemBalanceByPeriodMetadata.meta = new ItemBalanceByPeriodMetadata();
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
				meta.AddTypeMap("PeriodYear", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PeriodMonth", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("BeginningBalance", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QuantityIn", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QuantityOut", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AdjustmentIn", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("AdjustmentOut", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemBalanceByPeriod";
				meta.Destination = "ItemBalanceByPeriod";
				
				meta.spInsert = "proc_ItemBalanceByPeriodInsert";				
				meta.spUpdate = "proc_ItemBalanceByPeriodUpdate";		
				meta.spDelete = "proc_ItemBalanceByPeriodDelete";
				meta.spLoadAll = "proc_ItemBalanceByPeriodLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemBalanceByPeriodLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemBalanceByPeriodMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
