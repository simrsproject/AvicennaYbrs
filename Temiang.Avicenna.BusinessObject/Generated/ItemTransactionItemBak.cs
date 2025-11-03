/*
===============================================================================
                 Persistence Layer and Business Objects
===============================================================================
Version         : 2009.2.1214.0
Driver          : SQL
Date Generated  : 4/6/2015 2:03:21 PM
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
	abstract public class esItemTransactionItemBakCollection : esEntityCollectionWAuditLog
	{
		public esItemTransactionItemBakCollection()
		{

		}

		protected override string GetCollectionName()
		{
			return "ItemTransactionItemBakCollection";
		}

		#region Query Logic
		protected void InitQuery(esItemTransactionItemBakQuery query)
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
			this.InitQuery(query as esItemTransactionItemBakQuery);
		}
		#endregion
		
		virtual public ItemTransactionItemBak DetachEntity(ItemTransactionItemBak entity)
		{
			return base.DetachEntity(entity) as ItemTransactionItemBak;
		}
		
		virtual public ItemTransactionItemBak AttachEntity(ItemTransactionItemBak entity)
		{
			return base.AttachEntity(entity) as ItemTransactionItemBak;
		}
		
		virtual public void Combine(ItemTransactionItemBakCollection collection)
		{
			base.Combine(collection);
		}
		
		new public ItemTransactionItemBak this[int index]
		{
			get
			{
				return base[index] as ItemTransactionItemBak;
			}
		}

		public override Type GetEntityType()
		{
			return typeof(ItemTransactionItemBak);
		}
	}



	[Serializable]
	abstract public class esItemTransactionItemBak : esEntityWAuditLog
	{
		/// <summary>
		/// Used internally by the entity's DynamicQuery mechanism.
		/// </summary>
		virtual protected esItemTransactionItemBakQuery GetDynamicQuery()
		{
			return null;
		}

		public esItemTransactionItemBak()
		{

		}

		public esItemTransactionItemBak(DataRow row)
			: base(row)
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String transactionNo, System.String sequenceNo)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String transactionNo, System.String sequenceNo)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(transactionNo, sequenceNo);
			else
				return LoadByPrimaryKeyStoredProcedure(transactionNo, sequenceNo);
		}

		private bool LoadByPrimaryKeyDynamic(System.String transactionNo, System.String sequenceNo)
		{
			esItemTransactionItemBakQuery query = this.GetDynamicQuery();
			query.Where(query.TransactionNo == transactionNo, query.SequenceNo == sequenceNo);
			return query.Load();
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String transactionNo, System.String sequenceNo)
		{
			esParameters parms = new esParameters();
			parms.Add("TransactionNo",transactionNo);			parms.Add("SequenceNo",sequenceNo);
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
						case "TransactionNo": this.str.TransactionNo = (string)value; break;							
						case "SequenceNo": this.str.SequenceNo = (string)value; break;							
						case "ItemID": this.str.ItemID = (string)value; break;							
						case "ReferenceNo": this.str.ReferenceNo = (string)value; break;							
						case "ReferenceSequenceNo": this.str.ReferenceSequenceNo = (string)value; break;							
						case "Quantity": this.str.Quantity = (string)value; break;							
						case "SRItemUnit": this.str.SRItemUnit = (string)value; break;							
						case "ConversionFactor": this.str.ConversionFactor = (string)value; break;							
						case "QuantityFinishInBaseUnit": this.str.QuantityFinishInBaseUnit = (string)value; break;							
						case "PageNo": this.str.PageNo = (string)value; break;							
						case "CostPrice": this.str.CostPrice = (string)value; break;							
						case "Price": this.str.Price = (string)value; break;							
						case "PriceInCurrency": this.str.PriceInCurrency = (string)value; break;							
						case "Discount1Percentage": this.str.Discount1Percentage = (string)value; break;							
						case "Discount2Percentage": this.str.Discount2Percentage = (string)value; break;							
						case "BatchNumber": this.str.BatchNumber = (string)value; break;							
						case "ExpiredDate": this.str.ExpiredDate = (string)value; break;							
						case "IsPackage": this.str.IsPackage = (string)value; break;							
						case "IsBonusItem": this.str.IsBonusItem = (string)value; break;							
						case "IsClosed": this.str.IsClosed = (string)value; break;							
						case "Description": this.str.Description = (string)value; break;							
						case "LastUpdateDateTime": this.str.LastUpdateDateTime = (string)value; break;							
						case "LastUpdateByUserID": this.str.LastUpdateByUserID = (string)value; break;							
						case "RequestQty": this.str.RequestQty = (string)value; break;							
						case "Discount": this.str.Discount = (string)value; break;							
						case "DiscountInCurrency": this.str.DiscountInCurrency = (string)value; break;							
						case "IsDiscountInPercent": this.str.IsDiscountInPercent = (string)value; break;							
						case "IsInvoiceUpdate": this.str.IsInvoiceUpdate = (string)value; break;							
						case "PriorPrice": this.str.PriorPrice = (string)value; break;							
						case "PriorPriceInCurrency": this.str.PriorPriceInCurrency = (string)value; break;							
						case "PriorDiscount1Percentage": this.str.PriorDiscount1Percentage = (string)value; break;							
						case "PriorDiscount2Percentage": this.str.PriorDiscount2Percentage = (string)value; break;							
						case "PriorDiscount": this.str.PriorDiscount = (string)value; break;							
						case "PriorDiscountInCurrency": this.str.PriorDiscountInCurrency = (string)value; break;							
						case "LastInvoiceUpdateDateTime": this.str.LastInvoiceUpdateDateTime = (string)value; break;							
						case "LastInvoiceUpdateByUserID": this.str.LastInvoiceUpdateByUserID = (string)value; break;							
						case "HistoryPrice": this.str.HistoryPrice = (string)value; break;							
						case "HistoryPriceInCurrency": this.str.HistoryPriceInCurrency = (string)value; break;							
						case "HistoryDiscount1Percentage": this.str.HistoryDiscount1Percentage = (string)value; break;							
						case "HistoryDiscount2Percentage": this.str.HistoryDiscount2Percentage = (string)value; break;							
						case "HistoryDiscount": this.str.HistoryDiscount = (string)value; break;							
						case "HistoryDiscountInCurrency": this.str.HistoryDiscountInCurrency = (string)value; break;							
						case "Specification": this.str.Specification = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Quantity":
						
							if (value == null || value is System.Decimal)
								this.Quantity = (System.Decimal?)value;
							break;
						
						case "ConversionFactor":
						
							if (value == null || value is System.Decimal)
								this.ConversionFactor = (System.Decimal?)value;
							break;
						
						case "QuantityFinishInBaseUnit":
						
							if (value == null || value is System.Decimal)
								this.QuantityFinishInBaseUnit = (System.Decimal?)value;
							break;
						
						case "PageNo":
						
							if (value == null || value is System.Int32)
								this.PageNo = (System.Int32?)value;
							break;
						
						case "CostPrice":
						
							if (value == null || value is System.Decimal)
								this.CostPrice = (System.Decimal?)value;
							break;
						
						case "Price":
						
							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
							break;
						
						case "PriceInCurrency":
						
							if (value == null || value is System.Decimal)
								this.PriceInCurrency = (System.Decimal?)value;
							break;
						
						case "Discount1Percentage":
						
							if (value == null || value is System.Decimal)
								this.Discount1Percentage = (System.Decimal?)value;
							break;
						
						case "Discount2Percentage":
						
							if (value == null || value is System.Decimal)
								this.Discount2Percentage = (System.Decimal?)value;
							break;
						
						case "ExpiredDate":
						
							if (value == null || value is System.DateTime)
								this.ExpiredDate = (System.DateTime?)value;
							break;
						
						case "IsPackage":
						
							if (value == null || value is System.Boolean)
								this.IsPackage = (System.Boolean?)value;
							break;
						
						case "IsBonusItem":
						
							if (value == null || value is System.Boolean)
								this.IsBonusItem = (System.Boolean?)value;
							break;
						
						case "IsClosed":
						
							if (value == null || value is System.Boolean)
								this.IsClosed = (System.Boolean?)value;
							break;
						
						case "LastUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "RequestQty":
						
							if (value == null || value is System.Decimal)
								this.RequestQty = (System.Decimal?)value;
							break;
						
						case "Discount":
						
							if (value == null || value is System.Decimal)
								this.Discount = (System.Decimal?)value;
							break;
						
						case "DiscountInCurrency":
						
							if (value == null || value is System.Decimal)
								this.DiscountInCurrency = (System.Decimal?)value;
							break;
						
						case "IsDiscountInPercent":
						
							if (value == null || value is System.Boolean)
								this.IsDiscountInPercent = (System.Boolean?)value;
							break;
						
						case "IsInvoiceUpdate":
						
							if (value == null || value is System.Boolean)
								this.IsInvoiceUpdate = (System.Boolean?)value;
							break;
						
						case "PriorPrice":
						
							if (value == null || value is System.Decimal)
								this.PriorPrice = (System.Decimal?)value;
							break;
						
						case "PriorPriceInCurrency":
						
							if (value == null || value is System.Decimal)
								this.PriorPriceInCurrency = (System.Decimal?)value;
							break;
						
						case "PriorDiscount1Percentage":
						
							if (value == null || value is System.Decimal)
								this.PriorDiscount1Percentage = (System.Decimal?)value;
							break;
						
						case "PriorDiscount2Percentage":
						
							if (value == null || value is System.Decimal)
								this.PriorDiscount2Percentage = (System.Decimal?)value;
							break;
						
						case "PriorDiscount":
						
							if (value == null || value is System.Decimal)
								this.PriorDiscount = (System.Decimal?)value;
							break;
						
						case "PriorDiscountInCurrency":
						
							if (value == null || value is System.Decimal)
								this.PriorDiscountInCurrency = (System.Decimal?)value;
							break;
						
						case "LastInvoiceUpdateDateTime":
						
							if (value == null || value is System.DateTime)
								this.LastInvoiceUpdateDateTime = (System.DateTime?)value;
							break;
						
						case "HistoryPrice":
						
							if (value == null || value is System.Decimal)
								this.HistoryPrice = (System.Decimal?)value;
							break;
						
						case "HistoryPriceInCurrency":
						
							if (value == null || value is System.Decimal)
								this.HistoryPriceInCurrency = (System.Decimal?)value;
							break;
						
						case "HistoryDiscount1Percentage":
						
							if (value == null || value is System.Decimal)
								this.HistoryDiscount1Percentage = (System.Decimal?)value;
							break;
						
						case "HistoryDiscount2Percentage":
						
							if (value == null || value is System.Decimal)
								this.HistoryDiscount2Percentage = (System.Decimal?)value;
							break;
						
						case "HistoryDiscount":
						
							if (value == null || value is System.Decimal)
								this.HistoryDiscount = (System.Decimal?)value;
							break;
						
						case "HistoryDiscountInCurrency":
						
							if (value == null || value is System.Decimal)
								this.HistoryDiscountInCurrency = (System.Decimal?)value;
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
		/// Maps to ItemTransactionItemBak.TransactionNo
		/// </summary>
		virtual public System.String TransactionNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemBakMetadata.ColumnNames.TransactionNo);
			}
			
			set
			{
				base.SetSystemString(ItemTransactionItemBakMetadata.ColumnNames.TransactionNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.SequenceNo
		/// </summary>
		virtual public System.String SequenceNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemBakMetadata.ColumnNames.SequenceNo);
			}
			
			set
			{
				base.SetSystemString(ItemTransactionItemBakMetadata.ColumnNames.SequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.ItemID
		/// </summary>
		virtual public System.String ItemID
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemBakMetadata.ColumnNames.ItemID);
			}
			
			set
			{
				base.SetSystemString(ItemTransactionItemBakMetadata.ColumnNames.ItemID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.ReferenceNo
		/// </summary>
		virtual public System.String ReferenceNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemBakMetadata.ColumnNames.ReferenceNo);
			}
			
			set
			{
				base.SetSystemString(ItemTransactionItemBakMetadata.ColumnNames.ReferenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.ReferenceSequenceNo
		/// </summary>
		virtual public System.String ReferenceSequenceNo
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemBakMetadata.ColumnNames.ReferenceSequenceNo);
			}
			
			set
			{
				base.SetSystemString(ItemTransactionItemBakMetadata.ColumnNames.ReferenceSequenceNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.Quantity
		/// </summary>
		virtual public System.Decimal? Quantity
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.Quantity);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.Quantity, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.SRItemUnit
		/// </summary>
		virtual public System.String SRItemUnit
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemBakMetadata.ColumnNames.SRItemUnit);
			}
			
			set
			{
				base.SetSystemString(ItemTransactionItemBakMetadata.ColumnNames.SRItemUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.ConversionFactor
		/// </summary>
		virtual public System.Decimal? ConversionFactor
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.ConversionFactor);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.ConversionFactor, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.QuantityFinishInBaseUnit
		/// </summary>
		virtual public System.Decimal? QuantityFinishInBaseUnit
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.QuantityFinishInBaseUnit);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.QuantityFinishInBaseUnit, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.PageNo
		/// </summary>
		virtual public System.Int32? PageNo
		{
			get
			{
				return base.GetSystemInt32(ItemTransactionItemBakMetadata.ColumnNames.PageNo);
			}
			
			set
			{
				base.SetSystemInt32(ItemTransactionItemBakMetadata.ColumnNames.PageNo, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.CostPrice
		/// </summary>
		virtual public System.Decimal? CostPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.CostPrice);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.CostPrice, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.Price
		/// </summary>
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.Price);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.Price, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.PriceInCurrency
		/// </summary>
		virtual public System.Decimal? PriceInCurrency
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.PriceInCurrency);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.PriceInCurrency, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.Discount1Percentage
		/// </summary>
		virtual public System.Decimal? Discount1Percentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.Discount1Percentage);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.Discount1Percentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.Discount2Percentage
		/// </summary>
		virtual public System.Decimal? Discount2Percentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.Discount2Percentage);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.Discount2Percentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.BatchNumber
		/// </summary>
		virtual public System.String BatchNumber
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemBakMetadata.ColumnNames.BatchNumber);
			}
			
			set
			{
				base.SetSystemString(ItemTransactionItemBakMetadata.ColumnNames.BatchNumber, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.ExpiredDate
		/// </summary>
		virtual public System.DateTime? ExpiredDate
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionItemBakMetadata.ColumnNames.ExpiredDate);
			}
			
			set
			{
				base.SetSystemDateTime(ItemTransactionItemBakMetadata.ColumnNames.ExpiredDate, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.IsPackage
		/// </summary>
		virtual public System.Boolean? IsPackage
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemBakMetadata.ColumnNames.IsPackage);
			}
			
			set
			{
				base.SetSystemBoolean(ItemTransactionItemBakMetadata.ColumnNames.IsPackage, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.IsBonusItem
		/// </summary>
		virtual public System.Boolean? IsBonusItem
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemBakMetadata.ColumnNames.IsBonusItem);
			}
			
			set
			{
				base.SetSystemBoolean(ItemTransactionItemBakMetadata.ColumnNames.IsBonusItem, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.IsClosed
		/// </summary>
		virtual public System.Boolean? IsClosed
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemBakMetadata.ColumnNames.IsClosed);
			}
			
			set
			{
				base.SetSystemBoolean(ItemTransactionItemBakMetadata.ColumnNames.IsClosed, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.Description
		/// </summary>
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemBakMetadata.ColumnNames.Description);
			}
			
			set
			{
				base.SetSystemString(ItemTransactionItemBakMetadata.ColumnNames.Description, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.LastUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionItemBakMetadata.ColumnNames.LastUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemTransactionItemBakMetadata.ColumnNames.LastUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.LastUpdateByUserID
		/// </summary>
		virtual public System.String LastUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemBakMetadata.ColumnNames.LastUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemTransactionItemBakMetadata.ColumnNames.LastUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.RequestQty
		/// </summary>
		virtual public System.Decimal? RequestQty
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.RequestQty);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.RequestQty, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.Discount
		/// </summary>
		virtual public System.Decimal? Discount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.Discount);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.Discount, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.DiscountInCurrency
		/// </summary>
		virtual public System.Decimal? DiscountInCurrency
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.DiscountInCurrency);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.DiscountInCurrency, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.IsDiscountInPercent
		/// </summary>
		virtual public System.Boolean? IsDiscountInPercent
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemBakMetadata.ColumnNames.IsDiscountInPercent);
			}
			
			set
			{
				base.SetSystemBoolean(ItemTransactionItemBakMetadata.ColumnNames.IsDiscountInPercent, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.IsInvoiceUpdate
		/// </summary>
		virtual public System.Boolean? IsInvoiceUpdate
		{
			get
			{
				return base.GetSystemBoolean(ItemTransactionItemBakMetadata.ColumnNames.IsInvoiceUpdate);
			}
			
			set
			{
				base.SetSystemBoolean(ItemTransactionItemBakMetadata.ColumnNames.IsInvoiceUpdate, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.PriorPrice
		/// </summary>
		virtual public System.Decimal? PriorPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.PriorPrice);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.PriorPrice, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.PriorPriceInCurrency
		/// </summary>
		virtual public System.Decimal? PriorPriceInCurrency
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.PriorPriceInCurrency);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.PriorPriceInCurrency, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.PriorDiscount1Percentage
		/// </summary>
		virtual public System.Decimal? PriorDiscount1Percentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.PriorDiscount1Percentage);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.PriorDiscount1Percentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.PriorDiscount2Percentage
		/// </summary>
		virtual public System.Decimal? PriorDiscount2Percentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.PriorDiscount2Percentage);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.PriorDiscount2Percentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.PriorDiscount
		/// </summary>
		virtual public System.Decimal? PriorDiscount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.PriorDiscount);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.PriorDiscount, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.PriorDiscountInCurrency
		/// </summary>
		virtual public System.Decimal? PriorDiscountInCurrency
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.PriorDiscountInCurrency);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.PriorDiscountInCurrency, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.LastInvoiceUpdateDateTime
		/// </summary>
		virtual public System.DateTime? LastInvoiceUpdateDateTime
		{
			get
			{
				return base.GetSystemDateTime(ItemTransactionItemBakMetadata.ColumnNames.LastInvoiceUpdateDateTime);
			}
			
			set
			{
				base.SetSystemDateTime(ItemTransactionItemBakMetadata.ColumnNames.LastInvoiceUpdateDateTime, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.LastInvoiceUpdateByUserID
		/// </summary>
		virtual public System.String LastInvoiceUpdateByUserID
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemBakMetadata.ColumnNames.LastInvoiceUpdateByUserID);
			}
			
			set
			{
				base.SetSystemString(ItemTransactionItemBakMetadata.ColumnNames.LastInvoiceUpdateByUserID, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.HistoryPrice
		/// </summary>
		virtual public System.Decimal? HistoryPrice
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.HistoryPrice);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.HistoryPrice, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.HistoryPriceInCurrency
		/// </summary>
		virtual public System.Decimal? HistoryPriceInCurrency
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.HistoryPriceInCurrency);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.HistoryPriceInCurrency, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.HistoryDiscount1Percentage
		/// </summary>
		virtual public System.Decimal? HistoryDiscount1Percentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscount1Percentage);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscount1Percentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.HistoryDiscount2Percentage
		/// </summary>
		virtual public System.Decimal? HistoryDiscount2Percentage
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscount2Percentage);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscount2Percentage, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.HistoryDiscount
		/// </summary>
		virtual public System.Decimal? HistoryDiscount
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscount);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscount, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.HistoryDiscountInCurrency
		/// </summary>
		virtual public System.Decimal? HistoryDiscountInCurrency
		{
			get
			{
				return base.GetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscountInCurrency);
			}
			
			set
			{
				base.SetSystemDecimal(ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscountInCurrency, value);
			}
		}
		
		/// <summary>
		/// Maps to ItemTransactionItemBak.Specification
		/// </summary>
		virtual public System.String Specification
		{
			get
			{
				return base.GetSystemString(ItemTransactionItemBakMetadata.ColumnNames.Specification);
			}
			
			set
			{
				base.SetSystemString(ItemTransactionItemBakMetadata.ColumnNames.Specification, value);
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
			public esStrings(esItemTransactionItemBak entity)
			{
				this.entity = entity;
			}
			
	
			public System.String TransactionNo
			{
				get
				{
					System.String data = entity.TransactionNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TransactionNo = null;
					else entity.TransactionNo = Convert.ToString(value);
				}
			}
				
			public System.String SequenceNo
			{
				get
				{
					System.String data = entity.SequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SequenceNo = null;
					else entity.SequenceNo = Convert.ToString(value);
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
				
			public System.String ReferenceSequenceNo
			{
				get
				{
					System.String data = entity.ReferenceSequenceNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ReferenceSequenceNo = null;
					else entity.ReferenceSequenceNo = Convert.ToString(value);
				}
			}
				
			public System.String Quantity
			{
				get
				{
					System.Decimal? data = entity.Quantity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Quantity = null;
					else entity.Quantity = Convert.ToDecimal(value);
				}
			}
				
			public System.String SRItemUnit
			{
				get
				{
					System.String data = entity.SRItemUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SRItemUnit = null;
					else entity.SRItemUnit = Convert.ToString(value);
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
				
			public System.String QuantityFinishInBaseUnit
			{
				get
				{
					System.Decimal? data = entity.QuantityFinishInBaseUnit;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuantityFinishInBaseUnit = null;
					else entity.QuantityFinishInBaseUnit = Convert.ToDecimal(value);
				}
			}
				
			public System.String PageNo
			{
				get
				{
					System.Int32? data = entity.PageNo;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PageNo = null;
					else entity.PageNo = Convert.ToInt32(value);
				}
			}
				
			public System.String CostPrice
			{
				get
				{
					System.Decimal? data = entity.CostPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CostPrice = null;
					else entity.CostPrice = Convert.ToDecimal(value);
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
				
			public System.String PriceInCurrency
			{
				get
				{
					System.Decimal? data = entity.PriceInCurrency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceInCurrency = null;
					else entity.PriceInCurrency = Convert.ToDecimal(value);
				}
			}
				
			public System.String Discount1Percentage
			{
				get
				{
					System.Decimal? data = entity.Discount1Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Discount1Percentage = null;
					else entity.Discount1Percentage = Convert.ToDecimal(value);
				}
			}
				
			public System.String Discount2Percentage
			{
				get
				{
					System.Decimal? data = entity.Discount2Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Discount2Percentage = null;
					else entity.Discount2Percentage = Convert.ToDecimal(value);
				}
			}
				
			public System.String BatchNumber
			{
				get
				{
					System.String data = entity.BatchNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BatchNumber = null;
					else entity.BatchNumber = Convert.ToString(value);
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
				
			public System.String IsPackage
			{
				get
				{
					System.Boolean? data = entity.IsPackage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPackage = null;
					else entity.IsPackage = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsBonusItem
			{
				get
				{
					System.Boolean? data = entity.IsBonusItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsBonusItem = null;
					else entity.IsBonusItem = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsClosed
			{
				get
				{
					System.Boolean? data = entity.IsClosed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsClosed = null;
					else entity.IsClosed = Convert.ToBoolean(value);
				}
			}
				
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
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
				
			public System.String RequestQty
			{
				get
				{
					System.Decimal? data = entity.RequestQty;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestQty = null;
					else entity.RequestQty = Convert.ToDecimal(value);
				}
			}
				
			public System.String Discount
			{
				get
				{
					System.Decimal? data = entity.Discount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Discount = null;
					else entity.Discount = Convert.ToDecimal(value);
				}
			}
				
			public System.String DiscountInCurrency
			{
				get
				{
					System.Decimal? data = entity.DiscountInCurrency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscountInCurrency = null;
					else entity.DiscountInCurrency = Convert.ToDecimal(value);
				}
			}
				
			public System.String IsDiscountInPercent
			{
				get
				{
					System.Boolean? data = entity.IsDiscountInPercent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDiscountInPercent = null;
					else entity.IsDiscountInPercent = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsInvoiceUpdate
			{
				get
				{
					System.Boolean? data = entity.IsInvoiceUpdate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsInvoiceUpdate = null;
					else entity.IsInvoiceUpdate = Convert.ToBoolean(value);
				}
			}
				
			public System.String PriorPrice
			{
				get
				{
					System.Decimal? data = entity.PriorPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriorPrice = null;
					else entity.PriorPrice = Convert.ToDecimal(value);
				}
			}
				
			public System.String PriorPriceInCurrency
			{
				get
				{
					System.Decimal? data = entity.PriorPriceInCurrency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriorPriceInCurrency = null;
					else entity.PriorPriceInCurrency = Convert.ToDecimal(value);
				}
			}
				
			public System.String PriorDiscount1Percentage
			{
				get
				{
					System.Decimal? data = entity.PriorDiscount1Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriorDiscount1Percentage = null;
					else entity.PriorDiscount1Percentage = Convert.ToDecimal(value);
				}
			}
				
			public System.String PriorDiscount2Percentage
			{
				get
				{
					System.Decimal? data = entity.PriorDiscount2Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriorDiscount2Percentage = null;
					else entity.PriorDiscount2Percentage = Convert.ToDecimal(value);
				}
			}
				
			public System.String PriorDiscount
			{
				get
				{
					System.Decimal? data = entity.PriorDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriorDiscount = null;
					else entity.PriorDiscount = Convert.ToDecimal(value);
				}
			}
				
			public System.String PriorDiscountInCurrency
			{
				get
				{
					System.Decimal? data = entity.PriorDiscountInCurrency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriorDiscountInCurrency = null;
					else entity.PriorDiscountInCurrency = Convert.ToDecimal(value);
				}
			}
				
			public System.String LastInvoiceUpdateDateTime
			{
				get
				{
					System.DateTime? data = entity.LastInvoiceUpdateDateTime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastInvoiceUpdateDateTime = null;
					else entity.LastInvoiceUpdateDateTime = Convert.ToDateTime(value);
				}
			}
				
			public System.String LastInvoiceUpdateByUserID
			{
				get
				{
					System.String data = entity.LastInvoiceUpdateByUserID;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.LastInvoiceUpdateByUserID = null;
					else entity.LastInvoiceUpdateByUserID = Convert.ToString(value);
				}
			}
				
			public System.String HistoryPrice
			{
				get
				{
					System.Decimal? data = entity.HistoryPrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HistoryPrice = null;
					else entity.HistoryPrice = Convert.ToDecimal(value);
				}
			}
				
			public System.String HistoryPriceInCurrency
			{
				get
				{
					System.Decimal? data = entity.HistoryPriceInCurrency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HistoryPriceInCurrency = null;
					else entity.HistoryPriceInCurrency = Convert.ToDecimal(value);
				}
			}
				
			public System.String HistoryDiscount1Percentage
			{
				get
				{
					System.Decimal? data = entity.HistoryDiscount1Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HistoryDiscount1Percentage = null;
					else entity.HistoryDiscount1Percentage = Convert.ToDecimal(value);
				}
			}
				
			public System.String HistoryDiscount2Percentage
			{
				get
				{
					System.Decimal? data = entity.HistoryDiscount2Percentage;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HistoryDiscount2Percentage = null;
					else entity.HistoryDiscount2Percentage = Convert.ToDecimal(value);
				}
			}
				
			public System.String HistoryDiscount
			{
				get
				{
					System.Decimal? data = entity.HistoryDiscount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HistoryDiscount = null;
					else entity.HistoryDiscount = Convert.ToDecimal(value);
				}
			}
				
			public System.String HistoryDiscountInCurrency
			{
				get
				{
					System.Decimal? data = entity.HistoryDiscountInCurrency;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.HistoryDiscountInCurrency = null;
					else entity.HistoryDiscountInCurrency = Convert.ToDecimal(value);
				}
			}
				
			public System.String Specification
			{
				get
				{
					System.String data = entity.Specification;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Specification = null;
					else entity.Specification = Convert.ToString(value);
				}
			}
			

			private esItemTransactionItemBak entity;
		}
		#endregion

		#region Query Logic
		protected void InitQuery(esItemTransactionItemBakQuery query)
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
				throw new Exception("esItemTransactionItemBak can only hold one record of data");
			}

			return dataFound;
		}
		#endregion
		
		[NonSerialized]
		private esStrings esstrings;
	}


	
	public partial class ItemTransactionItemBak : esItemTransactionItemBak
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
	abstract public class esItemTransactionItemBakQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ItemTransactionItemBakMetadata.Meta();
			}
		}	
		

		public esQueryItem TransactionNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.TransactionNo, esSystemType.String);
			}
		} 
		
		public esQueryItem SequenceNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.SequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ItemID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.ItemID, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.ReferenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem ReferenceSequenceNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.ReferenceSequenceNo, esSystemType.String);
			}
		} 
		
		public esQueryItem Quantity
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.Quantity, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem SRItemUnit
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.SRItemUnit, esSystemType.String);
			}
		} 
		
		public esQueryItem ConversionFactor
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.ConversionFactor, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem QuantityFinishInBaseUnit
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.QuantityFinishInBaseUnit, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem PageNo
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.PageNo, esSystemType.Int32);
			}
		} 
		
		public esQueryItem CostPrice
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.CostPrice, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Price
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.Price, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem PriceInCurrency
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.PriceInCurrency, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Discount1Percentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.Discount1Percentage, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Discount2Percentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.Discount2Percentage, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem BatchNumber
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.BatchNumber, esSystemType.String);
			}
		} 
		
		public esQueryItem ExpiredDate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.ExpiredDate, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem IsPackage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.IsPackage, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsBonusItem
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.IsBonusItem, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsClosed
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.IsClosed, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem Description
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.Description, esSystemType.String);
			}
		} 
		
		public esQueryItem LastUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.LastUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.LastUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem RequestQty
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.RequestQty, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Discount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.Discount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem DiscountInCurrency
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.DiscountInCurrency, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem IsDiscountInPercent
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.IsDiscountInPercent, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem IsInvoiceUpdate
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.IsInvoiceUpdate, esSystemType.Boolean);
			}
		} 
		
		public esQueryItem PriorPrice
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.PriorPrice, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem PriorPriceInCurrency
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.PriorPriceInCurrency, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem PriorDiscount1Percentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.PriorDiscount1Percentage, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem PriorDiscount2Percentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.PriorDiscount2Percentage, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem PriorDiscount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.PriorDiscount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem PriorDiscountInCurrency
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.PriorDiscountInCurrency, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem LastInvoiceUpdateDateTime
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.LastInvoiceUpdateDateTime, esSystemType.DateTime);
			}
		} 
		
		public esQueryItem LastInvoiceUpdateByUserID
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.LastInvoiceUpdateByUserID, esSystemType.String);
			}
		} 
		
		public esQueryItem HistoryPrice
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.HistoryPrice, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem HistoryPriceInCurrency
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.HistoryPriceInCurrency, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem HistoryDiscount1Percentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscount1Percentage, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem HistoryDiscount2Percentage
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscount2Percentage, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem HistoryDiscount
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscount, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem HistoryDiscountInCurrency
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscountInCurrency, esSystemType.Decimal);
			}
		} 
		
		public esQueryItem Specification
		{
			get
			{
				return new esQueryItem(this, ItemTransactionItemBakMetadata.ColumnNames.Specification, esSystemType.String);
			}
		} 
		
	}



    [System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[XmlType("ItemTransactionItemBakCollection")]
	public partial class ItemTransactionItemBakCollection : esItemTransactionItemBakCollection, IEnumerable<ItemTransactionItemBak>
	{
		public ItemTransactionItemBakCollection()
		{

		}
		
		public static implicit operator List<ItemTransactionItemBak>(ItemTransactionItemBakCollection coll)
		{
			List<ItemTransactionItemBak> list = new List<ItemTransactionItemBak>();
			
			foreach (ItemTransactionItemBak emp in coll)
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
				return  ItemTransactionItemBakMetadata.Meta();
			}
		}
		
		
		
		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTransactionItemBakQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		
		override protected esEntity CreateEntityForCollection(DataRow row)
		{
			return new ItemTransactionItemBak(row);
		}

		override protected esEntity CreateEntity()
		{
			return new ItemTransactionItemBak();
		}
		
		
		#endregion


		[BrowsableAttribute( false )]
		public ItemTransactionItemBakQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTransactionItemBakQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}

		public bool Load(ItemTransactionItemBakQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		public ItemTransactionItemBak AddNew()
		{
			ItemTransactionItemBak entity = base.AddNewEntity() as ItemTransactionItemBak;
			
			return entity;
		}

		public ItemTransactionItemBak FindByPrimaryKey(System.String transactionNo, System.String sequenceNo)
		{
			return base.FindByPrimaryKey(transactionNo, sequenceNo) as ItemTransactionItemBak;
		}


		#region IEnumerable<ItemTransactionItemBak> Members

		IEnumerator<ItemTransactionItemBak> IEnumerable<ItemTransactionItemBak>.GetEnumerator()
		{
			System.Collections.IEnumerable enumer = this as System.Collections.IEnumerable;
			System.Collections.IEnumerator iterator = enumer.GetEnumerator();

			while(iterator.MoveNext())
			{
				yield return iterator.Current as ItemTransactionItemBak;
			}
		}

		#endregion
		
		private ItemTransactionItemBakQuery query;
	}


	/// <summary>
	/// Encapsulates the 'ItemTransactionItemBak' table
	/// </summary>

    [System.Diagnostics.DebuggerDisplay("ItemTransactionItemBak ({TransactionNo},{SequenceNo})")]
	[Serializable]
	public partial class ItemTransactionItemBak : esItemTransactionItemBak
	{
		public ItemTransactionItemBak()
		{

		}
	
		public ItemTransactionItemBak(DataRow row)
			: base(row)
		{

		}
		
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ItemTransactionItemBakMetadata.Meta();
			}
		}
		
		
		
		override protected esItemTransactionItemBakQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ItemTransactionItemBakQuery();
				this.InitQuery(query);
			}
			return this.query;
		}
		#endregion
		



		[BrowsableAttribute( false )]
		public ItemTransactionItemBakQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ItemTransactionItemBakQuery();
					base.InitQuery(this.query);
				}

				return this.query;
			}
		}

		public void QueryReset()
		{
			this.query = null;
		}
		

		public bool Load(ItemTransactionItemBakQuery query)
		{
			this.query = query;
			base.InitQuery(this.query);
			return this.Query.Load();
		}
		
		private ItemTransactionItemBakQuery query;
	}



    [System.Diagnostics.DebuggerDisplay("LastQuery = {es.LastQuery}")]
	[Serializable]
		
	public partial class ItemTransactionItemBakQuery : esItemTransactionItemBakQuery
	{
		public ItemTransactionItemBakQuery()
		{

		}		
		
		public ItemTransactionItemBakQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

        override protected string GetQueryName()
        {
            return "ItemTransactionItemBakQuery";
        }
		
			
	}


	[Serializable]
	public partial class ItemTransactionItemBakMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ItemTransactionItemBakMetadata()
		{
			_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.TransactionNo, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.TransactionNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.SequenceNo, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.SequenceNo;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 5;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.ItemID, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.ItemID;
			c.CharacterMaxLength = 10;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.ReferenceNo, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.ReferenceNo;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.ReferenceSequenceNo, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.ReferenceSequenceNo;
			c.CharacterMaxLength = 3;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.Quantity, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.Quantity;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.SRItemUnit, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.SRItemUnit;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.ConversionFactor, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.ConversionFactor;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.QuantityFinishInBaseUnit, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.QuantityFinishInBaseUnit;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.PageNo, 9, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.PageNo;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.CostPrice, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.CostPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.Price, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.Price;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.PriceInCurrency, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.PriceInCurrency;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.Discount1Percentage, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.Discount1Percentage;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.Discount2Percentage, 14, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.Discount2Percentage;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.BatchNumber, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.BatchNumber;
			c.CharacterMaxLength = 20;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.ExpiredDate, 16, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.ExpiredDate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.IsPackage, 17, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.IsPackage;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.IsBonusItem, 18, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.IsBonusItem;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.IsClosed, 19, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.IsClosed;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.Description, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.LastUpdateDateTime, 21, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.LastUpdateDateTime;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.LastUpdateByUserID, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.LastUpdateByUserID;
			c.CharacterMaxLength = 40;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.RequestQty, 23, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.RequestQty;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.Discount, 24, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.Discount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.DiscountInCurrency, 25, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.DiscountInCurrency;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.IsDiscountInPercent, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.IsDiscountInPercent;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.IsInvoiceUpdate, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.IsInvoiceUpdate;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.PriorPrice, 28, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.PriorPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.PriorPriceInCurrency, 29, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.PriorPriceInCurrency;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.PriorDiscount1Percentage, 30, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.PriorDiscount1Percentage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.PriorDiscount2Percentage, 31, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.PriorDiscount2Percentage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.PriorDiscount, 32, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.PriorDiscount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.PriorDiscountInCurrency, 33, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.PriorDiscountInCurrency;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.LastInvoiceUpdateDateTime, 34, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.LastInvoiceUpdateDateTime;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.LastInvoiceUpdateByUserID, 35, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.LastInvoiceUpdateByUserID;
			c.CharacterMaxLength = 15;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.HistoryPrice, 36, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.HistoryPrice;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.HistoryPriceInCurrency, 37, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.HistoryPriceInCurrency;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscount1Percentage, 38, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.HistoryDiscount1Percentage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscount2Percentage, 39, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.HistoryDiscount2Percentage;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscount, 40, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.HistoryDiscount;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.HistoryDiscountInCurrency, 41, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.HistoryDiscountInCurrency;
			c.NumericPrecision = 18;
			c.NumericScale = 2;
			c.IsNullable = true;
			_columns.Add(c);
				
			c = new esColumnMetadata(ItemTransactionItemBakMetadata.ColumnNames.Specification, 42, typeof(System.String), esSystemType.String);
			c.PropertyName = ItemTransactionItemBakMetadata.PropertyNames.Specification;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			_columns.Add(c);
				
		}
		#endregion	
	
		static public ItemTransactionItemBakMetadata Meta()
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
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string ItemID = "ItemID";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string ReferenceSequenceNo = "ReferenceSequenceNo";
			 public const string Quantity = "Quantity";
			 public const string SRItemUnit = "SRItemUnit";
			 public const string ConversionFactor = "ConversionFactor";
			 public const string QuantityFinishInBaseUnit = "QuantityFinishInBaseUnit";
			 public const string PageNo = "PageNo";
			 public const string CostPrice = "CostPrice";
			 public const string Price = "Price";
			 public const string PriceInCurrency = "PriceInCurrency";
			 public const string Discount1Percentage = "Discount1Percentage";
			 public const string Discount2Percentage = "Discount2Percentage";
			 public const string BatchNumber = "BatchNumber";
			 public const string ExpiredDate = "ExpiredDate";
			 public const string IsPackage = "IsPackage";
			 public const string IsBonusItem = "IsBonusItem";
			 public const string IsClosed = "IsClosed";
			 public const string Description = "Description";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string RequestQty = "RequestQty";
			 public const string Discount = "Discount";
			 public const string DiscountInCurrency = "DiscountInCurrency";
			 public const string IsDiscountInPercent = "IsDiscountInPercent";
			 public const string IsInvoiceUpdate = "IsInvoiceUpdate";
			 public const string PriorPrice = "PriorPrice";
			 public const string PriorPriceInCurrency = "PriorPriceInCurrency";
			 public const string PriorDiscount1Percentage = "PriorDiscount1Percentage";
			 public const string PriorDiscount2Percentage = "PriorDiscount2Percentage";
			 public const string PriorDiscount = "PriorDiscount";
			 public const string PriorDiscountInCurrency = "PriorDiscountInCurrency";
			 public const string LastInvoiceUpdateDateTime = "LastInvoiceUpdateDateTime";
			 public const string LastInvoiceUpdateByUserID = "LastInvoiceUpdateByUserID";
			 public const string HistoryPrice = "HistoryPrice";
			 public const string HistoryPriceInCurrency = "HistoryPriceInCurrency";
			 public const string HistoryDiscount1Percentage = "HistoryDiscount1Percentage";
			 public const string HistoryDiscount2Percentage = "HistoryDiscount2Percentage";
			 public const string HistoryDiscount = "HistoryDiscount";
			 public const string HistoryDiscountInCurrency = "HistoryDiscountInCurrency";
			 public const string Specification = "Specification";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string TransactionNo = "TransactionNo";
			 public const string SequenceNo = "SequenceNo";
			 public const string ItemID = "ItemID";
			 public const string ReferenceNo = "ReferenceNo";
			 public const string ReferenceSequenceNo = "ReferenceSequenceNo";
			 public const string Quantity = "Quantity";
			 public const string SRItemUnit = "SRItemUnit";
			 public const string ConversionFactor = "ConversionFactor";
			 public const string QuantityFinishInBaseUnit = "QuantityFinishInBaseUnit";
			 public const string PageNo = "PageNo";
			 public const string CostPrice = "CostPrice";
			 public const string Price = "Price";
			 public const string PriceInCurrency = "PriceInCurrency";
			 public const string Discount1Percentage = "Discount1Percentage";
			 public const string Discount2Percentage = "Discount2Percentage";
			 public const string BatchNumber = "BatchNumber";
			 public const string ExpiredDate = "ExpiredDate";
			 public const string IsPackage = "IsPackage";
			 public const string IsBonusItem = "IsBonusItem";
			 public const string IsClosed = "IsClosed";
			 public const string Description = "Description";
			 public const string LastUpdateDateTime = "LastUpdateDateTime";
			 public const string LastUpdateByUserID = "LastUpdateByUserID";
			 public const string RequestQty = "RequestQty";
			 public const string Discount = "Discount";
			 public const string DiscountInCurrency = "DiscountInCurrency";
			 public const string IsDiscountInPercent = "IsDiscountInPercent";
			 public const string IsInvoiceUpdate = "IsInvoiceUpdate";
			 public const string PriorPrice = "PriorPrice";
			 public const string PriorPriceInCurrency = "PriorPriceInCurrency";
			 public const string PriorDiscount1Percentage = "PriorDiscount1Percentage";
			 public const string PriorDiscount2Percentage = "PriorDiscount2Percentage";
			 public const string PriorDiscount = "PriorDiscount";
			 public const string PriorDiscountInCurrency = "PriorDiscountInCurrency";
			 public const string LastInvoiceUpdateDateTime = "LastInvoiceUpdateDateTime";
			 public const string LastInvoiceUpdateByUserID = "LastInvoiceUpdateByUserID";
			 public const string HistoryPrice = "HistoryPrice";
			 public const string HistoryPriceInCurrency = "HistoryPriceInCurrency";
			 public const string HistoryDiscount1Percentage = "HistoryDiscount1Percentage";
			 public const string HistoryDiscount2Percentage = "HistoryDiscount2Percentage";
			 public const string HistoryDiscount = "HistoryDiscount";
			 public const string HistoryDiscountInCurrency = "HistoryDiscountInCurrency";
			 public const string Specification = "Specification";
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
			lock (typeof(ItemTransactionItemBakMetadata))
			{
				if(ItemTransactionItemBakMetadata.mapDelegates == null)
				{
					ItemTransactionItemBakMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ItemTransactionItemBakMetadata.meta == null)
				{
					ItemTransactionItemBakMetadata.meta = new ItemTransactionItemBakMetadata();
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
				

				meta.AddTypeMap("TransactionNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ItemID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ReferenceSequenceNo", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Quantity", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("SRItemUnit", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ConversionFactor", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("QuantityFinishInBaseUnit", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PageNo", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CostPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Price", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriceInCurrency", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Discount1Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Discount2Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("BatchNumber", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ExpiredDate", new esTypeMap("smalldatetime", "System.DateTime"));
				meta.AddTypeMap("IsPackage", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsBonusItem", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsClosed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("LastUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RequestQty", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Discount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("DiscountInCurrency", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("IsDiscountInPercent", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsInvoiceUpdate", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PriorPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriorPriceInCurrency", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriorDiscount1Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriorDiscount2Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriorDiscount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("PriorDiscountInCurrency", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("LastInvoiceUpdateDateTime", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("LastInvoiceUpdateByUserID", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("HistoryPrice", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HistoryPriceInCurrency", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HistoryDiscount1Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HistoryDiscount2Percentage", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HistoryDiscount", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("HistoryDiscountInCurrency", new esTypeMap("numeric", "System.Decimal"));
				meta.AddTypeMap("Specification", new esTypeMap("varchar", "System.String"));			
				
				
				
				meta.Source = "ItemTransactionItemBak";
				meta.Destination = "ItemTransactionItemBak";
				
				meta.spInsert = "proc_ItemTransactionItemBakInsert";				
				meta.spUpdate = "proc_ItemTransactionItemBakUpdate";		
				meta.spDelete = "proc_ItemTransactionItemBakDelete";
				meta.spLoadAll = "proc_ItemTransactionItemBakLoadAll";
				meta.spLoadByPrimaryKey = "proc_ItemTransactionItemBakLoadByPrimaryKey";
				
				this._providerMetadataMaps["esDefault"] = meta;
			}
			
			return this._providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ItemTransactionItemBakMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
